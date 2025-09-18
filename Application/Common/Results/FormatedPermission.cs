/*
using System.Text.RegularExpressions;
using Domain.Entities;

namespace Application.Common.Results;

public class FormattedPermission
{
    private static readonly Regex PermissionRegex = new(@"^[a-zA-Z]+\.[a-zA-Z]+$", RegexOptions.Compiled);

    public string Permission { get; }

    public FormattedPermission(Permission permission)
    {
        if (permission is null)
        {
            throw new ArgumentNullException(nameof(permission), "Permission cannot be null.");
        }

        Permission = permission.Name;
    }

    public override string ToString()
    {
        return Permission;
    }

    public static bool TryParse(string input, out FormattedPermission? formattedPermission)
    {
        formattedPermission = null;

        if (!PermissionRegex.IsMatch(input))
            return false;

        formattedPermission = new FormattedPermission(
            new Permission { Name = input }
        );

        return true;
    }
}
*/