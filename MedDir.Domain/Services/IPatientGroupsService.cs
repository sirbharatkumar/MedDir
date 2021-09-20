using MedDir.Domain.ApiModels.RequestModel;
using MedDir.Domain.ApiModels.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedDir.Domain.Services
{
    public interface IPatientGroupsService
    {
        /// <summary>
        /// Determine the number of patient groups for a given set of people
        /// </summary>
        /// <param name="patientGroupsRequest">Group of people</param>
        /// <returns>Number of Patient group</returns>
        PatientGroupsResponse CalculatePatientGroups(PatientGroupsRequest patientGroupsRequest);

        /// <summary>
        /// Determine number of patient groups and their standing position for a given set of people
        /// </summary>
        /// <param name="patientGroupsRequest">Group of people</param>
        /// <returns>Number of Patient group along with standing position</returns>
        PatientGroupsDetailResponse CalculatePatientGroupsDetail(PatientGroupsRequest patientGroupsRequest);
    }
}
