using MedDir.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedDir.Domain.Extensions
{
    public static class Matrix
    {
        /// <summary>
        /// Return the value at the given row and column index if exists
        /// </summary>
        /// <param name="matrix">List of List of int for which the index needs to be provided</param>
        /// <param name="rowIndex">Row number of the List of List</param>
        /// <param name="columnIndex">Column number of the List of List</param>
        /// <returns>Value at the given index location</returns>
        public static int GetCellValue(this List<List<int>> matrix, int rowIndex, int columnIndex)
        {
            if (IndexValueExists(matrix, rowIndex, columnIndex))
            {
                return matrix[rowIndex][columnIndex];
            }
            else
                return ApplicationConstants.ErrorValue;
        }

        /// <summary>
        /// Check if the value exists at the given row and column index
        /// </summary>
        /// <param name="matrix">List of List of int for which the index needs to be provided</param>
        /// <param name="rowIndex">Row number of the List of List</param>
        /// <param name="columnIndex">Column number of the List of List</param>
        /// <returns>true if exists</returns>
        public static bool IndexValueExists(List<List<int>> matrix, int rowIndex, int columnIndex)
        {
            return rowIndex != -1 && columnIndex != -1 && matrix.ElementAtOrDefault(rowIndex) != null && matrix[rowIndex].ElementAtOrDefault(columnIndex) != 0;
        }
    }
}
