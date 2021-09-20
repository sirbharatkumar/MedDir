using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedDir.Domain.ApiModels.RequestModel
{
    public class PatientGroupsRequest
    {
        [JsonProperty(PropertyName = "matrix")]
        [Required(ErrorMessage = "People matrix is required.")]
        public List<List<int>> PeopleMatrix { get; set; }
    }
}
