using BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Controllers
{
	[Authorize]
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
		public ActionResult AddNewNote(string nameNote, string headerNote, string textNote)
		{
			_rep.AddNote(nameNote, headerNote, textNote, User.FindFirst("Id").Value);

			return RedirectPermanent("~/Home");

		}
	}
}
