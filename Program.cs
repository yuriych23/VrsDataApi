using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using VrsDataApi.DAL.Concrete;
using VrsDataApi.DAL.Abstract;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins(
                            "http://localhost:8080",
                            "https://icy-dune-02041e603.1.azurestaticapps.net")
                            .WithMethods("GET", "POST", "DELETE", "PUT");
                      });
});

var secretClient = new SecretClient(new Uri("https://vrs-data.vault.azure.net/"), new DefaultAzureCredential());
KeyVaultSecret secret = secretClient.GetSecret("CosmosPK");

builder.Services.AddDbContext<VrsDataApi.DAL.VrsDataDbContext>(
        options => options.UseCosmos(
            "https://vrs-data-db.documents.azure.com:443/",
            secret.Value,
            "VrsLogDb", o => {
                o.MaxRequestsPerTcpConnection(3);
                o.MaxTcpConnectionsPerEndpoint(3);
                o.RequestTimeout(TimeSpan.FromSeconds(10));
            }));

builder.Services.AddScoped<IVrsLogRepository, VrsLogRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
