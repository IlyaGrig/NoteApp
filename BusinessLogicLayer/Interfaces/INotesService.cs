using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;

namespace BusinessLogicLayer.Interfaces
{
	public interface INotesService
    {
	    List<Note> GetNoteList();
	    void DeleteNote(int id);
	    void AddNoteAsync(string name, string header, string text, string userId);
	    void UpdateNote(int id, string name, string header, string text);
	    IEnumerable<Note> Search(string searchingText);
	    Note GetNote(int id);

    }
}
