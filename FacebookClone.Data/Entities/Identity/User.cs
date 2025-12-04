using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Data.Entities.Identity
{
 
        public class User : IdentityUser
        {
            public string? ProfilePictureUrl { get; set; } = string.Empty;
            public string? Bio { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public bool IsBanned { get; set; }
            public string? BanReason { get; set; }
            public DateTime? BannedAt { get; set; }

           public ICollection<Post> Posts { get; set; } = new List<Post>();
            public ICollection<Comment> Comments { get; set; } = new List<Comment>();
            public ICollection<Like> Likes { get; set; } = new List<Like>();

            public ICollection<FriendRequest> SendRequests { get; set; } = new List<FriendRequest>();
            public ICollection<FriendRequest> ReceiveRequests { get; set; } = new List<FriendRequest>();

            public ICollection<Friendship> Friendships { get; set; } = new List<Friendship>();
            public ICollection<Friendship> FriendsOf { get; set; } = new List<Friendship>();
        }
    

}
