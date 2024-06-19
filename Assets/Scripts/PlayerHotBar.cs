using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotBar : MonoBehaviour
{
    [SerializeField]
    public int selected = 0;
    public PhotonView pw;

    // Start is called before the first frame update
    void Start()
    {
        pw = GetComponent<PhotonView>();
        if (!pw.IsMine)
            this.enabled = false;
        else
        {
            string nickname = pw.Controller.NickName;
            GameObject.Find("HotBar").GetComponent<HotBar>().UpdateWoolColor(nickname);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pw.IsMine)
        {
            int oldsel = selected;
            if (Input.GetKeyDown(KeyCode.Alpha1))
                selected = 0;
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                selected = 1;
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                selected = 2;
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                selected = 3;
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                selected = 4;
            else if (Input.GetKeyDown(KeyCode.Alpha6))
                selected = 5;
            else if (Input.GetKeyDown(KeyCode.Alpha7))
                selected = 6;
            else if (Input.GetKeyDown(KeyCode.Alpha8))
                selected = 7;

            if (oldsel != selected)
                GameObject.Find("HotBar").GetComponent<HotBar>().UpdateHotBar(selected);
        }
    }
}
