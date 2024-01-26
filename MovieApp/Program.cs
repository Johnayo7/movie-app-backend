using movie.application.MovieApiIntegration.Implementation;
using movie.application.MovieApiIntegration.Interface;
using movie.application.Services.Implementation;
using movie.application.Services.Interface;

namespace MovieApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IMovieClient, MovieClient>();

            builder.Services.AddHttpClient();
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<ISearchService, SearchService>();
            builder.Services.AddScoped<ICacheHandler, CacheHandler>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                             // .WithOrigins("http://127.0.0.1:5173")
                             .AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            app.UseCors();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
                app.UseSwaggerUI();

           

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}