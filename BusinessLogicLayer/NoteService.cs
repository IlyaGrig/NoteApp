using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IconService;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer
{
    public class NotesService : Interfaces.INotesService
	{
		private readonly NoteAppDbContext _db;
		private readonly IconHelper _iconService;

	    public NotesService(NoteAppDbContext db,IconHelper iconService)
	    {
		    _db = db;
		    _iconService = iconService;
	    }

	    public List<Note> GetNoteList()
	    {
		    return _db.Notes.ToList();
	    }

	    public void DeleteNote(int id)
	    {
		    var note = _db.Notes.First(e => e.NoteId == id);
		    if (note != null)
			    _db.Notes.Remove(note);
		    _db.SaveChanges();
	    }

	    public async void AddNoteAsync(string name, string header, string text, string userId)
	    {
			var newNote = new Note(userId, name, header, text)
			{
				DateNote = DateTime.Now,
				Base64Icon = await _iconService.GetIconAsync()
			};
			_db.Notes.Add(newNote);
		    _db.SaveChanges();
	    }

	    public void UpdateNote(int id, string name, string header, string text)
	    {
		    var note = _db.Notes.First(e => e.NoteId == id);
		    if (note != null)
		    {
			    //_db.Notes.Remove(note);
			    note.NoteName = name; note.HeaderNote = header; note.TextNote = text;
			    note.DateNote = DateTime.Now;
			    //_db.Notes.Add(note);
		    }
		    _db.SaveChanges();
	    }

	    public IEnumerable<Note> Search(string searchingText)
	    {
		    var search = new Search(_db.Notes.ToList(), searchingText);
		    return search.GetNotesFound();
	    }

	    public Note GetNote(int id)
	    {
		    var note = _db.Notes.First(e => e.NoteId == id);
		    return note ?? null;
	    }
	}
}
