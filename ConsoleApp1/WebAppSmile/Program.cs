using WebAppSmile.Services;
using WebAppSmile.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<FlowerApiService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<MazeGameSessionStore>();

// Registered type IApiHelper
builder.Services.AddScoped<IApiHelper, ApiHelper>();
builder.Services.AddScoped<IMyJsonSerializer, MyJsonSerializer>();
builder.Services.AddScoped<IMazeSaveService, MazeSaveService>();

// builder.Services.AddTransient<IApiHelper, ApiHelper>(); // new each time
// builder.Services.AddScoped<IApiHelper, ApiHelper>(); // one per http request
// builder.Services.AddSingleton<IApiHelper, ApiHelper>(); // only one time

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
