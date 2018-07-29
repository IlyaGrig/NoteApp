using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using IconService;

namespace BusinessLogicLayer
{
    public class RepositoryService
    {
	    private readonly NoteAppDbContext _db;

	    public RepositoryService (NoteAppDbContext db)
	    {
		    _db = db;
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

	    public void AddNote(string name, string header, string text, string userId)
	    {
			var newNote = new Note(userId, name, header, text)
			{
				DateNote = DateTime.Now,
				Base64Icon = IconBase64.GetIcon()
			};
			_db.Notes.Add(newNote);
		    _db.SaveChanges();

	    }

	    public void UpdateNote(int id, string name, string header, string text)
	    {
		    Note note = _db.Notes.First(e => e.NoteId == id);
		    if (note != null)
		    {
			    _db.Notes.Remove(note);
			    note.NoteName = name; note.HeaderNote = header; note.TextNote = text;
			    note.DateNote = DateTime.Now;
			    _db.Notes.Add(note);
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

	    public void Dispose()
	    {
		    _db.Dispose();
	    }
	}
}
