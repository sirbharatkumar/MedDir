using MedDir.Domain.ApiModels;
using MedDir.Domain.ApiModels.RequestModel;
using MedDir.Domain.ApiModels.ResponseModel;
using MedDir.Domain.Constants;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedDir.Domain.Services
{
    public class PatientGroupsParallelService : IPatientGroupsService, IDisposable
    {
        #region Private Members
        private ConcurrentBag<PatientModel> _masterPatientList;
        private readonly ILogger<PatientGroupsService> _logger;
        // To detect redundant calls
        private bool _disposed = false;
        #endregion

        #region Constructor
        public PatientGroupsParallelService(ILogger<PatientGroupsService> logger)
        {
            _masterPatientList = new ConcurrentBag<PatientModel>();
            _logger = logger;
        }

        ~PatientGroupsParallelService() => Dispose(false);
        #endregion

        /// <summary>
        /// Determine the number of patient groups for a given set of people
        /// </summary>
        public PatientGroupsResponse CalculatePatientGroups(PatientGroupsRequest patientGroupsRequest)
        {
            PatientGroupsResponse patientGroupsResponse = new PatientGroupsResponse();

            int totalNoOfPeopleTested = IteratePeopleMatrix(patientGroupsRequest.PeopleMatrix);

            patientGroupsResponse.NumberOfPatientGroups = _masterPatientList.GroupBy(x => x.GroupNumber).Count();

            _logger.LogInformation("CalculatePatientGroups Output: {0}", patientGroupsResponse.ToString());

            return patientGroupsResponse;
        }

        /// <summary>
        ///  Determine number of patient groups and their standing position for a given set of people
        /// </summary>
        public PatientGroupsDetailResponse CalculatePatientGroupsDetail(PatientGroupsRequest patientGroupsRequest)
        {
            PatientGroupsDetailResponse patientGroupsDetailResponse = new PatientGroupsDetailResponse();

            patientGroupsDetailResponse.TotalTests = IteratePeopleMatrix(patientGroupsRequest.PeopleMatrix);

            patientGroupsDetailResponse.NumberOfPatientGroups = _masterPatientList.GroupBy(x => x.GroupNumber).Count();

            int postiveCount = _masterPatientList.Count();

            patientGroupsDetailResponse.TotalNumberOfPositive = postiveCount;

            patientGroupsDetailResponse.TotalNumberOfNegative = patientGroupsDetailResponse.TotalTests - postiveCount;

            patientGroupsDetailResponse.InfectedPercentage = ((double)postiveCount / patientGroupsDetailResponse.TotalTests) * 100;

            //patientGroupsDetailResponse.PatientDetails = _masterPatientList;

            _logger.LogInformation("CalculatePatientGroupsDetail Output: {0}", patientGroupsDetailResponse.ToString());

            return patientGroupsDetailResponse;
        }

        /// <summary>
        /// Method to iterate the given list of people and if the person is already a patient it will skip and move on
        /// </summary>
        /// <param name="people">Group of people</param>
        /// <returns>Total number of people tested</returns>
        private int IteratePeopleMatrix(List<List<int>> people)
        {
            int totalTested = 0;
            int groupCount = 0;
            //Iterate the list row style
            for (int i = 0; i < people.Count; i++)
            {
                //Iterate the list column style
                for (int j = 0; j < people[i].Count; j++)
                {
                    int? value = people[i][j];
                    //If the given index value is 1 & the given index is not present it will add to the list
                    if (value.HasValue && value.Value.Equals(ApplicationConstants.InfectedPatientIndicator)
                        && !_masterPatientList.Any(x => x.RowNumber == i && x.ColumnNumber == j))
                    {
                        groupCount++;
                        PreparePatientGroups(people, i, j, groupCount);
                    }
                }
                totalTested += people[i].Count;
            }

            return totalTested;
        }

        /// <summary>
        /// Prepare the patient list based on the group, incase if the person is infected it will check the surronding person if they are also infected
        /// And create the list in one go
        /// </summary>
        /// <param name="people">Group of people</param>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnIndex">Column index</param>
        /// <param name="groupCount">Current Group number</param>
        private void PreparePatientGroups(List<List<int>> people, int rowIndex, int columnIndex, int groupCount)
        {
            if (!_masterPatientList.Any(x => x.RowNumber == rowIndex && x.ColumnNumber == columnIndex))
            {
                _masterPatientList.Add(new PatientModel { RowNumber = rowIndex, ColumnNumber = columnIndex, GroupNumber = string.Format("Group_{0}", groupCount) });
            }

            //This is prepare the list of neighbouring patient which are adjacent to the current patient
            using (NeighbouringPatientParallelService neighbouringPatientService = new NeighbouringPatientParallelService(people, rowIndex, columnIndex, groupCount))
            {
                neighbouringPatientService.DetermineSurroundingPatients();

                //Iterate the adjacent patient and get all their surrounding patient. This will continue untill no adjacent patient is found.
                Parallel.ForEach(neighbouringPatientService.Patients, patient => {
                    if (!_masterPatientList.Any(x => x.RowNumber == patient.RowNumber && x.ColumnNumber == patient.ColumnNumber))
                    {
                        _masterPatientList.Add(patient);
                        this.PreparePatientGroups(people, patient.RowNumber, patient.ColumnNumber, groupCount);
                    }
                });
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed objects that implement IDisposable.
                // Assign null to managed objects that consume large amounts of memory or consume scarce resources.
            }

            // Free unmanaged resources (unmanaged objects).

            _disposed = true;
        }
    }
}
