using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StickyNotesDatabase.Models;
using SticyNotesDatabase.Controllers;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var constr = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<StickyNotesContext>(opts => opts.UseSqlServer(constr));

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddAntiforgery();
builder.Services.AddTransient<UserController>();
builder.Services.AddTransient<NoteController>();

var app = builder.Build();

app.UseAntiforgery();
app.UseCors(
  options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/RandomNote", (NoteController service, string? Username) => {
    return Results.Ok(service.GetRandomNote(Username));
});
app.MapPost("/GetNote", (NoteController service, string Author) => {
    return Results.Ok(service.GetNote(Author));
});
app.MapGet("/FrontRandomNote", (NoteController service) => {
    return Results.Ok(service.GetFrontRandomNotes());
});
app.MapGet("/GetBestNotes", (NoteController service, string? Username) => {
    return Results.Ok(service.GetBestNotes(Username));
});
app.MapGet("/GetRecentNotes", (NoteController service, string? Username) => {
    return Results.Ok(service.GetRecentNotes(Username));
});
app.MapPost("/Vote", (NoteController services, string Author, string Username) =>
{
    return Results.Ok(services.AddVote(Author, Username));
});
app.MapPost("/AddNote", (NoteController service, StickyNoteDTO note) =>
{
    var Result = service.PostNote(note);
    if (Result == null)
    {
        return Results.Conflict();
    }
    return Results.Ok(Result);
});

app.MapPost("/FindUser", (UserController service, UserDTO dto) =>
{
    var answer = service.CheckForUser(dto);
    if (answer == "Nima")
    {
        return Results.Ok("NotExists");
    }
    return Results.Ok("Exists");
});
app.MapPost("/AddUser", (UserController service, UserDTO dto) =>
{
    var answer = service.CreateUser(dto);
    if (answer == "Added") { return Results.Ok("Added"); };
    return Results.Ok("Exists");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=Add}/{id?}");

app.Run();
