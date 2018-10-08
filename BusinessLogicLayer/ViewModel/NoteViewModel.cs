using System;

namespace NoteApp.BusinessLogicLayer.ViewModel
{
    public class NoteViewModel
    {
	    public int NoteId { get; set; }
	    public string Base64Icon { get; set; }
	    public string UserId { get; set; }
	    public string NoteName { get; set; }
	    public string HeaderNote { get; set; }
	    public string TextNote { get; set; }
	    public DateTime DateNote { get; set; }

	    public NoteViewModel(string userId, string noteName, string headerNote, string textNote)
	    {
		    UserId = userId;
		    NoteName = noteName;
		    HeaderNote = headerNote;
		    TextNote = textNote;
	    }
	}
}
