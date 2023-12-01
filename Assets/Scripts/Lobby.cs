using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.VisualScripting;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    
    async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Successfully signed in with ID : " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync(); // SignIn Anonymously for now
    }

    
    void Update()
    {
        
    }

    private void CreateLobby()
    {
        try
        {
            string lobbyName = "My Lobby";
            int maxPlayers = 4;
            LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
            Debug.Log("Successfully created a lobby with name " + lobbyName + "!");
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e.Message);
        }
    }
}
