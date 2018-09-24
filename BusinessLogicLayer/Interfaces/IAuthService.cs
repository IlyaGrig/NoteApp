using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.VIewModel;
using DataAccessLayer;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAuthService
    {
	    Task<bool> UserCheck(IUserModel model);
	    Task<User> GetUser(IUserModel model);
	    Task SetUser(IUserModel model);

    }
}
