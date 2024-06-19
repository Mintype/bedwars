using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class detectplayer : MonoBehaviourPun
{
    GameObject manager;
    NetworkManager gameManager;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {

        manager = GameObject.Find("Directional Light");
        gameManager = manager.GetComponent<NetworkManager>();
        //text = GameObject.Find("gameoverr").GetComponent<TMP_Text>();
    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
                PhotonView pv = collision.gameObject.GetComponent<PhotonView>();

                string[] winner = { pv.Controller.NickName };
        PhotonView p = PhotonView.Get(this);
        GetComponent<PhotonView>().RPC("GameOver", RpcTarget.All, winner);

        print("whafihewnif");
        /*Destroy(gameObject, 0.25f);*/
    }
    public void del()
    {
        Destroy(this, 1f);
    }

    [PunRPC]
    public void GameOver(string winner)
    {
        print("GAME OVEwefweER!!! " + winner);
        text.text = "GameOVER!! " + winner + " wins!";
        text.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
