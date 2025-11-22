using FacebookClone.Data.Entities;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

public class FriendRepository : IFriendsRepository
{
    private readonly AppDb _appDb;

    public FriendRepository(AppDb appDb)
    {
        _appDb = appDb;
    }

    public async Task<Friendship> AcceptFriend(Friendship friendRequest)
    {
        await _appDb.friendShips.AddAsync(friendRequest);
        await _appDb.SaveChangesAsync();
        return friendRequest;
    }

    public async Task<FriendRequest> SendFriendRequest(FriendRequest friendRequest)
    {
        await _appDb.friendRequests.AddAsync(friendRequest);
        await _appDb.SaveChangesAsync();
        return friendRequest;
    }

    public async Task<FriendRequest> GetFriendRequest(string senderId, string receiverId)
    {
        return await _appDb.friendRequests
            .FirstOrDefaultAsync(x => x.SenderId == senderId && x.ReceiverId == receiverId);
    }
    public async Task DeleteFriendshipEntity(Friendship friendship)
    {
        _appDb.friendShips.Remove(friendship);
        await _appDb.SaveChangesAsync();
    }


    public async Task UpdateFriendRequest(FriendRequest friendRequest)
    {
        _appDb.friendRequests.Update(friendRequest);
        await _appDb.SaveChangesAsync();
    }

    public async Task<string> RemoveFriendShip(string userId, string friendId)
    {
        var friendship = await _appDb.friendShips
            .FirstOrDefaultAsync(f =>
                (f.UserId == userId && f.FriendId == friendId) ||
                (f.UserId == friendId && f.FriendId == userId));

        if (friendship == null)
            throw new Exception("Friendship not found");

        _appDb.friendShips.Remove(friendship);
        await _appDb.SaveChangesAsync();

        return "Removed";
    }



    public async Task<Friendship> GetFriendShip(string userId, string friendId)
    {
        return await _appDb.friendShips
            .FirstOrDefaultAsync(x => x.UserId == userId && x.FriendId == friendId);
    }

    public Task RemoveFriendRequests(string userId)
    {
        var requests = _appDb.friendRequests
            .Where(x => x.SenderId == userId || x.ReceiverId == userId);

        _appDb.friendRequests.RemoveRange(requests);
        return _appDb.SaveChangesAsync();
    }

    public async Task<List<string>> getAllFriends(string userId)
    {
        return await _appDb.friendShips
            .Where(x => x.UserId == userId || x.FriendId == userId)
            .Select(x => x.UserId == userId ? x.FriendId : x.UserId)
            .ToListAsync();
    }
}
