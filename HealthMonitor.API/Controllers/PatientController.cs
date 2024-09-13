using HealthMonitor.API.Helpers;
using HealthMonitor.API.ViewModels;
using HealthMonitor.Domain.AggregatesModel;
using HealthMonitor.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace HealthMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {

        private readonly ILogger<PatientController> _logger;
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository,
            ILogger<PatientController> logger)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Получить список пациентов
        /// </summary>
        /// <param name="pageSize">Количесвто строк</param>
        /// <param name="pageIndex">Номер страницы</param>
        /// <returns>Список пациентов</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<PatientViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<PatientViewModel>>> Get([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            try
            {
                var patients = _patientRepository.GetAll();
                var patientCount = await patients.CountAsync();
                var selectPatients = await patients.Skip(pageSize * pageIndex)
                .Take(pageSize)
                .Select(p => ViewModelHelper.ConvertToPatientViewModel(p))
                .ToListAsync();

                return Ok(new PaginatedItemsViewModel<PatientViewModel>(pageIndex, pageSize, patientCount, selectPatients));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Получить документ по Id
        /// </summary>
        /// <param name="id">Id пациента</param>
        /// <returns>Пациент</returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PatientViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PatientViewModel>> Get(Guid id)
        {
            try
            {
                var patient = await _patientRepository.Get(id);
                return Ok(ViewModelHelper.ConvertToPatientViewModel(patient));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Создать запись пациент
        /// </summary>
        /// <param name="patient">Данные о пациенте</param>
        /// <returns>Пациент</returns>
        [HttpPost("Create")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PatientViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PatientViewModel>> Post([FromBody] PatientViewModel patient)
        {
            try
            {
                var newPacient = ViewModelHelper.ConvertToPatient(patient);
                newPacient = _patientRepository.Create(newPacient);
                await _patientRepository.UnitOfWork.SaveEntitiesAsync();
                return Ok(ViewModelHelper.ConvertToPatientViewModel(newPacient));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Обновление информации о пациенте
        /// </summary>
        /// <param name="id">Id пациента</param>
        /// <param name="patient">Данные о пациенте</param>
        [HttpPut("Update/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Put(Guid id, [FromBody] PatientViewModel patient)
        {
            try
            {
                var dbPacient = await _patientRepository.Get(id);
                var updatePacient = ViewModelHelper.ConvertToPatient(patient);
                ViewModelHelper.Update(dbPacient, updatePacient);
                _patientRepository.Update(dbPacient);
                await _patientRepository.UnitOfWork.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Удалить сущность пациент
        /// </summary>
        /// <param name="id">Id пациента</param>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _patientRepository.Delete(id);
                await _patientRepository.UnitOfWork.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Поиск по дате рождения пациента
        /// </summary>
        /// <param name="birthDate">Параметры даты рождения</param>
        /// <returns>Список пациентов</returns>
        [HttpGet("Search")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<PatientViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Patient>>> SearchPatients([FromQuery] string[] birthDate)
        {
            try
            {

                if (birthDate.Any())
                {
                    return BadRequest("birthDate parameter is required.");
                }
                var query = _patientRepository.GetAll();
                MatchCollection matches = Regex.Matches(string.Join('&',birthDate), SearchHelper.Pattern, RegexOptions.IgnoreCase);

                foreach (Match match in matches)
                {
                    var operatorQuery = match?.Groups[1]?.Value;
                    if (!DateTime.TryParse(match?.Groups[2]?.Value, out var dateQuery))
                        throw new HealthMonitorException($"Cannot parce DateTime {nameof(dateQuery)}");

                    query = SearchHelper.SearchPatientByBirthDate(operatorQuery, dateQuery, query);

                    Console.WriteLine($"Tag: {match.Groups[1].Value}, Date: {match.Groups[2].Value}");
                }
                var patients = await query
                .Select(p => ViewModelHelper.ConvertToPatientViewModel(p))
                .ToListAsync();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

    }
}
