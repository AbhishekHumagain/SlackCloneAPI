using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using slack_clone_model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace slack_clone_repo
{
    public class SlackCloneDBContext : DbContext
    {
        //public SlackCloneDBContext(DbContextOptions<SlackCloneDBContext> options)
        //    : base(options)
        //{ }

        public SlackCloneDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SlackData> SlackData { get; set; }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<SlackCloneDBContext>
    {
        public SlackCloneDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder();

            builder.UseSqlServer("Data Source = habhishekiw; Initial Catalog = SlackClone; Integrated Security = True; MultipleActiveResultSets = True",
            //builder.UseSqlServer("Data Source=iwbotapidbserver.database.windows.net,1433;Initial Catalog=IWBotAPI_db;Persist Security Info=False;MultipleActiveResultSets=False; User ID=IWBOT;Password=InsightWorkshop@#001",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(SlackCloneDBContext).GetTypeInfo().Assembly.GetName().Name));
            return new SlackCloneDBContext(builder.Options);
        }
    }
}