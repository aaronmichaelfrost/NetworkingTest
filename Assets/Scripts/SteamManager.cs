﻿
using UnityEngine;
using Steamworks;
using System.Net;


public class SteamManager : MonoBehaviour
{

    public static SteamManager Instance;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        InitClient();

        DontDestroyOnLoad(this.gameObject);
    }


    void InitClient()
    {
        // Initialize steam client
        try
        {
            SteamClient.Init(1551700, true);

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log("Could not initialize steam client. Is steam not open?");
        }
    }


    public void InitServer()
    {
        SteamServerInit init = new SteamServerInit("ModDir", "Game Description")
        {
            Secure = true,
            DedicatedServer = true,
            IpAddress = IPAddress.Any,
            SteamPort = 27015,
            GameDescription = "A Long Road From Home",
            ModDir = "NetworkingTest",
            VersionString = "0.0.0.0",
            GamePort = 28015,
            QueryPort = 28016
            
        };

        try
        {
            SteamServer.Init(1551700, init, true);
        }
        catch (System.Exception)
        {
            Debug.Log("Could not initialize steam server. Is steam not open?");
        }
    }



    public bool refreshServerList = false;

    // Updates server list when refreshServerList is set to true through inspector
    private async void Update()
    {

        


        if (refreshServerList)
        {
            using (var list = new Steamworks.ServerList.Internet())
            {
                // list.AddFilter("map", "de_dust");
                await list.RunQueryAsync();

                foreach (var server in list.Responsive)
                {
                    Debug.Log($"{server.Address} {server.Name}");
                }

                Debug.Log("Found " + list.Responsive.Count + " internet servers.");
            }


            using (var list = new Steamworks.ServerList.LocalNetwork())
            {
                // list.AddFilter("map", "de_dust");
                await list.RunQueryAsync();

                foreach (var server in list.Responsive)
                {
                    Debug.Log($"{server.Address} {server.Name}");
                }

                Debug.Log("Found " + list.Responsive.Count + " local network servers.");
            }

            refreshServerList = false;
        }


    }



}
