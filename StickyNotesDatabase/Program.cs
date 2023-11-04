using Microsoft.EntityFrameworkCore;
using StickyNotesDatabase.Dtos;
using StickyNotesDatabase.Models;
using StickyNotesDatabase.Services;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<StickyNotesContext>(opts => 
    opts.UseMySql(connString, ServerVersion.AutoDetect(connString))
    );

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddAntiforgery();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<INotesService, NotesService>();

var app = builder.Build();

app.UseAntiforgery();
app.UseCors(
  options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/RandomNote", (INotesService service, string? username) => {
    return Results.Ok(service.GetRandomNote(username));
});
app.MapPost("/GetNote", (INotesService service, string author) => {
    return Results.Ok(service.GetNote(author));
});
app.MapGet("/FrontRandomNote", (INotesService service) => {
    return Results.Ok(service.GetFrontRandomNotes(null));
});
app.MapGet("/GetBestNotes", (INotesService service, string? username) => {
    return Results.Ok(service.GetBestNotes(username));
});
app.MapGet("/GetRecentNotes", (INotesService service, string? username) => {
    return Results.Ok(service.GetRecentNotes(username));
});
app.MapPost("/Vote", (INotesService services, string author, string username) =>
{
    return Results.Ok(services.AddVote(author, username));
});
app.MapPost("/AddNote", (INotesService service, StickyNoteDto note) =>
{
    var result = service.PostNote(note);
    return Results.Ok(result);
});

app.MapPost("/FindUser", (IUserService service, User dto) =>
{
    var answer = service.CheckForUser(dto);
    return Results.Ok(answer == "Nima" ? "NotExists" : "Exists");
});
app.MapPost("/AddUser", (IUserService service, User dto) =>
{
    var answer = service.CreateUser(dto);
    return answer == "Added" ? Results.Ok("Added") : Results.Ok("Exists");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Run();
