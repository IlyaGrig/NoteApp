using System.Threading.Tasks;
using DataAccessLayer;

namespace NoteApp.BusinessLogicLayer.Interfaces
{
    public interface IAuthService
    {
	    Task<bool> UserCheck(IUserModel model);
	    Task<User> GetUser(IUserModel model);
	    Task SetUser(IUserModel model);

    }
}
