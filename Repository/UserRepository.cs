using TokenJwtBearer.Model;

namespace TokenJwtBearer.Repository
{
    public static class UserRepository
    {
        public static User Get (string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "Rogerio Ceni", Password = "mundial2005", Role = "manager" });
            users.Add(new User { Id = 2, Username = "Julio Casares", Password = "golpista", Role = "employee" });

            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password.ToLower() == password.ToLower()).FirstOrDefault();
        }
    }
}