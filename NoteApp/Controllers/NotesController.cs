
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

		public NotesController(NotesService rep)
		{
			_rep = rep;
		}

		public async Task<ActionResult> Index()
		{
			return await Task.Run(() => View());
		}

		[HttpPost]
		public async Task<ActionResult> AddNewNote(string nameNote, string headerNote, string textNote)
		{
			return await Task.Run(async () =>
			{
				await _rep.AddNote(nameNote, headerNote, textNote, User.FindFirst("Id").Value);
				return RedirectPermanent("~/Home");
			});

		}
	}
}
