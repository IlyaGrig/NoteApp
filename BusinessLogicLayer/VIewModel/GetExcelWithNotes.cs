using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using DataAccessLayer;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLogicLayer.VIewModel
{
    public class GetExcelWithNotes
    {
	    private ExcelHelper _excelHelper;
	    public GetExcelWithNotes()
	    {
		    _excelHelper = null;
	    }

	    public FileContentResult GetExcelFile(IEnumerable<Note> notes)
	    {
			List<string> headers = new List<string>() {"NoteId","UserId","NameNote","HeaderNote","TextNote","Date"};
			_excelHelper = new ExcelHelper(headers.Count,headers);
		    foreach (var note in notes)
		    {
				List<string> row = new List<string>(){note.NoteId.ToString(),note.UserId,note.NoteName,note.HeaderNote,note.TextNote,note.DateNote.ToString(CultureInfo.InvariantCulture)};
			    _excelHelper.AddNewRow(row);				
		    }
			

		    using (MemoryStream stream = _excelHelper.GetExecelFile())
		    {
			    return new FileContentResult(stream.ToArray(),
				    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
			    {
				    FileDownloadName = $"notes{notes.First().UserId}_{DateTime.UtcNow.ToShortDateString()}.xlsx"
			    };
			}

		   

		}
	}
}
