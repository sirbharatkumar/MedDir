using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MedDir.Domain.ApiModels.RequestModel;
using MedDir.Domain.ApiModels.ResponseModel;
using MedDir.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MedDir.API.Controllers
{
    /// <summary>
    /// Controller to be used to get Patient group details
    /// </summary>
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public partial class PatientGroupsController : ControllerBase
    {
        #region Private Members
        private readonly IPatientGroupsService _patientGroupsService;

        private readonly ILogger<PatientGroupsController> _logger;
        #endregion

        #region Constructor
        public PatientGroupsController(IPatientGroupsService patientGroupsService, ILogger<PatientGroupsController> logger)
        {
            _patientGroupsService = patientGroupsService;
            _logger = logger;

        }
        #endregion

        #region Exposed API
        /// <summary>
        /// Determine the number of patient groups for a given set of people
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///         "matrix":
        ///             [
        ///                 [1, 0, 1, 1, 1],
        ///                 [1, 0, 0, 0, 0],
        ///                 [1, 0, 0, 0, 1],
        ///                 [0, 0, 1, 0, 0],
        ///                 [0, 1, 0, 0, 0],
        ///                 [0, 1, 0, 0, 1]
        ///             ]
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Group of people</param>
        /// <returns>Number of Patient group</returns>
        /// <response code="200">Returns the success response</response>
        /// <response code="400">If the input is not valid</response>  
        /// <response code="401">Unauthorized Access can't proceed further</response>  
        /// <response code="413">Request is large to process</response>  
        [HttpPost]
        [Route("~/api/patient-groups/calculate HTTP/{version:apiVersion}")]
        [ApiVersion("1.1")]
        [RequestSizeLimit(102400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces("application/json", Type = typeof(PatientGroupsResponse))]
        public ActionResult<PatientGroupsResponse> CalculatePatientGroups([FromBody] PatientGroupsRequest request)
        {
            try
            {
                _logger.LogInformation("CalculatePatientGroups Request: {0}", request.ToString());
                return _patientGroupsService.CalculatePatientGroups(request);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception inside CalculatePatientGroups Error: {0}", ex);
                return StatusCode(500, ex);
            }
        }


        /// <summary>
        /// Determine number of patient groups and their standing position for a given set of people
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///         "matrix":
        ///             [
        ///                 [1, 0, 1, 1, 1],
        ///                 [1, 0, 0, 0, 0],
        ///                 [1, 0, 0, 0, 1],
        ///                 [0, 0, 1, 0, 0],
        ///                 [0, 1, 0, 0, 0],
        ///                 [0, 1, 0, 0, 1]
        ///             ]
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Group of people</param>
        /// <returns>Number of Patient group along with standing position</returns>
        /// <response code="200">Returns the success response</response>
        /// <response code="400">If the input is not valid</response> 
        /// <response code="401">Unauthorized Access can't proceed further</response>  
        /// <response code="413">Request is large to process</response>  
        [HttpPost]
        [Route("~/api/patient-groups-detail/calculate HTTP/{version:apiVersion}")]
        [ApiVersion("2.0")]
        [RequestSizeLimit(102400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces("application/json", Type = typeof(PatientGroupsDetailResponse))]
        public ActionResult<PatientGroupsDetailResponse> CalculatePatientGroupsDetail([FromBody] PatientGroupsRequest request)
        {
            try
            {
                _logger.LogInformation("CalculatePatientGroupsDetail Request: {0}", request.ToString());
                return _patientGroupsService.CalculatePatientGroupsDetail(request);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception inside CalculatePatientGroups Error: {0}", ex);
                return StatusCode(500, ex);
            }
        }

        #endregion

    }
}
