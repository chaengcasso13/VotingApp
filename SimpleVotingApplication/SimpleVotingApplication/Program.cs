using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using SimpleVotingApplication.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// for the identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();
    

// might be a problem:
builder.Services.AddDbContextPool<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString")));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Login";
});


//// for the authentication --ERROR--
//builder.Services.AddMvc(options =>
//{
//    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//}).AddXmlDataContractSerializerFormatters();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
