using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Controllers
{
	[Authorize]
	public class NotesController : Controller
	{
		readonly INotesService _rep;

		public NotesController(INotesService rep)
		{
			_rep = rep;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddNewNote(string nameNote, string headerNote, string textNote)
		{
			_rep.AddNoteAsync(nameNote, headerNote, textNote, User.FindFirst("Id").Value);

			return RedirectPermanent("~/Home");

		}
	}
}
