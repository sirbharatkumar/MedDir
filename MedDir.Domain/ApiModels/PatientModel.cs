using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MedDir.Domain.ApiModels
{
    public class PatientModel
    {
        [JsonIgnore]
        public int RowNumber { get; set; }

        [JsonIgnore]
        public int ColumnNumber { get; set; }
        public string Position { get { return string.Format("{0}{1}", RowNumber, ColumnNumber); } }
        public string GroupNumber { get; set; }
    }
}
