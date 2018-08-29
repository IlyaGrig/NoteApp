using System.Collections.Generic;
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
	    public ActionResult Index()
	    {
		    return View(_rep.GetNoteList());
	    }

	    [HttpPost]
	    public ActionResult Search(string searchText)
	    {
		    return View(_rep.Search(searchText));
	    }

	    [HttpPost]
	    public ActionResult Update(int idNote)
	    {

		    return View(_rep.GetNote(idNote));

	    }

	    [HttpPost]
	    public ActionResult SaveUpdate(int idNote, string userId, string base64Icon, string nameNote, string headerNote, string textNote)
	    {

		    _rep.UpdateNote(idNote, nameNote, headerNote, textNote);
		    return RedirectPermanent("~/Home");


	    }

		[HttpPost]
		public ActionResult Delete(int idNote)
		{

			_rep.DeleteNote(idNote);
			return View(_rep.GetNoteList());

		}
	    [HttpPost]
	    public ActionResult GetExcel(List<Note> notes)
	    {
		    return _xlHelper.GetExcelFile(notes);
	    }
	}
}
