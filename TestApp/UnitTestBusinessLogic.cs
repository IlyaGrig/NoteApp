using System;
using System.Linq;
using System.Threading;
using BusinessLogicLayer;
using DataAccessLayer;
using Helpers;
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
				var cancellationToken = scope.ServiceProvider.GetService<CancellationTokenSource>();
				var rep = new NotesService(context, iconHelper, cancellationToken);
				var options = new DbContextOptionsBuilder<NoteAppDbContext>()
					.UseInMemoryDatabase(databaseName: "Add_writes_to_database")
					.Options;



				context.Notes.Add(new Note("qw", "qwe", "zxc", "qwewr"));
				context.SaveChanges();





				var count = context.Notes.Count();
				var headerNote = rep.Search("qw").Result.FirstOrDefault()?.HeaderNote;

				Assert.Equal(1, count);
				Assert.Equal("zxc", headerNote);

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
