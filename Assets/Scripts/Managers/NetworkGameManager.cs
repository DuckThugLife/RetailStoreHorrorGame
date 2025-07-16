using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Lobbies;
using Unity.Services.Relay.Models;
using Unity.Services.Lobbies.Models;
using System.Threading.Tasks;
using TMPro;
using Unity.Networking.Transport.Relay;

public class NetworkGameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField joinCodeInput;
    [SerializeField] TMP_Text statusText;
    [SerializeField] Button hostBtn, joinBtn;

    [SerializeField] private int maxPlayers;

    async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        statusText.text = $"Signed in: {AuthenticationService.Instance.PlayerId}";

    }

    public async void HostGame()
    {
        try
        {
            var alloc = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(alloc.AllocationId);

            var lobby = await LobbyService.Instance.CreateLobbyAsync(
                "Lobby", 4, new CreateLobbyOptions
                {
                    Data = new System.Collections.Generic.Dictionary<string, DataObject>
                    {
                        ["joinCode"] = new DataObject(DataObject.VisibilityOptions.Public, joinCode)
                    }
                }
            );

            SetupRelay(AllocationUtils.ToRelayServerData(alloc, "dtls"));
            NetworkManager.Singleton.StartHost();
            statusText.text = "Hosting | Code: " + joinCode;
        }
        catch (System.Exception e)
        {
            statusText.text = "Host failed: " + e.Message;
        }
    }

    public async void JoinGame()
    {
        try
        {
            string code = joinCodeInput.text.Trim();
            var alloc = await RelayService.Instance.JoinAllocationAsync(code);

            SetupRelay(AllocationUtils.ToRelayServerData(alloc, "dtls"));
            NetworkManager.Singleton.StartClient();
            statusText.text = "Joined lobby.";
        }
        catch (System.Exception e)
        {
            statusText.text = "Join failed: " + e.Message;
        }
    }

    void SetupRelay(RelayServerData relayData)
    {
        var utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.SetRelayServerData(relayData);
    }
}
