using AuctionService.Consumers;
using AuctionService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(opt =>
{
    opt.AddEntityFrameworkOutbox<AuctionDbContext>(cfg =>
    {
        cfg.QueryDelay = TimeSpan.FromMilliseconds(10);
        cfg.UsePostgres();
    });
    
    opt.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();
    opt.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));
    
    opt.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

 if (app.Environment.IsDevelopment())
{
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//NOTE: should be inside the development
try
{
    DbInitializer.InitDb(app);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

app.Run();
