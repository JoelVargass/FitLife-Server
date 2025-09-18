namespace Application.Interfaces.Services;

public interface IUserService
{
    Guid GetUserId();
    string GetUserEmail();
    //string GetUserRole();
    //bool HasPermission(string permission);
    //IEnumerable<string> GetPermissions();
    
    //bool HasSuperAccess();
    
}