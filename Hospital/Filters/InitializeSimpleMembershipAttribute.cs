using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using Hospital.Models;
using Hospital.DAL;
using System.Web.Security;

namespace Hospital.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
            SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;


            public void AddDoctorsAndPatients()
            {
                if (membership.GetUser("doctor1", false) == null)
                {
                    WebSecurity.CreateUserAndAccount("doctor1", "111111",
                            new
                            {
                                FirstName = "doctor1",
                                MidName = "doctor1",
                                LastName = "doctor1",
                                BirthDate = 01/01/1985
                            });
                    roles.AddUsersToRoles(new[] { "doctor1" }, new[] { "Doctor" });
                }
                if (membership.GetUser("doctor2", false) == null)
                {
                    WebSecurity.CreateUserAndAccount("doctor2", "111111",
                            new
                            {
                                FirstName = "doctor2",
                                MidName = "doctor2",
                                LastName = "doctor2",
                                BirthDate = 01/01/1985
                            });
                    roles.AddUsersToRoles(new[] { "doctor2" }, new[] { "Doctor" });
                }
                if (membership.GetUser("patient1", false) == null)
                {
                    WebSecurity.CreateUserAndAccount("patient1", "111111",
                           new
                           {
                               FirstName = "patient1",
                               MidName = "patient1",
                               LastName = "patient1",
                               Age = 25
                           });
                    roles.AddUsersToRoles(new[] { "patient1" }, new[] { "Patient" });
                }
                if (membership.GetUser("patient2", false) == null)
                {
                    WebSecurity.CreateUserAndAccount("patient2", "111111",
                           new
                           {
                               FirstName = "patient2",
                               MidName = "patient2",
                               LastName = "patient2",
                               Age = 25
                           });
                    roles.AddUsersToRoles(new[] { "patient2" }, new[] { "Patient" });
                }
            }


            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UserContext>(null);

                try
                {
                    using (var context = new UserContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("UserContext", "User", "UserId", "UserName", autoCreateTables: true);


                    if (!roles.RoleExists("Admin"))
                    {
                        roles.CreateRole("Admin");/*илиRoles.CreateRole("Admin"); */
                    }
                    if (!roles.RoleExists("User"))
                    {
                        roles.CreateRole("User");
                    }
                    if (!roles.RoleExists("Nurse"))
                    {
                        roles.CreateRole("Nurse");
                    }
                    if (!roles.RoleExists("Doctor"))
                    {
                        roles.CreateRole("Doctor");
                    }
                    if (!roles.RoleExists("Patient"))
                    {
                        roles.CreateRole("Patient");
                    }

                    if (membership.GetUser("admin1", false) == null)
                    {
                        
                        WebSecurity.CreateUserAndAccount("admin1", "admin1admin1",
                            new
                            {
                                FirstName = "admin",
                                MidName = "admin",
                                LastName = "admin", 
                                BirthDate = 01/01/1985
                            });
                        roles.AddUsersToRoles(new[] { "admin1" }, new[] { "Admin" });
                    }}
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
