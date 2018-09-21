using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.VIewModel
{
	public interface IUserModel
	{
		string Email { get; set; }
		string Password { get; set; }
	}
}
