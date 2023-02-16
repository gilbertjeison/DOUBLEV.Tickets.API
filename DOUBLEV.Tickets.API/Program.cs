using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

CreateDBContext<DataAccess.Common.Interfaces.IMainContext, DataAccess.Common.TicketsContext>
    (builder.Services, configuration.GetConnectionString("ConnectionString") ?? "");

builder.Services.AddTransient<DataAccess.Interfaces.ITicketDao, DataAccess.Dao.TicketDao>();
builder.Services.AddTransient<BusinessRules.Interfaces.ITicket, BusinessRules.BusinessRules.Tickect>();

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


static void CreateDBContext<TService, TContext>(IServiceCollection services, string conection)
                   where TContext : DbContext, TService
                   where TService : class
{
    services.AddDbContext<TContext>(options =>
                                          options
                                          .UseSqlServer(conection, opts =>
                                          {
                                              opts.CommandTimeout((int)TimeSpan.FromMinutes(8).TotalSeconds);
                                          })
                                          .UseLazyLoadingProxies(true));


    services.AddScoped<TService>(provider => provider.GetService<TContext>());
}
