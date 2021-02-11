using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SteamManager : MonoBehaviour
{
    private void Awake()
    {
        try
        {
            Steamworks.SteamClient.Init(1551700, true);

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log("Could not initialize steam client. Is steam not open?");
        }

        DontDestroyOnLoad(this.gameObject);

    }


    public bool refreshServerList = false;

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
            }

            refreshServerList = false;
        }


    }
}
