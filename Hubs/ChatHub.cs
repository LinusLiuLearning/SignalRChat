using Microsoft.AspNetCore.SignalR;
using SignalRChat.Models;

namespace SignalRChat.Hubs
{
    public class ChatHub:Hub
    {
        private ConUserService conUserList;
        public ChatHub(ConUserService conUserService)
        {
            conUserList = conUserService;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // 加入群組
        public async Task AddGroup(string groupName, string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            // 傳送加入群組訊息
            await Clients.Group(groupName).SendAsync("RecAddGroupMsg", $"{username} 已加入 群組：{groupName}。");
        }

        public Task SendMessageToGroup(string groupName, string username, string message)
        {
            return Clients.Group(groupName).SendAsync("ReceiveGroupMessage", groupName, username, message);
        }

        public async void AddConUserList(string user)
        {
            var _user = new ConUserModel();
            _user.username = user;
            _user.connectID = Context.ConnectionId;
            _user.onlineTime = DateTime.Now;

            await Groups.AddToGroupAsync(Context.ConnectionId, "TestGroup");
            await Clients.Group("TestGroup").SendAsync("GetConList", conUserList.AddList(_user));
        }

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    await base.OnDisconnectedAsync(exception);
        //    await Clients.Group("TestGroup").SendAsync("GetConList", conUserList.RemoveList(Context.ConnectionId));
        //}

        //public override async Task OnConnectedAsync()
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");

        //    await base.OnConnectedAsync();
        //}

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
        //    await base.OnDisconnectedAsync(exception);
        //}
    }
}
