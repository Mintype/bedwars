using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator animator;
    public GameObject Sword;
    public PhotonView pw;
    public bool isEnabled = true;
    public GameObject camera;
    public LayerMask mask;
    private bool isBreaking = false;
    private float breakTime = 1.0f;
    private float breakTimer = 0f;

    private TMP_Text text;
    public AudioClip placeBlockSound;

    void Start()
    {
        text = GameObject.Find("Bed Destroyed Text").GetComponent<TMP_Text>();
        pw = GetComponent<PhotonView>();
        if (pw.IsMine)
        {
            animator = Sword.GetComponent<Animator>();
            if (camera == null)
            {
                camera = GetComponentInChildren<Camera>().gameObject;
            }
            text.enabled = false;
        }
    }

    void Update()
    {
        if (pw.IsMine && !GetComponentInParent<pausemenu>().paused && !GetComponentInParent<ShopOpener>().shopOpen)
        {
            Sword.transform.position = transform.position;

            if (GetComponentInParent<PlayerHotBar>().selected == 0)
            {
                isEnabled = true;
                Sword.SetActive(isEnabled);
            } else
            {
                isEnabled= false;
                Sword.SetActive(isEnabled);
            }

            if (Input.GetMouseButton(0) && GetComponentInParent<PlayerHotBar>().selected == 0)
            {
                // attack!!!
                animator.SetTrigger("Attack");
            }
            else if (Input.GetMouseButton(0) && GetComponentInParent<PlayerHotBar>().selected != 0)
            {
                if (!isBreaking)
                {
                    print("starting to break!!!!");
                    isBreaking = true;
                    StartCoroutine(BreakObject());
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isBreaking = false;
                breakTimer = 0f;
            }
        }
    }

    private IEnumerator BreakObject()
    {
        while (isBreaking)
        {
            print("breaking!");
            breakTimer += Time.deltaTime;
            if (breakTimer >= breakTime)
            {
                if (Physics.Raycast(camera.transform.position, camera.transform.forward, out var hit, Mathf.Infinity, mask))
                {
                    var obj = hit.collider.gameObject;
                    string obj_tag = obj.tag;
                    string obj_name = obj.name;
                    Debug.Log($"Looking at {obj.name}", this);
                    if (obj.name != (pw.Controller.NickName + " Bed"))
                    {
                        Debug.Log($"Looking at {obj.name}", this);
                        try
                        {
                            PhotonView pv = obj.GetComponent<PhotonView>();
                            pv.TransferOwnership(PhotonNetwork.LocalPlayer);
                            PhotonNetwork.Destroy(obj);
                            if(obj_tag == "Bed")
                            {
                                pw.RPC("BedDestroyed", RpcTarget.All, obj_name);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Debug.LogError($"Error destroying object: {ex.Message}");
                        }
                        Debug.Log($"Looking at {obj.name}", this);
                    } else
                    {
                        print("YOU CANT BREAK UR OWN BED IDIOT!");
                    }

                }
                breakTimer = 0f;
                isBreaking = false;
            }
            yield return null;
        }
    }

    [PunRPC]
    private void BedDestroyed(string name)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if(audioSource != null)
        {
            audioSource.PlayOneShot(placeBlockSound);
        } else
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(placeBlockSound);
        }


        text.text = name + " has been destroyed!";
        text.enabled = true;
        Invoke("turnoffdeathtext", 2.0f);
    }
    void turnoffdeathtext()
    {
        text.enabled = false;
    }
}
