namespace BSTU.Results.Authenticate
{
    public interface IAuthenticate
    {
        Task<bool> SignInAsync(string login, string password);
        Task SignOutAsync();
    }
}
