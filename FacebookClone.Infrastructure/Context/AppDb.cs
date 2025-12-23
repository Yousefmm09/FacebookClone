using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Context
{
    public class AppDb:IdentityDbContext<User>
    {
        public AppDb()
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostMedia> PostMedia { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<FriendRequest> friendRequests { get; set; }
        public DbSet<Friendship> friendShips { get; set; }
        public DbSet<UserRefreshToken> userRefreshToken { get; set; }
        public DbSet<PostsShare> postsShares { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<OtpEmail> OtpEmails { get; set; }
        
        public AppDb(DbContextOptions<AppDb> options): base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Friendship>()
                 .HasOne(f => f.User)
                .WithMany(u => u.Friendships)
                 .HasForeignKey(f => f.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany(u => u.FriendsOf)
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FriendRequest>()
                       .HasOne(fr => fr.Sender)
                        .WithMany(u => u.SendRequests)
    .HasForeignKey(fr => fr.SenderId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Receiver)
                .WithMany(u => u.ReceiveRequests)
                .HasForeignKey(fr => fr.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
         
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);
       


    }


}
}
