// начальные данные
using Lab_back.Entities;

List<User> users = new List<User>
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom" },
    new() { Id = Guid.NewGuid().ToString(), Name = "Bob" },
    new() { Id = Guid.NewGuid().ToString(), Name = "Sam" }
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", () => users);

app.MapGet("/api/users/{id}", (string id) =>
{
    User? user = users.FirstOrDefault(u => u.Id == id);
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (string id) =>
{
    User? user = users.FirstOrDefault(u => u.Id == id);

    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

    users.Remove(user);
    return Results.Json(user);
});

app.MapPost("/api/users", (User user) => {

    user.Id = Guid.NewGuid().ToString();
    users.Add(user);
    return user;
});

app.MapPut("/api/users", (User userData) => {

    var user = users.FirstOrDefault(u => u.Id == userData.Id);
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

    user.Name = userData.Name;
    return Results.Json(user);
});

app.Run();