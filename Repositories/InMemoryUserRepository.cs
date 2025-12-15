using UserManagementApi.Models;

namespace UserManagementApi.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new()
        {
            new User { Id = 1, FirstName = "Ahmet", LastName = "Yilmaz", Email = "ahmet@techhive.com", Department = "IT", Role = "Admin" },
            new User { Id = 2, FirstName = "Zeynep", LastName = "Kaya", Email = "zeynep@techhive.com", Department = "HR", Role = "Manager" }
        };

        public IEnumerable<User> GetAllUsers() => _users;

        public User? GetUserById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public void AddUser(User user)
        {
            user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
        }

        public void UpdateUser(User user)
        {
            var existingUser = GetUserById(user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Department = user.Department;
                existingUser.Role = user.Role;
            }
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null) _users.Remove(user);
        }
    }
}