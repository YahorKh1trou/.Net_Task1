using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class DataRepository : IDataRepository
    {
        private readonly ApplicationContext db = new ApplicationContext();
        public DataRepository()
        {
            this.db = db;
        }
        public List<User> GetUsers() => db.Users.ToList();
        public List<User> AddUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return db.Users.ToList();
        }
        public List<User> FindUsers(string lastname) => db.Users.Where(p => EF.Functions.Like(p.Lastname, $"%{lastname}%")).ToList();
        public User FindUserById(int id) => db.Users.FirstOrDefault(x => x.Id == id);
        public List<User> RemoveUser(User user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
            return db.Users.ToList();
        }
    }
}
