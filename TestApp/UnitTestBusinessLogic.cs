using System;
using System.Linq;
using BusinessLogicLayer;
using DataAccessLayer;
using IconService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace TestApp
{
    public class UnitTestBusinessLogic
    {
        [Fact]
        public void TestSearch()
        {
			using (var scope = new DIConfigurator().ServiceProvider.CreateScope())
			{
				var iconHelper = scope.ServiceProvider.GetService<IconHelper>();
				var context = scope.ServiceProvider.GetService<NoteAppDbContext>();
				NotesService rep = new NotesService(context, iconHelper);
			}

			var options = new DbContextOptionsBuilder<NoteAppDbContext>()
		        .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
		        .Options;
	        using (var context = new NoteAppDbContext(options))
	        {
		        context.Notes.Add(new Note("qw", "qwe", "zxc", "qwewr"));
		        context.SaveChanges();
				NotesService rep = new NotesService(context,new IconHelper());
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
