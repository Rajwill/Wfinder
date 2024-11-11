using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the controller.
        // Add Json functionality for the json file processing

        builder.Services.AddControllers().AddNewtonsoftJson(options =>
                        {
                            options.SerializerSettings.Formatting = Formatting.Indented;
                            options.SerializerSettings.TypeNameHandling = TypeNameHandling.Objects;
                            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                            options.SerializerSettings.Converters.Add(new StringEnumConverter());
                            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        });
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

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}