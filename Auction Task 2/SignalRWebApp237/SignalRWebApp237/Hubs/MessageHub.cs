using Microsoft.AspNetCore.SignalR;
using SignalRWebApp237.Services;
using System.Collections.Concurrent;

namespace SignalRWebApp237.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IFileService _fileService;
        private static Dictionary<string, List<string>> _rooms = new Dictionary<string, List<string>>();
        public MessageHub(IFileService fileService)
        {
            _fileService = fileService;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveConnectInfo", "User Connected");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Others.SendAsync("ReceiveDisconnectInfo", "User Disconnected");
        }

        public async Task SendMessage(string message, double data)
        {
            await Clients.All.SendAsync("ReceiveMessage", message + "'s Offer is ", data);
        }

        public async Task JoinRoom(string room, string user)
        {
            if (_rooms.ContainsKey(room) && _rooms[room].Count >= 3)
            {
                await Clients.Caller.SendAsync("RoomFull", "The room is full");
                return;
            }

            if (!_rooms.ContainsKey(room))
            {
                _rooms[room] = new List<string>();
            }

            _rooms[room].Add(user);
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.OthersInGroup(room).SendAsync("ReceiveJoinInfo", user);
        }

        public async Task LeaveRoom(string room, string user)
        {
            if (_rooms.ContainsKey(room))
            {
                _rooms[room].Remove(user);
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
            await Clients.OthersInGroup(room).SendAsync("UserLeft", user);
        }

        public async Task SendMessageRoom(string room, string user)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveInfoRoom", user, await _fileService.Read(room));
        }
    }
}
