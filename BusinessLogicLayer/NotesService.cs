using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Helpers;
using Microsoft.EntityFrameworkCore;
using NoteApp.BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer
{
	public class NotesService : INotesService
	{
		private readonly NoteAppDbContext _db;
		private readonly IconHelper _iconService;
		private readonly CancellationToken _cancellationToken;

		public NotesService(NoteAppDbContext db, IconHelper iconService, CancellationTokenSource cancellationToken)
		{
			_db = db;
			_iconService = iconService;
			_cancellationToken = cancellationToken.Token;
		}

		public async Task<List<Note>> GetNoteList()
		{
			return  await _db.Notes.ToListAsync(_cancellationToken);
		}

		public async Task DeleteNote(int id)
		{

			var note = _db.Notes.First(e => e.NoteId == id);
			if (note != null)
				_db.Notes.Remove(note);
			await _db.SaveChangesAsync(_cancellationToken);
		}

		public async Task AddNote(string name, string header, string text, string userId)
		{
			var newNote = new Note(userId, name, header, text)
			{
				DateNote = DateTime.Now,
				Base64Icon = await _iconService.GetIconAsync()
			};
			await _db.Notes.AddAsync(newNote, _cancellationToken);
			await _db.SaveChangesAsync(_cancellationToken);
		}

		public async Task UpdateNote(int id, string name, string header, string text)
		{

			var note = _db.Notes.First(e => e.NoteId == id);
			if (note != null)
			{
				note.NoteName = name;
				note.HeaderNote = header;
				note.TextNote = text;
				note.DateNote = DateTime.Now;

			}

			await _db.SaveChangesAsync(_cancellationToken);

		}

		public Task<IEnumerable<Note>> Search(string searchingText)
		{

			var search = new Search(_db.Notes.ToListAsync(_cancellationToken), searchingText);
			return search.GetNotesFound();

		}

		public async Task<Note> GetNote(int id)
		{
			
				var note = await _db.Notes.FirstAsync(e => e.NoteId == id, _cancellationToken);
				return note;
		
		}
	}
}
