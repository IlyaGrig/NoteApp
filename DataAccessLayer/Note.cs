using System;

namespace DataAccessLayer
{
    public class Note
    {
	    public int NoteId { get; set; }
	    public string Base64Icon { get; set; }
	    public string UserId { get; set; }
	    public string NoteName { get; set; }
	    public string HeaderNote { get; set; }
	    public string TextNote { get; set; }
	    public DateTime DateNote { get; set; }

	    public Note(string userId, string noteName, string headerNote, string textNote)
	    {
		    UserId = userId;
		    NoteName = noteName;
		    HeaderNote = headerNote;
		    TextNote = textNote;
	    }
	}
}
