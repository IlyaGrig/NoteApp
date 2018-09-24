
using System.Threading;
using System.Threading.Tasks;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Controllers
{
	[Authorize]
	public class NotesController : Controller
	{
		private readonly NotesService _rep;
		private readonly CancellationToken _cancellationToken;


		public NotesController(NotesService rep, CancellationToken cancellationToken)
		{
			_rep = rep;
			_cancellationToken = cancellationToken;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> AddNewNote(string nameNote, string headerNote, string textNote)
		{
			await _rep.AddNote(nameNote, headerNote, textNote, User.FindFirst("Id").Value);
			return RedirectPermanent("~/Home");
		}
	}
}
