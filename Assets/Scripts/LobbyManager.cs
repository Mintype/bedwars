using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private const string GameVersion = "1";
    /*private const*/public int MaxPlayers = 2;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = GameVersion;
    }

    public override void OnConnectedToMaster()
    {

    }

    public void OnPlayButtonClicked()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("GameRoom", new RoomOptions { MaxPlayers = MaxPlayers }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        CheckPlayersInRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        CheckPlayersInRoom();
    }

    private void CheckPlayersInRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;  // close room
            PhotonNetwork.CurrentRoom.IsVisible = false;  // hide room
            StartGame();
        }
    }

    private void StartGame()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
