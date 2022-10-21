// начальные данные
using Lab_back.Entities;
char UserId = 'A';
char CategoryId = 'a';
int RecordId = 0;
List<User> users = new List<User>
{
    new() { Id = UserId++.ToString(), Name = "Tom" },
    new() { Id = UserId++.ToString(), Name = "Bob" },
    new() { Id = UserId++.ToString(), Name = "Sam" }
};

List<Category> categories = new List<Category>
{
    new() { Id = CategoryId++.ToString(), Name = "Fruits" },
    new() { Id = CategoryId++.ToString(), Name = "Cars" },
    new() { Id = CategoryId++.ToString(), Name = "Snacks" }
};

List<Record> records = new List<Record>
{
    new() { Id = RecordId++.ToString(), UserId="C", CategoryId="a", 
        Created=new DateTime(2022,10,12,12,13,00), Sum=12.13 },
    new() { Id = RecordId++.ToString(), UserId="B", CategoryId="b", 
        Created=new DateTime(2021,4,21,14,23,34), Sum=1442.15  },
    new() { Id = RecordId++.ToString(), UserId="A", CategoryId="c", 
        Created=new DateTime(2022,1,19,18,4,5), Sum=1244.45  }
};



var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", () => users);
app.MapGet("/api/categories", () => categories);
app.MapGet("/api/records", () => records);

app.MapGet("/api/users/{id}", (string Id) =>
{
    List<Record> resultRecords = records.FindAll(u => u.UserId.Equals(Id));
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (resultRecords == null) return Results.NotFound(new { message = "Records didn't find" });

    return Results.Json(resultRecords);
});

app.MapPost("/api/users", (User user) => {

    user.Id = UserId++.ToString();
    users.Add(user);
    return user;
});
app.MapPost("/api/categories", (Category category) => {

    category.Id = CategoryId++.ToString();
    categories.Add(category);
    return category;
});
app.MapPost("/api/records", (Record record) => {

    record.Id = RecordId++.ToString();
    record.Created = new(DateTime.Now.Year,
        DateTime.Now.Month,
        DateTime.Now.Day,
        DateTime.Now.Hour,
        DateTime.Now.Minute,
        DateTime.Now.Second);
    records.Add(record);
    return record;
});

app.Run();