using Microsoft.EntityFrameworkCore;
using Sprint.Models;
using Sprint.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprint.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext db;
        public UserRepository(ApplicationDbContext appdb)
        {
            this.db = appdb;
        }

        public async Task<ApplicationUser> getUser(string id)
        {
            ApplicationUser user = await db.Users.FindAsync(id) as ApplicationUser;
            return user;
        }

        public async Task<ApplicationUser> getUserByEmail(string email)
        {
            ApplicationUser user = await db.Users.Where(u => u.Email == email).FirstOrDefaultAsync() as ApplicationUser;
            return user;
        }

    }
}