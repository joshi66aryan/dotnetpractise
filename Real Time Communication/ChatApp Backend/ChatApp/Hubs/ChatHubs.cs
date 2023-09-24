using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    [EnableCors("AllowLocalhost")]
    public class ChatHubs : Hub
	{

		// this method send notification all the clients.
		// if client have to communicate, it will call <SendMessage()> method.
		// if client have to recieve notification from server it will use <RecieveMessage()> method.

		public async Task SendMessage(string user, string sendMessage)
		{
			await Clients.All.SendAsync("RecieveMessage", user, sendMessage);
		}

		// every one will get notified except who have joined the chat.

		public async Task JoinChat( string user, string sendMessage)
		{
			await Clients.Others.SendAsync("RecieveMessage", user, sendMessage);
		}
		
	}
}

