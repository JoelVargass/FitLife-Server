using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Plan
    {
        // Errores de la entidad Plan
        public static Error DuplicateName =>
            Error.Conflict(
                code: "Plan.DuplicateName", 
                description: "Ya existe un plan con ese nombre."
            );

        public static Error NotFound =>
            Error.NotFound(
                code: "Plan.NotFound", 
                description: "El plan no fue encontrado."
            );

        public static Error AlreadyDeleted =>
            Error.Conflict(
                code: "Plan.AlreadyDeleted", 
                description: "El plan ya ha sido eliminado."
            );

        public static Error CannotRestoreDeleted =>
            Error.Conflict(
                code: "Plan.CannotRestoreDeleted", 
                description: "No se puede restaurar un plan eliminado."
            );

        public static Error InvalidName =>
            Error.Validation(
                code: "Plan.InvalidName",
                description: "El nombre del plan es inválido o está vacío."
            );

        public static Error InvalidDescription =>
            Error.Validation(
                code: "Plan.InvalidDescription",
                description: "La descripción del plan no puede exceder el límite permitido."
            );

        public static Error EmptyPlan =>
            Error.Validation(
                code: "Plan.EmptyPlan",
                description: "El plan no contiene ningún ejercicio."
            );
    }

    public static class PlanExercise
    {
        public static Error ExerciseAlreadyInPlan =>
            Error.Conflict(
                code: "PlanExercise.ExerciseAlreadyInPlan",
                description: "El ejercicio ya está incluido en este plan."
            );

        public static Error NotFound =>
            Error.NotFound(
                code: "PlanExercise.NotFound",
                description: "La relación entre el plan y el ejercicio no fue encontrada."
            );

        public static Error InvalidDayOfWeek =>
            Error.Validation(
                code: "PlanExercise.InvalidDayOfWeek",
                description: "El día de la semana para el ejercicio no es válido."
            );

        public static Error SeriesOrRepsRequired =>
            Error.Validation(
                code: "PlanExercise.SeriesOrRepsRequired",
                description: "Se deben especificar series y repeticiones para el ejercicio."
            );

        public static Error InvalidWeight =>
            Error.Validation(
                code: "PlanExercise.InvalidWeight",
                description: "El peso especificado para el ejercicio no es válido."
            );

        public static Error CannotAddDeletedPlanExercise =>
            Error.Conflict(
                code: "PlanExercise.CannotAddDeletedPlanExercise",
                description: "No se puede añadir un ejercicio a un plan eliminado."
            );
    }
}
