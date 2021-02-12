
using UnityEngine;
using Steamworks;
using System.Collections;
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


        // InitClient();

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


    

    public void StopServer()
    {
        SteamServer.Shutdown();
    }

        

    public bool refreshServerList = false;

    public bool initClient = false;

    public bool debugIP = false;

    // Updates server list when refreshServerList is set to true through inspector
    private async void Update()
    {
        if (initClient)
        {
            InitClient();
            initClient = false;
        }


        if (debugIP)
        {
            Debug.Log(SteamServer.PublicIp);
            debugIP = false;
        }


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

            refreshServerList = false;
        }


    }



}
