using System.ComponentModel.DataAnnotations;
using BusinessLogicLayer.VIewModel;

namespace BusinessLogicLayer
{
	public class LoginModel : IUserModel
	{
		[Required(ErrorMessage = "Не указан Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Не указан пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
