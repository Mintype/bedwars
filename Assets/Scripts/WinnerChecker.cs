using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerChecker : MonoBehaviour
{
    public PhotonView pw;

    [SerializeField]
    private GameObject[] players;

    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        pw = GetComponent<PhotonView>();
        if (pw.IsMine)
        {
            text = GameObject.Find("Player Won Text").GetComponent<TMP_Text>();

            players = new GameObject[8];
            Invoke("FindPlayers", 2f);
        }
    }

    void FindPlayers()
    {
        GameObject[] foundPlayers = GameObject.FindGameObjectsWithTag("Player");
        int index = 0;

        foreach (GameObject player in foundPlayers)
        {
            if (index < 8 && player.GetComponent<Rigidbody>() != null)
            {
                players[index] = player;
                index++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pw.IsMine)
        {
            int nullCount = 0;

            foreach (GameObject player in players)
            {
                if (player == null)
                {
                    nullCount++;
                }
            }

            if (nullCount == 7)
            {
                Debug.Log("WINNER");
                string nickname = pw.Controller.NickName;
                pw.RPC("winnerletsgo", RpcTarget.All, nickname);
            }
        }
    }
    [PunRPC]
    void winnerletsgo(string a)
    {
        text.text = a + " Has Won!";
        text.enabled = true;
    }
}
