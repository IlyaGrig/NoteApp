using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using Helpers;

namespace BusinessLogicLayer
{
	public class NotesService : INotesService
	{
		private readonly NoteAppDbContext _db;
		private readonly IconHelper _iconService;

		public NotesService(NoteAppDbContext db, IconHelper iconService)
		{
			_db = db;
			_iconService = iconService;
		}

		public async Task<List<Note>> GetNoteList()
		{
			return await Task.Run(() => _db.Notes.ToList());
		}

		public async Task DeleteNote(int id)
		{
			await Task.Run(() =>
			 {
				 var note = _db.Notes.First(e => e.NoteId == id);
				 if (note != null)
					 _db.Notes.Remove(note);
				 _db.SaveChanges();
			 });
		}

		public async Task AddNote(string name, string header, string text, string userId)
		{
			var newNote = new Note(userId, name, header, text)
			{
				DateNote = DateTime.Now,
				Base64Icon = await _iconService.GetIconAsync()
			};
			_db.Notes.Add(newNote);
			_db.SaveChanges();
		}

		public async Task UpdateNote(int id, string name, string header, string text)
		{
			await Task.Run(() =>
			{
				var note = _db.Notes.First(e => e.NoteId == id);
				if (note != null)
				{
					note.NoteName = name;
					note.HeaderNote = header;
					note.TextNote = text;
					note.DateNote = DateTime.Now;

				}

				_db.SaveChanges();
			});
		}

		public async Task<IEnumerable<Note>> Search(string searchingText)
		{
			return await Task.Run(() =>
			{
				var
					search = new Search(_db.Notes.ToList(), searchingText);
				return search.GetNotesFound();
			});
		}

		public async Task<Note> GetNote(int id)
		{
			return await Task.Run(() =>
			{
				var note = _db.Notes.First(e => e.NoteId == id);
				return note;
			});
		}
	}
}
