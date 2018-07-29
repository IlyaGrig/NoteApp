using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Controllers
{
	public class AddNoteController : Controller
	{
		readonly RepositoryService _rep;

		public AddNoteController(RepositoryService rep)
		{
			_rep = rep;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddNewNote(string userId, string nameNote, string headerNote, string textNote)
		{
			//NotesDbContext context = new NotesDbContext();
			//context.Notes.Add(new Note(userId, nameNote, headerNote, textNote));
			//context.SaveChanges();

			_rep.AddNote(nameNote, headerNote, textNote, userId);

			return RedirectPermanent("~/MainPage");

		}
	}
}
