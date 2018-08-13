using BusinessLogicLayer;
using System;
using Xunit;

namespace Tests
{
	public class BLL
	{
		[Fact]
		public void IndexTest()
		{
			// Arrange
			RepositoryService

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.Equal("Hello world!", result?.ViewData["Message"]);
			Assert.NotNull(result);
			Assert.Equal("Index", result?.ViewName);
		}
	}
}
