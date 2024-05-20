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

app.UseCors(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
});

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

// Middleware để đọc dữ liệu từ yêu cầu và chuyển đổi thành đối tượng C#
app.Use(async (context, next) =>
{
    // Đảm bảo rằng yêu cầu có thể đọc lại nếu cần thiết
    context.Request.EnableBuffering();

    using (var reader = new StreamReader(context.Request.Body))
    {
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0; // Đặt lại vị trí của luồng để đọc lại nếu cần thiết
                                           // Lưu trữ dữ liệu trong context để các action sau có thể truy cập
        context.Items["RequestBody"] = body;
    }

    // Chuyển quyền kiểm soát sang middleware tiếp theo trong pipeline
    await next();
});

app.Run();
