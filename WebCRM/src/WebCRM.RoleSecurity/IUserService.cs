namespace WebCRM.RoleSecurity
{
    using WebCRM.RoleSecurity.Helpers;
    
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(int id);
    }
}