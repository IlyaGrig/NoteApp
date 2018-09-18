
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
		readonly NotesService _rep;
		private readonly CancellationToken _cancellationToken;


		public NotesController(NotesService rep, CancellationToken cancellationToken)
		{
			_rep = rep;
			_cancellationToken = cancellationToken;
		}

		public async Task<ActionResult> Index()
		{
			return await Task.Run(() => View(), _cancellationToken);
		}

		[HttpPost]
		public async Task<ActionResult> AddNewNote(string nameNote, string headerNote, string textNote)
		{
			return await Task.Run(async () =>
			{
				await _rep.AddNote(nameNote, headerNote, textNote, User.FindFirst("Id").Value);
				return RedirectPermanent("~/Home");
			}, _cancellationToken);

		}
	}
}
