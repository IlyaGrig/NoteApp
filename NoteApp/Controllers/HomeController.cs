﻿using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
		RepositoryService _rep;
	    public HomeController(RepositoryService rep)
	    {
		    _rep = rep;
	    }
	    [HttpGet]
	    public ActionResult Index()
	    {
		    return View(_rep.GetNoteList());
	    }

	    [HttpGet]
	    public ActionResult HiNoname()
	    {
		    return View();
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
		    return RedirectPermanent("~/MainPage");


	    }

	    [HttpPost]
	    public ActionResult Delete(int idNote)
	    {

		    _rep.DeleteNote(idNote);
		    return View(_rep.GetNoteList());

	    }
	}
}
