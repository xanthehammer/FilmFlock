using FilmFlock.Models;
using FilmFlock.Mongo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMongoDB();

builder.Services.AddScoped<IRoomIdGenerator, RoomIdGenerator>();
builder.Services.AddScoped<IRoomStorage, RoomMongoStorage>();
builder.Services.AddScoped<IUpvoteActivityStorage, UpdateActivityStorage>();
builder.Services.AddScoped<IRoomActivityCreating, RoomActivityCreator>();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowLocalhost3000",
            builder => builder.WithOrigins("http://localhost:3000")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials()
                              .SetIsOriginAllowed((host) => true));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowLocalhost3000");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
