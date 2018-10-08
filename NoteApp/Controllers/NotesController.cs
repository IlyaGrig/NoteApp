
using System.Threading;
using System.Threading.Tasks;
using NoteApp.BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.BusinessLogicLayer.Interfaces;

namespace NoteApp.Controllers
{
	[Authorize]
	public class NotesController : Controller
	{
		private readonly INotesService _rep;
		private readonly CancellationTokenSource _cancellationToken;


		public NotesController(INotesService rep, CancellationTokenSource cancellationToken)
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
