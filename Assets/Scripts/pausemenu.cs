using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausemenu : MonoBehaviour
{
    // Start is called before the first frame update
    public bool paused;
    public GameObject pausemeeenu;
    public PhotonView pw;
    void Start()
    {
        pw = GetComponent<PhotonView>();

        if (pw.IsMine)
        {
            pausemeeenu = GameObject.Find("pausemenu");


            //pausemeeenu = GameObject.Find("pausemenu");
            paused = false;
            pausemeeenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("yess" + notpaused);
        if(Input.GetKeyDown(KeyCode.Escape) && pw.IsMine && !GetComponentInParent<ShopOpener>().shopOpen)
        {
            paused = !paused;
            pausemeeenu.SetActive(paused);
            if(paused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else
            {

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
