using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
	public class Search
	{
		public IEnumerable<Note> InListNote { get; set; }
		public string SearchText { get; set; }
		public Search(Task<List<Note>> inListNote, string searchText)
		{
			InListNote = inListNote.Result;
			SearchText = searchText ?? "";
		}

		public Task<IEnumerable<Note>> GetNotesFound()
		{
			return Task.Run( () => InListNote.Where(e => e.HeaderNote.Contains(SearchText) ||
										 e.NoteName.Contains(SearchText)));
		}
	}
}
