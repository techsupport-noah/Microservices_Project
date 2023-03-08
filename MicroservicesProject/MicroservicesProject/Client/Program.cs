using MicroservicesProject.App.Course.ApiClient;
using MicroservicesProject.App.Event.ApiClient;
using MicroservicesProject.App.Invitation.ApiClient;
using MicroservicesProject.App.Student.ApiClient;
using MicroservicesProject.App.User.ApiClient;
using MicroservicesProject.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MicroservicesProject.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

			//get configs
			var coursesApiUrl = builder.Configuration["CoursesApiUrl"];
			var eventsApiUrl = builder.Configuration["EventsApiUrl"];
			var invitationsApiUrl = builder.Configuration["InvitationsApiUrl"];
			var studentsApiUrl = builder.Configuration["StudentsApiUrl"];
			var usersApiUrl = builder.Configuration["UsersApiUrl"];

            //http registration
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //api class registrations
            builder.Services.AddScoped<CourseApiClient>(provider =>
			{
				var httpClient = provider.GetRequiredService<HttpClient>();
				return new CourseApiClient(coursesApiUrl, httpClient);
			});
			builder.Services.AddScoped<EventApiClient>(provider =>
			{
				var httpClient = provider.GetRequiredService<HttpClient>();
				return new EventApiClient(eventsApiUrl, httpClient);
			});
			builder.Services.AddScoped<InvitationApiClient>(provider =>
			{
				var httpClient = provider.GetRequiredService<HttpClient>();
				return new InvitationApiClient(invitationsApiUrl, httpClient);
			});
			builder.Services.AddScoped<StudentApiClient>(provider =>
			{
				var httpClient = provider.GetRequiredService<HttpClient>();
				return new StudentApiClient(studentsApiUrl, httpClient);
			});
			builder.Services.AddScoped<UserApiClient>(provider =>
			{
				var httpClient = provider.GetRequiredService<HttpClient>();
				return new UserApiClient(usersApiUrl, httpClient);
			});


			await builder.Build().RunAsync();
        }
    }
}