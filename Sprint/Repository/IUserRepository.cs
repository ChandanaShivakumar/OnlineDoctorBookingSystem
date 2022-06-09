using Sprint.Models;
using Sprint.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprint.Repository
{
    public interface IUserRepository
    {
        Task<ApplicationUser> getUser(string id);
        Task<ApplicationUser> getUserByEmail(string email);
    }
}
