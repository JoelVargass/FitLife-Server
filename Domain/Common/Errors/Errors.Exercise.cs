using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Exercise
    {
        public static Error DuplicateName =>
            Error.Conflict(
                code: "Exercise.DuplicateName", 
                description: "El ejercicio ya existe con ese nombre."
            );

        public static Error NotFound =>
            Error.NotFound(
                code: "Exercise.NotFound", 
                description: "El ejercicio no fue encontrado."
            );

        public static Error InvalidDuration =>
            Error.Validation(
                code: "Exercise.InvalidDuration", 
                description: "La duración del ejercicio no es válida."
            );

        public static Error MuscleTypeRequired =>
            Error.Validation(
                code: "Exercise.MuscleTypeRequired", 
                description: "El tipo de músculo es requerido para este ejercicio."
            );

        public static Error AlreadyDeleted =>
            Error.Conflict(
                code: "Exercise.AlreadyDeleted", 
                description: "El ejercicio ya ha sido eliminado."
            );

        public static Error CannotRestoreDeleted =>
            Error.Conflict(
                code: "Exercise.CannotRestoreDeleted", 
                description: "No se puede restaurar un ejercicio eliminado."
            );
    }
}