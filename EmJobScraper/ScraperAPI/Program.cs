using OpenQA.Selenium.Chrome;
using ScraperAPI.Controllers;
using ScraperAPI.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<PageGenerator>();
        builder.Services.AddCors();
        //builder.Services.AddTransient<NSWService>();
        //builder.Services.AddTransient<ACTService>();
        //builder.Services.AddTransient<QLDService>();
        //builder.Services.AddTransient<SAService>();
        //builder.Services.AddTransient<TASService>();
        //builder.Services.AddTransient<VICService>();
        //builder.Services.AddTransient<WAService>();

        ChromeOptions opts = new ChromeOptions();
        opts.AddArgument("--headless");
        opts.AddArgument("--no-sandbox");
        opts.AddArgument("--disable-gpu");
        opts.AddArgument("--disable-dev-shm-usage");
        opts.AddArgument("--window-size=1920,1080");

        ChromeDriverService chromeService = ChromeDriverService.CreateDefaultService();
        chromeService.SuppressInitialDiagnosticInformation = true;

        var driver = new ChromeDriver(chromeService, opts);

        builder.Services.AddSingleton(driver);

        builder.Services.AddSignalR();

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
        app.UseCors(x =>
        x.AllowAnyHeader()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true));

        app.MapHub<JobHub>("jobhub");

        app.Run();
    }
}