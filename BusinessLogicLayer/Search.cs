using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;

namespace BusinessLogicLayer
{
	public class Search
	{
		public IEnumerable<Note> InListNote { get; set; }
		public string SearchText { get; set; }
		public Search(IEnumerable<Note> inListNote, string searchText)
		{
			InListNote = inListNote;
			SearchText = searchText ?? "";
		}

		public IEnumerable<Note> GetNotesFound()
		{
			return InListNote.Where(e => e.HeaderNote.Contains(SearchText) ||
			                             e.NoteName.Contains(SearchText));
		}
	}
}
