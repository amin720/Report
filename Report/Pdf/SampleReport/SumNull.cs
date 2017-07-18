using System;
using System.Collections.Generic;
using System.Linq;
using PdfRpt.Core.Contracts;


namespace Report.Pdf.SampleReport
{
	public class SumNull : IAggregateFunction
	{
		/// <summary>
		/// Fires before rendering of this cell.
		/// Now you have time to manipulate the received object and apply your custom formatting function.
		/// It can be null.
		/// </summary>
		public Func<object, string> DisplayFormatFormula { set; get; }

		#region Fields (2)

		double _groupSum;
		double _overallSum;

		#endregion Fields

		#region Properties (2)

		/// <summary>
		/// Returns current groups' aggregate value.
		/// </summary>
		public object GroupValue
		{
			get { return _groupSum; }
		}

		/// <summary>
		/// Returns current row's aggregate value without considering the presence of the groups.
		/// </summary>
		public object OverallValue
		{
			get { return _overallSum; }
		}

		#endregion Properties

		#region Methods (2)

		// Public Methods (1) 

		/// <summary>
		/// Fires after adding a cell to the main table.
		/// </summary>
		/// <param name="cellDataValue">Current cell's data</param>
		/// <param name="isNewGroupStarted">Indicated starting a new group</param>
		public void CellAdded(object cellDataValue, bool isNewGroupStarted)
		{
			checkNewGroupStarted(isNewGroupStarted);


			double value;
			Double.TryParse(cellDataValue.ToString(), out value);
			_groupSum += value;
			_overallSum += value;
		}
		// Private Methods (1) 

		private void checkNewGroupStarted(bool newGroupStarted)
		{
			if (newGroupStarted)
			{
				_groupSum = 0;
			}
		}

		/// <summary>
		/// A general method which takes a list of data and calculates its corresponding aggregate value.
		/// It will be used to calculate the aggregate value of each pages individually, with considering the previous pages data.
		/// </summary>
		/// <param name="columnCellsSummaryData">List of data</param>
		/// <returns>Aggregate value</returns>
		public object ProcessingBoundary(IList<SummaryCellData> columnCellsSummaryData)
		{
			if (columnCellsSummaryData == null || !columnCellsSummaryData.Any()) return 0;

			var list = columnCellsSummaryData;
			return list.Sum(item => Convert.ToInt64(String.IsNullOrEmpty(item.CellData.PropertyValue.ToString()) ? "0" : item.CellData.PropertyValue));
		}

		#endregion Methods

	}
}