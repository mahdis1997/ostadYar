using System.Collections.Generic;
using System.Data.Entity;

namespace OstadYarProject.Models
{
    public class DataBaseContext:DbContext
    {
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectStudent> ProjectStudent { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentTest> StudentTest { get; set; }
        public DbSet<User> User { get; set; }

        public DataBaseContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new OstadYarDbContextInitializer());
            Database.Initialize(true);
        }
        public class OstadYarDbContextInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
        {
            protected override void Seed(DataBaseContext context)
            {
                IList<User> roleses = new List<User>();
                roleses.Add(new User() { UserName = "Admin", Password = "1234" ,IsAdmin = true});
                foreach (User std in roleses)
                    context.User.Add(std);
                base.Seed(context);
            }
        }
    }
    
}