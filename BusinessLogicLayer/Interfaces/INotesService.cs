using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer.Interfaces
{
	public interface INotesService
    {
	    Task<List<Note>> GetNoteList();
	    Task DeleteNote(int id);
	    Task AddNote(string name, string header, string text, string userId);
	    Task UpdateNote(int id, string name, string header, string text);
	    Task<IEnumerable<Note>> Search(string searchingText);
	    Task<Note> GetNote(int id);
    }
}
