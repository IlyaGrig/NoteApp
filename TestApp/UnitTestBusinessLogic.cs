using System;
using System.Linq;
using BusinessLogicLayer;
using DataAccessLayer;
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
		        context.Notes.Add(new Note("qw", "qwe", "zxc", "qwewr"));
		        context.SaveChanges();
				RepositoryService rep = new RepositoryService(context);
		        var count = context.Notes.Count();
		        var headerNote = rep.Search("qw").FirstOrDefault()?.HeaderNote;

		        Assert.Equal(1,count);
		        Assert.Equal("zxc",headerNote);
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
			    var count = context.Notes.Count();
			    Assert.Equal(0, count);
			}
	    }
	}
}
