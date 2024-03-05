using NumberToWords.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<INumToWordsRepository, NumToWordsRepository>();
////builder.Services.AddHttpClient<INumToWordsRepository, NumToWordsApiRepository>(client =>
////{
////    client.BaseAddress = new Uri("http://localhost:5258");
////});
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


app.Run();
