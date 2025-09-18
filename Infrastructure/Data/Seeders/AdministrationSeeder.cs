using Microsoft.EntityFrameworkCore;
//using Domain.Common.Constants;
using Domain.Entities;
using Infrastructure.Services.Authentication;

namespace Infrastructure.Data.Seeders;

public static class Seeder
{
    public static class AdministrationSeeder
    {
        public static async Task SeedAsync(DbContext context)
        {
            /*
            if (!await context.Set<Permission>().AnyAsync() &&
                !await context.Set<Role>().AnyAsync())
            {
                // Permisos
                var permissions = new List<Permission>
                {
                    new Permission { Name = PermissionsConstants.Users.Create, DisplayName = "Crear usuario" },
                    new Permission { Name = PermissionsConstants.Users.Read, DisplayName = "Ver usuarios" },
                    new Permission { Name = PermissionsConstants.Users.Update, DisplayName = "Actualizar usuario" },
                    new Permission { Name = PermissionsConstants.Users.Delete, DisplayName = "Eliminar usuario" },

                    new Permission { Name = PermissionsConstants.Roles.Create, DisplayName = "Crear rol" },
                    new Permission { Name = PermissionsConstants.Roles.Read, DisplayName = "Ver roles" },
                    new Permission { Name = PermissionsConstants.Roles.Update, DisplayName = "Actualizar rol" },
                    new Permission { Name = PermissionsConstants.Roles.Delete, DisplayName = "Eliminar rol" },
                    new Permission { Name = PermissionsConstants.Roles.UpdatePermissions, DisplayName = "Modificar permisos de rol" },

                    new Permission { Name = PermissionsConstants.EducationalPrograms.Create, DisplayName = "Crear carreras" },
                    new Permission { Name = PermissionsConstants.EducationalPrograms.Delete, DisplayName = "Eliminar carrera" },

                    new Permission { Name = PermissionsConstants.Periods.Create, DisplayName = "Crear Cuatrimestres" },
                    new Permission { Name = PermissionsConstants.Periods.Delete, DisplayName = "Eliminar cuatrimestres" },

                    new Permission { Name = PermissionsConstants.Subjects.Create, DisplayName = "Crear Materias" },
                    new Permission { Name = PermissionsConstants.Subjects.Delete, DisplayName = "Eliminar Materias" },

                    new Permission { Name = PermissionsConstants.Groups.Read, DisplayName = "Ver Grupos" },
                    new Permission { Name = PermissionsConstants.Groups.Create, DisplayName = "Crear Grupos" },
                    new Permission { Name = PermissionsConstants.Groups.Delete, DisplayName = "Eliminar Grupos" },

                    new Permission { Name = PermissionsConstants.Sessions.Read, DisplayName = "Ver sesiones de tutoría" },
                    new Permission { Name = PermissionsConstants.Sessions.Request, DisplayName = "Solicitar tutoría" },
                    new Permission { Name = PermissionsConstants.Sessions.Approve, DisplayName = "Aceptar sesión de tutoría" },
                    new Permission { Name = PermissionsConstants.Sessions.Reject, DisplayName = "Rechazar sesión de tutoría"},
                    new Permission { Name =PermissionsConstants.Sessions.Complete, DisplayName = "Completar sesión" },
                    new Permission { Name = PermissionsConstants.Sessions.GenerateReports, DisplayName = "Generar reportes de tutoría"},

                    new Permission { Name = PermissionsConstants.TutorsRequests.Request, DisplayName = "Solicitar ser tutor" },
                    new Permission { Name = PermissionsConstants.TutorsRequests.Approved, DisplayName = "Aprobar solicitud de tutor" },

                    new Permission { Name = PermissionsConstants.Events.Create, DisplayName = "Crear evento" },
                };

                await context.Set<Permission>().AddRangeAsync(permissions);
                await context.SaveChangesAsync();

                var permissionsIds = await context.Set<Permission>()
                    .ToDictionaryAsync(p => p.Name, p => p.Id);

                var roles = new List<Role>
                {
                    new() { Name = "Administrador", Description = "Acceso total al sistema" },
                    new() { Name = "Estudiante", Description = "Acceso a tutorías y postulaciones" },
                    new() { Name = "Tutor", Description = "Estudiante que brinda tutorías" }
                };

                await context.Set<Role>().AddRangeAsync(roles);
                await context.SaveChangesAsync();

                var rolesIds = roles.ToDictionary(r => r.Name, r => r.Id);

                var rolePermissionsMapping = new Dictionary<string, List<string>>
                {
                    {
                        "Administrador", new List<string>
                        {
                            PermissionsConstants.Users.Create,
                            PermissionsConstants.Users.Read,
                            PermissionsConstants.Users.Update,
                            PermissionsConstants.Users.Delete,
                            PermissionsConstants.Roles.Create,
                            PermissionsConstants.Roles.Read,
                            PermissionsConstants.Roles.Update,
                            PermissionsConstants.Roles.Delete,
                            PermissionsConstants.Roles.UpdatePermissions,
                            PermissionsConstants.EducationalPrograms.Create,
                            PermissionsConstants.EducationalPrograms.Delete,
                            PermissionsConstants.Periods.Create,
                            PermissionsConstants.Periods.Delete,
                            PermissionsConstants.Groups.Read,
                            PermissionsConstants.Groups.Create,
                            PermissionsConstants.Groups.Delete,
                            PermissionsConstants.Subjects.Create,
                            PermissionsConstants.Subjects.Delete,
                            PermissionsConstants.TutorsRequests.Approved,
                            PermissionsConstants.TutorsRequests.Read,
                            PermissionsConstants.Sessions.Create,
                            PermissionsConstants.Sessions.GenerateReports,
                        }
                    },
                    {
                        "Estudiante", new List<string>
                        {
                            PermissionsConstants.Sessions.Read,
                            PermissionsConstants.Sessions.Request,
                            PermissionsConstants.TutorsRequests.Request
                        }
                    },
                    {
                        "Tutor", new List<string>
                        {
                            PermissionsConstants.Sessions.Read,
                            PermissionsConstants.Sessions.Request,
                            PermissionsConstants.Sessions.Approve,
                            PermissionsConstants.Sessions.Reject,
                            PermissionsConstants.Sessions.Complete,
                            PermissionsConstants.Sessions.Update,
                        }
                    }
                };

                var rolePermissions = new List<RolePermission>();

                foreach (var (roleName, perms) in rolePermissionsMapping)
                {
                    var roleId = rolesIds[roleName];
                    foreach (var perm in perms)
                    {
                        if (permissionsIds.TryGetValue(perm, out var permissionId))
                        {
                            rolePermissions.Add(new RolePermission
                            {
                                RoleId = roleId,
                                PermissionId = permissionId
                            });
                        }
                    }
                }

                await context.Set<RolePermission>().AddRangeAsync(rolePermissions);
                await context.SaveChangesAsync();
*/


            // Usuarios iniciales
            if (!await context.Set<User>().AnyAsync())
            {
                var users = new List<User>
                {
                    new()
                    {
                        Name = "Joel",
                        FirstLastName = "Vargas",
                        SecondLastName = "Pérez",
                        Email = "jovap723@gmail.com",
                        Password = "password"
                    },
                    new()
                    {
                        Name = "Celeste",
                        FirstLastName = "Gonzalez",
                        SecondLastName = "Cruz",
                        Email = "23393205@utcancun.edu.mx",
                        Password = "password"
                    },
                    new()
                    {
                        Name = "Renata",
                        FirstLastName = "Cortés",
                        SecondLastName = "Mancilla",
                        Email = "23393204@utcancun.edu.mx",
                        Password = "password"
                    }
                };

                var passwordService = new PasswordService();
                foreach (var user in users)
                {
                    user.Password = passwordService.HashPassword(user, user.Password!);
                }

                await context.Set<User>().AddRangeAsync(users);
                await context.SaveChangesAsync();
            }
        }
    }
}
