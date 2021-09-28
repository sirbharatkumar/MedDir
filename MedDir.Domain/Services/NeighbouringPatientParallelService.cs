using MedDir.Domain.ApiModels;
using MedDir.Domain.Constants;
using MedDir.Domain.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedDir.Domain.Services
{
    public class NeighbouringPatientParallelService : IDisposable
    {
        #region Public and Private Members
        public ConcurrentBag<PatientModel> Patients;
        private List<List<int>> _people;
        private int _rowIndex;
        private int _columnIndex;
        private int _groupCount;

        // To detect redundant calls
        private bool _disposed = false;
        #endregion

        #region Constructor
        public NeighbouringPatientParallelService()
        {
        }

        public NeighbouringPatientParallelService(List<List<int>> people, int rowIndex, int columnIndex, int groupCount)
        {
            Patients = new ConcurrentBag<PatientModel>();
            _people = people;
            _rowIndex = rowIndex;
            _columnIndex = columnIndex;
            _groupCount = groupCount;
        }

        ~NeighbouringPatientParallelService() => Dispose(false);
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
                if (!Patients.Any(x => x.RowNumber == rowIndex && x.ColumnNumber == columnIndex))
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
