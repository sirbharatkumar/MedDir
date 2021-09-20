using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedDir.Domain.ApiModels.ResponseModel
{
    public class PatientGroupsResponse
    {
        [JsonProperty("numberOfGroups")]
        public int NumberOfPatientGroups { get; set; }
    }
}
