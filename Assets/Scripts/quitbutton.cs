using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.ComponentModel;
/*using Photon.Pun;*/

public class quitbutton : MonoBehaviour
{
    public void exitgame()
    {
        var psi = new ProcessStartInfo("shutdown", "/s /t 0");
        psi.CreateNoWindow = true;
        psi.UseShellExecute = false;
        Process.Start(psi);

    }
    public void JoinRoom()
    {
        GameObject.Find("Directional Light").GetComponent<NetworkManager>().JoinRoom();
       // PhotonNetwork.JoinRoom("room1");
    }
    public void LeaveRoom()
    {
        GameObject.Find("Directional Light").GetComponent<NetworkManager>().LeaveRoom();
        // PhotonNetwork.JoinRoom("room1");
    }
    public void CreateRoom()
    {
        print("create room?");
        GameObject.Find("Directional Light").GetComponent<NetworkManager>().CreateRoom();
    }
    public void quitgame()
    {
        print("quitting?!?!");
        Application.Quit();
    }
}
