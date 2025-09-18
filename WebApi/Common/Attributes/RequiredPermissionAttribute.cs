
namespace WebApi.Common.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class RequiredPermissionAttribute : Attribute
{
    public string Permission { get; }

    public RequiredPermissionAttribute(string permission)
    {
        Permission = permission;
    }
}
