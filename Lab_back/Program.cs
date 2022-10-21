// начальные данные
using Lab_back.Entities;

List<User> users = new List<User>
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom" },
    new() { Id = Guid.NewGuid().ToString(), Name = "Bob" },
    new() { Id = Guid.NewGuid().ToString(), Name = "Sam" }
};

List<Category> categories = new List<Category>
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Fruits" },
    new() { Id = Guid.NewGuid().ToString(), Name = "Cars" },
    new() { Id = Guid.NewGuid().ToString(), Name = "Snacks" }
};

List<Record> records = new List<Record>
{
    new() { Id = Guid.NewGuid().ToString(), UserId=Guid.NewGuid().ToString(), CategoryId=Guid.NewGuid().ToString(), 
        Created=new DateTime(2022,10,12,12,13,25), Sum=12.135 },
    new() { Id = Guid.NewGuid().ToString(), UserId=Guid.NewGuid().ToString(), CategoryId=Guid.NewGuid().ToString(), 
        Created=new DateTime(2021,4,21,14,23,34), Sum=1442.15  },
    new() { Id = Guid.NewGuid().ToString(), UserId=Guid.NewGuid().ToString(), CategoryId=Guid.NewGuid().ToString(), 
        Created=new DateTime(2022,1,19,18,4,5), Sum=1244.45  }
};



var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", () => users);
app.MapGet("/api/categories", () => categories);
app.MapGet("/api/records", () => records);

app.MapGet("/api/users/{id}", (string id) =>
{
    User? user = users.FirstOrDefault(u => u.Id == id);
    if (user == null) return Results.NotFound(new { message = "User didn't find" });

    return Results.Json(user);
});
app.MapGet("/api/categories/{id}", (string id) =>
{
    Category? category = categories.FirstOrDefault(c => c.Id == id);
    if (category == null) return Results.NotFound(new { message = "Category didn't find" });

    return Results.Json(category);
});
app.MapGet("/api/records/{id}", (string id) =>
{
    Record? record = records.FirstOrDefault(c => c.Id == id);
    if (record == null) return Results.NotFound(new { message = "Record didn't find" });

    return Results.Json(record);
});

app.MapDelete("/api/users/{id}", (string id) =>
{
    User? user = users.FirstOrDefault(u => u.Id == id);

    if (user == null) return Results.NotFound(new { message = "User didn't find" });

    users.Remove(user);
    return Results.Json(user);
});
app.MapDelete("/api/categories/{id}", (string id) =>
{
    Category? category = categories.FirstOrDefault(u => u.Id == id);

    if (category == null) return Results.NotFound(new { message = "Category didn't find" });

    categories.Remove(category);
    return Results.Json(category);
});
app.MapDelete("/api/records/{id}", (string id) =>
{
    Record? record = records.FirstOrDefault(u => u.Id == id);

    if (record == null) return Results.NotFound(new { message = "Record didn't find" });

    records.Remove(record);
    return Results.Json(record);
});


app.MapPost("/api/users", (User user) => {

    user.Id = Guid.NewGuid().ToString();
    users.Add(user);
    return user;
});
app.MapPost("/api/categories", (Category category) => {

    category.Id = Guid.NewGuid().ToString();
    categories.Add(category);
    return category;
});
app.MapPost("/api/records", (Record record) => {

    record.Id = Guid.NewGuid().ToString();
    records.Add(record);
    return record;
});

app.MapPut("/api/users", (User userData) => {

    var user = users.FirstOrDefault(u => u.Id == userData.Id);
    if (user == null) return Results.NotFound(new { message = "User didn't find" });

    user.Name = userData.Name;
    return Results.Json(user);
});
app.MapPut("/api/categories", (Category categoryData) => {

    var category = categories.FirstOrDefault(u => u.Id == categoryData.Id);
    if (category == null) return Results.NotFound(new { message = "User didn't find" });

    category.Name = categoryData.Name;
    return Results.Json(category);
});
app.MapPut("/api/records", (Record recordData) => {

    var record = records.FirstOrDefault(u => u.Id == recordData.Id);
    if (record == null) return Results.NotFound(new { message = "Record didn't find" });

    record.Created = recordData.Created;
    record.Sum = recordData.Sum;
    return Results.Json(record);
});

app.Run();