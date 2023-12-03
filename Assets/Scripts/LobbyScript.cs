using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyScript : MonoBehaviour
{
    private Lobby hostLobby;

    private float heartbeatTimer;

    private string RandomPlayerName;
    
    async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Successfully signed in with ID : " + AuthenticationService.Instance.PlayerId);
        };
        RandomPlayerName = "TestUser" + UnityEngine.Random.Range(10, 99);
        Debug.Log("PlayerName " + RandomPlayerName);
        await AuthenticationService.Instance.SignInAnonymouslyAsync(); // SignIn Anonymously for now
    }

    
    void Update()
    {
        HandleLobbyHeartBeat();
    }

    private async void HandleLobbyHeartBeat()
    {
        if (hostLobby != null)
        {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer < 0.0f)
            {
                float heartbeatTimerMax = 15.0f;
                heartbeatTimer = heartbeatTimerMax;
                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }

    private async void CreateLobby()
    {
        try
        {
            string lobbyName = "My Lobby";
            int maxPlayers = 4;
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = true,
                Player = new Player
                {
                    //Id = AuthenticationService.Instance.PlayerId,
                    Data = new Dictionary<string, PlayerDataObject>
                    {
                        { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, RandomPlayerName) }
                    }
                }
            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, createLobbyOptions);
            hostLobby = lobby;
            Debug.Log("Successfully created a lobby with name " + lobbyName + "!" + "Lobby Code is " + lobby.LobbyCode);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e.Message);
        }
    }
    private async void ListAllLobbies()
    {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();
            Debug.Log("Found " + queryResponse.Results.Count + " Lobbies");
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private async void SearchForLobbyByAvailableSlots()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 10,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);
            Debug.Log("Found " + queryResponse.Results.Count + " Lobbies");
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, RandomPlayerName) }
            }
        };
    }
    
    private async void JoinLobbyByCode(string LobbyCode)
    {
        try
        {
            JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer(),
            };
            
            Lobby joinedLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(LobbyCode, joinLobbyByCodeOptions);
            DebugPrintPlayers(joinedLobby);
            Debug.Log("Joined Lobby with code " + LobbyCode);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private async void QuickJoinLobby()
    {
        try
        {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private void DebugPrintPlayers(Lobby lobby)
    {
        Debug.Log("--- Players in Lobby " + lobby.Name + "---");
        foreach (Player player in lobby.Players)
        {
            Debug.Log(player.Id + " " + player.Data["PlayerName"].Value);
        }
    }
}
