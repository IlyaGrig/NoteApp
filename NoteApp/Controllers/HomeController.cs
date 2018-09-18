using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer;
using BusinessLogicLayer.VIewModel;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {

		private readonly NotesService _rep;
		private readonly GetExcelWithNotes _xlHelper;

		public HomeController(NotesService rep, GetExcelWithNotes xlHelper)
	    {
			_rep = rep;
			_xlHelper = xlHelper;

	    }
	    [Authorize]
		[HttpGet]
	    public async Task<ActionResult> Index()
	    {
		    return View(await _rep.GetNoteList());
	    }

	    [HttpPost]
	    public async Task<ActionResult> Search(string searchText)
	    {
		    return View(await _rep.Search(searchText));
	    }

	    [HttpPost]
	    public async Task<ActionResult> Update(int idNote)
	    {
			return View(await _rep.GetNote(idNote));
	    }

	    [HttpPost]
	    public async Task<ActionResult> SaveUpdate(int idNote, string userId, string base64Icon, string nameNote, string headerNote, string textNote)
	    {
		    await _rep.UpdateNote(idNote, nameNote, headerNote, textNote);
		    return RedirectPermanent("~/Home");
	    }

		[HttpPost]
		public async Task<ActionResult> Delete(int idNote)
		{
			await _rep.DeleteNote(idNote);
			var x = await _rep.GetNoteList();
			return View(x);
		}
	    [HttpPost]
	    public async Task<FileContentResult> GetExcel(List<Note> notes)
	    {		    
			return _xlHelper.GetExcelFile(await _rep.GetNoteList());;
	    }
	}
}
