using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ClosedXML.Excel;

namespace Helpers
{
    public class ExcelHelper
    {
	    private XLWorkbook _workbook;
	    private Dictionary<string,string> _hashTableHeaders;
		private readonly List<string> _headers = new List<string>() { "A1", "B1", "C1", "D1", "E1", "F1", "G1", "H1" };
		private readonly List<List<string>> _rows;
	    public ExcelHelper(int countColunmns, List<string> headers) //The number of specified columns has a higher priority , other column names will not be taken into account
		{
			_rows = new List<List<string>>();
			_hashTableHeaders = new Dictionary<string, string>();
		    for (int i = 0; i < countColunmns; i++)
		    {
			    _hashTableHeaders.Add(_headers[i],headers[i]);
		    }
	    }

		public void AddNewRow(List<string> row)
		{
			_rows.Add(row);
		}

	    private void GenerateExelFile()
	    {
			using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
			{
				var worksheet = workbook.Worksheets.Add("Notes");


				foreach (var head in _hashTableHeaders)
				{
					worksheet.Cell(head.Key).Value = head.Value;
				}

				worksheet.Row(1).Style.Font.Bold = true;


				for (int i = 0; i < _rows.Count; i++)
				{
					for (int j = 0; j < _hashTableHeaders.Count; j++)
					{
						worksheet.Cell(i + 2, j + 1).Value = _rows[i][j];
					}
				}

				_workbook = workbook;

			}
	    }

	    public MemoryStream GetExecelFile()
	    {
			GenerateExelFile();
		    using (MemoryStream stream = new MemoryStream())
		    {
				_workbook.SaveAs(stream);
				stream.Flush();
			    return stream;
		    }
	    }
    }
}
