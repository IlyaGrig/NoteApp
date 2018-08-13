using System;
using System.Linq;
using BusinessLogicLayer;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestApp
{
    public class UnitTestBusinessLogic
    {
        [Fact]
        public void TestSearch()
        {
	        var options = new DbContextOptionsBuilder<NoteAppDbContext>()
		        .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
		        .Options;
	        using (var context = new NoteAppDbContext(options))
	        {
		        var count = context.Notes.Count();
				Assert.Equal(0,count);
	        }
		}
	    [Fact]
	    public void TestRepository()
	    {
		    var options = new DbContextOptionsBuilder<NoteAppDbContext>()
			    .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
			    .Options;
		    using (var context = new NoteAppDbContext(options))
		    {

		    }
	    }
	}
}
