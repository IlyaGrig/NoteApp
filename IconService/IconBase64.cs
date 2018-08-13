using System;
using System.IO;
using System.Net.Http;

namespace IconService
{
    public class IconBase64
    {
	    public static string GetIcon()
	    {
		    string imgInString64 = "";
		    using (var client = new HttpClient())
		    {
			    using (var response = client.GetStreamAsync($"https://avatars.dicebear.com/v2/identicon/пасхалка{DateTime.Now}.svg").Result)
			    {
				    MemoryStream q = new MemoryStream();
				    response.CopyTo(q);
				    q.Seek(0, SeekOrigin.Begin);
				    byte[] byteArray = new byte[q.Length];
				    q.ReadAsync(byteArray, 0, (int)q.Length);
				    imgInString64 = Convert.ToBase64String(byteArray);

			    }
		    }
		    return imgInString64;
		}
    }
}
