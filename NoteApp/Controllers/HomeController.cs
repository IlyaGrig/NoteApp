using BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
	    


		NotesService _rep;
	    public HomeController(NotesService rep)
	    {
		    _rep = rep;
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
	}
}
