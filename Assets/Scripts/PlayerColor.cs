using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    // Start is called before the first frame update
    public PhotonView pw;


    void Start()
    {
        pw = GetComponent<PhotonView>();
      //  print("tacos: " + pw.Controller.NickName);
        string nickname = pw.Controller.NickName;

        GameObject body = transform.Find("body").gameObject;
        Renderer bodyRenderer = body.GetComponent<Renderer>();

        switch (nickname.ToLower())
        {
            case "red":
                bodyRenderer.material.SetColor("_Color", Color.red);
                break;
            case "blue":
                bodyRenderer.material.SetColor("_Color", Color.blue);
                break;
            case "green":
                bodyRenderer.material.SetColor("_Color", Color.green);
                break;
            case "yellow":
                bodyRenderer.material.SetColor("_Color", Color.yellow);
                break;
            case "black":
                bodyRenderer.material.SetColor("_Color", Color.black);
                break;
            case "white":
                bodyRenderer.material.SetColor("_Color", Color.white);
                break;
            case "cyan":
                bodyRenderer.material.SetColor("_Color", Color.cyan);
                break;
            case "magenta":
                bodyRenderer.material.SetColor("_Color", Color.magenta);
                break;
            case "gray":
            case "grey":
                bodyRenderer.material.SetColor("_Color", Color.gray);
                break;
            default:
                Debug.LogWarning("Unknown color: " + nickname);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
