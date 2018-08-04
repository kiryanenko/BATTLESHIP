using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    public UnityEvent StartLocalPlayerEvent;

    public GameObject LocalPlayer { get; private set; }

    public static CustomNetworkManager Instance
    {
        get { return (CustomNetworkManager) singleton; }
    }

    public void OnStartLocalPlayer(GameObject player)
    {
        LocalPlayer = player;
        StartLocalPlayerEvent.Invoke();
    }
}
