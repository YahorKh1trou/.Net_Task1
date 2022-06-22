using System.Collections.Generic;

namespace Task1
{
    public interface IDataRepository
    {
        List<User> AddUser(User user);
        List<User> GetUsers();
        List<User> FindUsers(string lastname);
        User FindUserById(int id);
        List<User> RemoveUser(User user);
    }
}
