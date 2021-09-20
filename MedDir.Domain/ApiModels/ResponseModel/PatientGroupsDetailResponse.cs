using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedDir.Domain.ApiModels.ResponseModel
{
    public class PatientGroupsDetailResponse
    {
        [JsonProperty("numberOfGroups")]
        public int NumberOfPatientGroups { get; set; }

        public int TotalTests { get; set; }

        public int TotalNumberOfNegative { get; set; }

        public int TotalNumberOfPositive { get; set; }

        [JsonIgnore]
        public double InfectedPercentage { get; set; }

        public string InfectedPercentageDisplay
        {
            get { return string.Format("{0}% percentage of people are currently infected.", InfectedPercentage.ToString("00")); }
        }

        public List<PatientModel> PatientDetails { get; set; }


    }
}
