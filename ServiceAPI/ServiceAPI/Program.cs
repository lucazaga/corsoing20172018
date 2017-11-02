using Microsoft.AspNetCore.Hosting;
using ServiceAPI.Dal;
using System;
using System.Threading.Tasks;

namespace ServiceAPI
{
    class Program
    {
        static void Main(string[] args)
        {

            var host = new WebHostBuilder()
               .UseKestrel()
               .UseStartup<Startup>()
               .Build();

            Task restService = host.RunAsync();


            //using (var context = new StudentsDbContext())
            //{
            //    // Create database
            //    context.Database.EnsureCreated();

            //    Student s = new Student()
            //    {
            //        Name = "Giovanni",
            //        DateOfBirth = new DateTime(2012, 1, 1),
            //    };

            //    context.Students.Add(s);

            //    context.SaveChanges();
            //}


            //System.Diagnostics.Process.Start("chrome.exe", "http://localhost/netcoreapp2.0/corsoing/");
            //System.Diagnostics.Process.Start("cmd", "/C start http://localhost/netcoreapp2.0/corsoing/");
            restService.Wait();
        }
    }
}
