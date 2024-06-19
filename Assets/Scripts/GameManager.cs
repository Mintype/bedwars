using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform[] spawnPoints;
    private bool[] occupiedSpawns;
    public Color[] colors;
    public string[] colors1;
    public List<Player> alivePlayers = new List<Player>();

    public PhotonView view;
    void Start()
    {
        if(view == null)
           view = this.gameObject.AddComponent<PhotonView>();
        occupiedSpawns = new bool[spawnPoints.Length];
        colors = new Color[]
            {
                Color.red,
                Color.blue,
                Color.cyan,
                Color.white,
                Color.green,
                Color.magenta,
                Color.yellow,
                Color.gray
            };

        colors1 = new string[]
            {
                "red",
                "blue",
                "cyan",
                "white",
                "green",
                "pink",
                "yellow",
                "gray"
            };

        if (PhotonNetwork.IsMasterClient)
        {
            AssignIslands();
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                alivePlayers.Add(player);
                print("player: " + player.ToString());
            }
        }
    }

    [PunRPC]
    public void PlayerDied(Player deadPlayer)
    {
        alivePlayers.Remove(deadPlayer);
        print(deadPlayer.ToString() + " HAS DIED!!!");

        if (alivePlayers.Count == 1)
        {
            Player lastPlayer = alivePlayers[0];
            Debug.Log("The last player alive is: " + lastPlayer.NickName);

            // victory dance lol
        }
    }

    void AssignIslands()
    {
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            int spawnIndex = GetRandomAvailableSpawnIndex();
            occupiedSpawns[spawnIndex] = true;

            photonView.RPC("SpawnPlayer", players[i], spawnIndex);
        }
    }

    int GetRandomAvailableSpawnIndex()
    {
        int index;
        do
        {
            index = Random.Range(0, spawnPoints.Length);
        } while (occupiedSpawns[index]);

        return index;
    }

    [PunRPC]
    void SpawnPlayer(int spawnIndex)
    {
        Transform spawnPoint = spawnPoints[spawnIndex];

        object[] myCustomInitData = new object[2];
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        myCustomInitData[0] = randomColor.ToString();
        myCustomInitData[1] = "hello world";

        GameObject player = PhotonNetwork.Instantiate("Player", spawnPoint.position, Quaternion.identity, 0, myCustomInitData);
        PhotonNetwork.NickName = colors1[spawnIndex].ToString();
/*        GameObject body = player.transform.Find("body").gameObject;
        Renderer bodyRenderer = body.GetComponent<Renderer>();
        print("oof");

        if (bodyRenderer != null)
        {
            //bodyRenderer.material.color = Random.ColorHSV();
            bodyRenderer.material.SetColor("_Color", colors[spawnIndex]);
            print("ewfewf");
        }
        else
        {
            Debug.LogError("Body renderer not found!");
        }*/

        player.GetComponentInChildren<Camera>().enabled = true;
    }
}
