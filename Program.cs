var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

// Thêm middleware để đọc dữ liệu từ yêu cầu và chuyển đổi nó thành đối tượng C#
app.Use((context, next) =>
{
    context.Request.EnableBuffering(); // Cho phép đọc lại yêu cầu
    return next();
});

app.Run();
