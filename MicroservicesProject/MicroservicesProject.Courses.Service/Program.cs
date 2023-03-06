using MicroservicesProject.Courses.Service.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesProject.Courses.Service
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddCourseDb();
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var connectionString = "Server=127.0.0.1;Port=3306;Database=microservices;User Id=main;Password=main";

			builder.Services.AddDbContext<CourseDbContext>(optionsBuilder =>
			{
				optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();


			app.MapControllers();

			//migrate to initial state
			var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<CourseDbContext>>();
			var dbContext = dbContextFactory.CreateDbContext();
			dbContext.Database.Migrate();

			app.Run();
		}
	}
}