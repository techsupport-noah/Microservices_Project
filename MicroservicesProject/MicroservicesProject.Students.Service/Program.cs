using FluentValidation;
using MicroservicesProject.Students.Service.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using MicroservicesProject.Students.Domain.Dto;
using MicroservicesProject.Students.Domain.Validations;
using MicroservicesProject.Students.Service.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MicroservicesProject.Students.Service
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAutoMapper(typeof(StudentProfile).Assembly);
			builder.Services.AddScoped<IValidator<StudentDetailsDto>, StudentValidator>();
			builder.Services.AddStudentDb();
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var connectionString = "Server=127.0.0.1;Port=3306;Database=microservices;User Id=main;Password=main";

			builder.Services.AddDbContext<StudentDbContext>(optionsBuilder =>
			{
				optionsBuilder.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
			});

			var authority = builder.Configuration["Identity:Authority"];
			var audience = builder.Configuration["Identity:Audience"];

			builder.Services
				//.AddTransient<IClaimsTransformation>(_ => new KeycloakRolesClaimsTransformer("roles", audience))
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options =>
				{
					options.Authority = authority;
					options.Audience = audience;
					options.RequireHttpsMetadata = false; //UNSECURE!!!
					options.TokenValidationParameters.ValidateIssuer = false;
					options.IncludeErrorDetails = true;
					options.Events.OnAuthenticationFailed += context =>
					{
						Console.WriteLine($"Authentication failed: {context?.Exception?.Message}");
						return Task.FromResult(0);
					};
					options.Events.OnForbidden += async context => Console.WriteLine($"Authentication forbidden");
					options.SaveToken = true;
					options.TokenValidationParameters.RoleClaimType = "roles";
					options.TokenValidationParameters.NameClaimType = "preferred_username";
				});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

			app.UseAuthorization();

			app.MapControllers();

			//migrate to initial state
			var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<StudentDbContext>>();
			var dbContext = dbContextFactory.CreateDbContext();
			dbContext.Database.Migrate();

			app.Run();
		}
	}
}