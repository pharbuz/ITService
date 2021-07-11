namespace ITService.Domain.Command.User
{
    public sealed class LoginCommand : ICommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}