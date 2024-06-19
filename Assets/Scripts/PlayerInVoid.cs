using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInVoid : MonoBehaviour
{
    private Vector3 spawnPosition;
    public PhotonView pw;
    private TMP_Text text;
    public bool isDead = false;
    Rigidbody rb;
    private GameObject myBed;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        pw = GetComponent<PhotonView>();
        text = GameObject.Find("PlayerDiedText").GetComponent<TMP_Text>();
        text.enabled = false;

        if(pw.IsMine)
        {
            rb = GetComponent<Rigidbody>();
            myBed = GameObject.Find(pw.Controller.NickName + " Bed");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pw.IsMine)
        {
            if (transform.position.y < -10.0f)
            {
                print("player fell!!!!!");
            }
            if (transform.position.y < -10.0f && !isDead)
            {
                print("player fell and is not dead");
                isDead = true;
                // die
                //  print("player die!");
                //transform.position = spawnPosition;

                if (myBed != null) {

                    Invoke("death", 1.0f);
                    string nickname = pw.Controller.NickName;
                    pw.RPC("deathofplayer", RpcTarget.All, nickname);

                } else
                {
/*                    Player player = GetComponent<Player>();
                    if (player != null)
                    {
                        print("player value is nuulll1!");
                    } else
                    {
                        print("player is not null yessssssssss");
                    }*/
                    //pw.RPC("PlayerDied", RpcTarget.All, player);
                    GetComponentInParent<PlayerHealth>().turnbrointoghost();
                    transform.position = spawnPosition;
                    string nickname = pw.Controller.NickName;
                    pw.RPC("permDeathofPlayer", RpcTarget.All, nickname);
                }

            }
        }
    }

    // death script
    public void death()
    {
        if (pw.IsMine)
        {
            GetComponentInParent<ItemCollector>().ResetItems();
            transform.position = spawnPosition;
            rb.velocity = Vector3.zero;
            print("player respawned!");
            isDead = false;
        }
    }

    [PunRPC]
    void deathofplayer(string a)
    {
       // Debug.Log("bro died: " + a);

        text.text = a + " Has died!";
        text.enabled = true;
        Invoke("turnoffdeathtext", 2.0f);
    }

    [PunRPC]
    void permDeathofPlayer(string a)
    {
        // Debug.Log("bro died: " + a);

        text.text = a + " Has been eliminated!";
        text.enabled = true;
        DisableAllColliders(gameObject);
        MakeInvisible(gameObject);
        Invoke("turnoffelimtext", 2.0f);
    }
    void turnoffelimtext()
    {
        text.enabled = false;
        if (!pw.IsMine)
        {
            print("DESTROYING: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    void turnoffdeathtext()
    {
        text.enabled = false;
    }
    private void DisableAllColliders(GameObject obj)
    {
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }
    private void MakeInvisible(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }
}
