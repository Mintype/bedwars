using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    public PhotonView view;
    public GameObject panel;
    public GameObject inputField;
    public GameObject leavebutton;
    public TMP_Text username;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        this.gameObject.AddComponent<PhotonView>();
      /*  view = PhotonView.Get(this);*/
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public override void OnConnectedToMaster()
    {
        print("connected!!!");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("room1");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        panel.SetActive(true);
        leavebutton.SetActive(false);
    }
    public override void OnJoinedRoom()
    {
            print("room jo8ined");
            panel.SetActive(false);
            leavebutton.SetActive(true);
            GameObject player = (GameObject)PhotonNetwork.Instantiate("Player", new Vector3(0, 5, 0), Quaternion.identity);
            player.GetComponentInChildren<Camera>().enabled = true;
            PhotonNetwork.NickName = username.text/*inputField.GetComponent<TMP_InputField>().text*/;
            
    }
    public void CreateRoom()
    {
        if(instance != null)
            PhotonNetwork.CreateRoom("room1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void GameOver(string winner)
    {
        print("GAME OVEER!!! " + winner);
        Time.timeScale = 0;
    }

}
