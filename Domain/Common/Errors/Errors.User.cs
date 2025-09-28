using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail =>
            Error.Conflict(code: "User.DuplicateEmail", description: "Correo electrónico ya registrado");

        public static Error NotFound =>
            Error.NotFound(code: "User.UserNotFound", description: "Usuario no encontrado");
        
        public static Error ExternalAuthenticationConflict =>
            Error.Conflict(code: "User.ExternalAuthenticationConflict",
                description: "El usuario se registrado con otro método de autenticación");
        public static Error InvalidHeight =>
            Error.Validation("User.InvalidHeight", "La altura proporcionada no es válida. Debe ser un número positivo con hasta 2 decimales.");

        public static Error InvalidWeight =>
            Error.Validation("User.InvalidWeight", "El peso proporcionado no es válido. Debe ser un número positivo con hasta 2 decimales.");
    }
}