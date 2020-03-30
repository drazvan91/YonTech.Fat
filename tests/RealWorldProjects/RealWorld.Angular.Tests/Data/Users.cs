namespace RealWorld.Angular.Tests.Data
{
  public class UserData
  {
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
  }

  public static class Users
  {
    public static UserData Drazvan91 = new UserData()
    {
      Email = "drazvan91@gmail.com",
      Password = "password",
      UserName = "fat_user"
    };

    public static UserData WrongUser = new UserData()
    {
      Email = "Wrong_User@abc.com",
      Password = "wrong passsword",
      UserName = "wrong_username"
    };
  }
}