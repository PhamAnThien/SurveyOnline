using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SurveyOnline.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Workplace> Workplaces { set; get; }
        public virtual DbSet<WorkplaceType> WorkplaceTypes { set; get; }
        public virtual DbSet<QuestionType> QuestionTypes { set; get; }
        public virtual DbSet<QuestionSurvey> QuestionSurveys { set; get; }
        public virtual DbSet<AnswerTemplates> AnswerTemplates { set; get; }
        public virtual DbSet<AnswerUnit> AnswerUnits { set; get; }
        public virtual DbSet<SurveyProfile> SurveyProfiles { set; get; }
        public virtual DbSet<Department> Departments { set; get; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<SurveyOnline.Models.JobTitle> JobTitles { get; set; }

        public System.Data.Entity.DbSet<SurveyOnline.Models.SurveySubject> SurveySubjects { get; set; }

        public System.Data.Entity.DbSet<SurveyOnline.Models.SurveyGroup> SurveyGroups { get; set; }

        public System.Data.Entity.DbSet<SurveyOnline.Models.Branch> Branches { get; set; }

        public System.Data.Entity.DbSet<SurveyOnline.Models.SubQuestion> SubQuestions { get; set; }

        public System.Data.Entity.DbSet<SurveyOnline.Models.QuestionTypesGroup> QuestionTypesGroups { get; set; }

        public System.Data.Entity.DbSet<SurveyOnline.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<SurveyOnline.Models.ListTable> ListTables { get; set; }
    }
}