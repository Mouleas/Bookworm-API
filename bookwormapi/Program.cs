using bookwormapi.Data;
using Microsoft.EntityFrameworkCore;

namespace bookwormapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BookwormContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookwormApiContext") ?? throw new InvalidOperationException("Connection string 'BookwormApiContext' not found.")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                      policy =>
                                      {
                                          policy.SetIsOriginAllowed(_ => true)
                                                              .AllowAnyHeader()
                                                              .AllowAnyMethod();
                                      });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(MyAllowSpecificOrigins);
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}