using System.Collections.Generic;
using System.Threading.Tasks;

using Blooso.Models;

namespace Blooso.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();

        User GetUser(int id);
    }
}