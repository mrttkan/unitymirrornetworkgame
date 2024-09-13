using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Mirror.Discovery;

public class NetworkUI : MonoBehaviour
{
    public Button hostButton;
    public Button joinButton;
    public Button stopButton; // Stop button

    private NetworkManager networkManager;
    private NetworkDiscovery networkDiscovery;

    void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();
        networkDiscovery = FindObjectOfType<NetworkDiscovery>();

        hostButton.onClick.AddListener(StartHosting);
        joinButton.onClick.AddListener(StartClientDiscovery);
        stopButton.onClick.AddListener(StopNetwork); // Stop button listener
    }

    public void StartHosting()
    {
        networkManager.StartHost();
        networkDiscovery.AdvertiseServer();
    }

    public void StartClientDiscovery()
    {
        networkDiscovery.StartDiscovery();
        networkDiscovery.OnServerFound.AddListener(StartClient);
    }

    private void StartClient(ServerResponse info)
    {
        networkManager.StartClient(info.uri);
    }

    public void StopNetwork()
    {
        if (networkManager.isNetworkActive)
        {
            networkManager.StopHost();
            networkManager.StopClient();
            networkManager.StopServer();
        }
    }
}
