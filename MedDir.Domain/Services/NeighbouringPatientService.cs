using MedDir.Domain.ApiModels;
using MedDir.Domain.Constants;
using MedDir.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedDir.Domain.Services
{
    public class NeighbouringPatientService
    {
        #region Public and Private Members
        public List<PatientModel> Patients;
        private List<List<int>> _people;
        private int _rowIndex;
        private int _columnIndex;
        private int _groupCount;
        #endregion

        #region Constructor
        public NeighbouringPatientService()
        {
        }

        public NeighbouringPatientService(List<List<int>> people, int rowIndex, int columnIndex, int groupCount)
        {
            Patients = new List<PatientModel>();
            _people = people;
            _rowIndex = rowIndex;
            _columnIndex = columnIndex;
            _groupCount = groupCount;
        }
        #endregion

        /// <summary>
        /// Add the patient to the list if the required condition is satisfied
        /// </summary>
        /// <param name="cellValue">Patient value</param>
        /// <param name="rowIndex">Patient standing row index</param>
        /// <param name="columnIndex">Patient standing column index</param>
        public void PrepareInfectedPatientList(int cellValue, int rowIndex, int columnIndex)
        {
            if (cellValue.Equals(ApplicationConstants.InfectedPatientIndicator))
                if (!Patients.Exists(x => x.RowNumber == rowIndex && x.ColumnNumber == columnIndex))
                    Patients.Add(new PatientModel { RowNumber = rowIndex, ColumnNumber = columnIndex, GroupNumber = string.Format("Group_{0}", _groupCount) });
        }

        /// <summary>
        /// Get all the infected patients based on the current patient standing
        /// </summary>
        public void DetermineSurroundingPatients()
        {
            //Look for the person: UP
            PrepareInfectedPatientList(_people.GetCellValue(_rowIndex - 1, _columnIndex), _rowIndex - 1, _columnIndex);
            //Look for the person: UPRIGHT
            PrepareInfectedPatientList(_people.GetCellValue(_rowIndex - 1, _columnIndex + 1), _rowIndex - 1, _columnIndex + 1);
            //Look for the person: RIGHT
            PrepareInfectedPatientList(_people.GetCellValue(_rowIndex, _columnIndex + 1), _rowIndex, _columnIndex + 1);
            //Look for the person: DOWNRIGHT
            PrepareInfectedPatientList(_people.GetCellValue(_rowIndex + 1, _columnIndex + 1), _rowIndex + 1, _columnIndex + 1);
            //Look for the person: DOWN
            PrepareInfectedPatientList(_people.GetCellValue(_rowIndex + 1, _columnIndex), _rowIndex + 1, _columnIndex);
            //Look for the person: DOWNLEFT
            PrepareInfectedPatientList(_people.GetCellValue(_rowIndex + 1, _columnIndex - 1), _rowIndex + 1, _columnIndex - 1);
            //Look for the person: LEFT
            PrepareInfectedPatientList(_people.GetCellValue(_rowIndex, _columnIndex - 1), _rowIndex, _columnIndex - 1);
            //Look for the person: UPLEFT
            PrepareInfectedPatientList(_people.GetCellValue(_rowIndex - 1, _columnIndex - 1), _rowIndex - 1, _columnIndex - 1);
         
        }
    }
}
