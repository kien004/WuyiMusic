using Microsoft.EntityFrameworkCore;
using WuyiDAL.IReponsitory;
using WuyiDAL.Repository;
using WuyiDAL.Models;
using WuyiServices.IServices;
using WuyiServices.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Data Source=DESKTOP-77H263D\\SQLEXPRESS;Initial Catalog=DB_WuyiMusic2;Integrated Security=True;Trust Server Certificate=True"));

// Đăng ký Repository và Service
builder.Services.AddScoped(typeof(IAllReponsitories<User>), typeof(AllReponsitories<User>));
builder.Services.AddScoped(typeof(IServices<User>), typeof(Services<User>));
builder.Services.AddScoped(typeof(IAllReponsitories<Album>), typeof(AllReponsitories<Album>));
builder.Services.AddScoped(typeof(IServices<Album>), typeof(Services<Album>));
builder.Services.AddScoped(typeof(IAllReponsitories<Song>), typeof(AllReponsitories<Song>));
builder.Services.AddScoped(typeof(IServices<Song>), typeof(Services<Song>));
builder.Services.AddScoped(typeof(IAllReponsitories<UserFollowArtist>), typeof(AllReponsitories<UserFollowArtist>));
builder.Services.AddScoped(typeof(IServices<UserFollowArtist>), typeof(Services<UserFollowArtist>));
builder.Services.AddScoped(typeof(IAllReponsitories<Genre>), typeof(AllReponsitories<Genre>));
builder.Services.AddScoped(typeof(IServices<Genre>), typeof(Services<Genre>));
builder.Services.AddScoped(typeof(IAllReponsitories<Notification>), typeof(AllReponsitories<Notification>));
builder.Services.AddScoped(typeof(IServices<Notification>), typeof(Services<Notification>));
builder.Services.AddScoped(typeof(IAllReponsitories<Artist>), typeof(AllReponsitories<Artist>));
builder.Services.AddScoped(typeof(IServices<Artist>), typeof(Services<Artist>));
builder.Services.AddScoped(typeof(IAllReponsitories<Playlist>), typeof(AllReponsitories<Playlist>));
builder.Services.AddScoped(typeof(IServices<Playlist>), typeof(Services<Playlist>));
builder.Services.AddScoped(typeof(IAllReponsitories<PlaylistSong>), typeof(AllReponsitories<PlaylistSong>));
builder.Services.AddScoped(typeof(IServices<PlaylistSong>), typeof(Services<PlaylistSong>));
builder.Services.AddScoped(typeof(IAllReponsitories<UserLikedSong>), typeof(AllReponsitories<UserLikedSong>));
builder.Services.AddScoped(typeof(IServices<UserLikedSong>), typeof(Services<UserLikedSong>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();
