// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddControllersWithViews();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();

// app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// app.Run();

using Azure.Messaging.ServiceBus.Administration;

ServiceBusAdministrationClient client = new ServiceBusAdministrationClient("Endpoint=sb://ventilation-enhancementcndly.servicebus.chinacloudapi.cn/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=g8zIkpCxoIpUPFX5Lplw0mybFTBPv4zMBp6AhgdbBuQ=");
var response = await client.CreateSubscriptionAsync(
    new CreateSubscriptionOptions("sendreminder", "test"),
    new CreateRuleOptions()
    {
        Name = "EventSqlRule",
        Filter = new SqlRuleFilter("Color = 'Blue'"),
        Action = new SqlRuleAction("SET Color = 'BlueProcessed'")
    });

Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(response.Value));