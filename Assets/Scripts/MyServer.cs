using UnityEngine;
using Steamworks;
using Steamworks.Data;
using System;

public class MyServer : SocketManager
{
	MyServer server;

	public void CreateServer()
    {
		server = SteamNetworkingSockets.CreateNormalSocket<MyServer>(NetAddress.AnyIp(21893));
	}


	public override void OnConnecting(Connection connection, ConnectionInfo data)
	{
		connection.Accept();
		Debug.Log(data.Identity + " is connecting");
	}

	public override void OnConnected(Connection connection, ConnectionInfo data)
	{
		Debug.Log(data.Identity + " has joined the game");
	}

	public override void OnDisconnected(Connection connection, ConnectionInfo data)
	{
		Debug.Log(data.Identity + " has left the game");
	}

	public override void OnMessage(Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel)
	{
		Debug.Log(identity + " sent us a message!");

		// Send it right back
		connection.SendMessage(data, size, SendType.Reliable);
	}

}
