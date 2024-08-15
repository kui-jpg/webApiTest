namespace webApiTest
{
    using MyDataBaseContext;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.SqlServer;
    
    using Model;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Logging.AddLog4Net("config/log4net.config");

            builder.Services.AddDbContext<MyDataBaseContext_main>(options => {
                options.UseMySql(builder.Configuration.GetConnectionString("MySqlDataBase"), new MySqlServerVersion(new Version(8, 0, 31)));
            });

            var app = builder.Build();
            app.UseCors("default");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();   
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
