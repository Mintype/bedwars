using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    public GameObject cubePrefab;

    private GameObject cubeInstance;
    public bool placing = false;
    Vector3 cubePosition;
    private PhotonView photonView;
    private string blockname;
    public GameObject[] wools;
    private GameObject wooltoinstaliate;
    public AudioClip placeBlockSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        cubePosition = new Vector3(0, 0, 0);
        photonView = GetComponentInParent<PhotonView>();
        blockname = photonView.Controller.NickName + "wool";
        for(int i = 0; i < wools.Length; i++)
        {
            if (wools[i].name == blockname)
            {
                wooltoinstaliate = wools[i];
            }
        }
    }

    void Update()
    {
        if(GetComponentInParent<pausemenu>().paused || GetComponentInParent<ShopOpener>().shopOpen && cubeInstance != null)
        {
            if (cubeInstance != null)
            {
                cubeInstance.GetComponent<Renderer>().enabled = false;
            }
        }


        if (!GetComponentInParent<pausemenu>().paused && !GetComponentInParent<ShopOpener>().shopOpen)
        {
            bool canPlace = false;
            if (GetComponentInParent<PlayerHotBar>().selected == 1)
            {
                placing = true;
                if (placing)
                {
                    if(cubeInstance != null)
                    {
                        cubeInstance.GetComponent<Renderer>().enabled = false;
                    } else {
                    // make the cube at the position of the camera
                    cubeInstance = Instantiate(wooltoinstaliate, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

                    if (cubeInstance.GetComponent<Collider>() != null)
                    {
                        cubeInstance.GetComponent<Collider>().enabled = false;
                    }
                    cubeInstance.GetComponent<Renderer>().enabled = false;

                    }


                }
            } else
            {
                if(cubeInstance != null)
                    cubeInstance.GetComponent<Renderer>().enabled = false;

                //Destroy(cubeInstance);
                placing = false;
/*                if (cubeInstance != null)
                {
                    Destroy(cubeInstance);
                    cubeInstance = null;
                }*/
            }
            if (placing)
            {
                Vector3 cameraPosition = transform.position;
                Quaternion cameraRotation = transform.rotation;

                Vector3 forwardDirection = cameraRotation * Vector3.forward;

                cubePosition = cameraPosition + forwardDirection * 3f;

                cubePosition.x = Mathf.Floor(cubePosition.x) + 0.5f;
                cubePosition.y = Mathf.Floor(cubePosition.y) + 0.5f;
                cubePosition.z = Mathf.Floor(cubePosition.z) + 0.5f;

                cubeInstance.transform.position = cubePosition;

                Vector3 halfExtents = cubeInstance.transform.localScale / 2;
                Collider[] hitColliders = Physics.OverlapBox(cubePosition, halfExtents, cubeInstance.transform.rotation);

                canPlace = hitColliders.Length > 0;

                for(int i = 0; i < hitColliders.Length; i++)
                {
                    if (/*hitColliders[i].tag == "Bed" ||*/ hitColliders[i].tag == "Iron" || hitColliders[i].tag == "Gold")
                    {
                        canPlace = false;
                        break;
                    }
                }
                //canPlace = true; // testing only

                if (!canPlace)
                {
                    cubeInstance.GetComponent<Renderer>().material.color = Color.red;
                    cubeInstance.GetComponent<Renderer>().enabled = false;
                }
                else
                {
                    cubeInstance.GetComponent<Renderer>().material.color = Color.white;
                    cubeInstance.GetComponent<Renderer>().enabled = true;
                }
            }
            if (Input.GetMouseButtonDown(1) && placing && !GetComponentInParent<pausemenu>().paused && !GetComponentInParent<ShopOpener>().shopOpen) // 1 is the right mouse button
            {
                if (canPlace && GetComponentInParent<ItemCollector>().amountOfBlocksIHave > 0)
                {
                    print("placing!!");
                    PhotonNetwork.Instantiate(blockname, cubePosition, Quaternion.Euler(new Vector3(-90, 0, 0)));
                    GetComponentInParent<ItemCollector>().amountOfBlocksIHave--;
                    audioSource.PlayOneShot(placeBlockSound);
                }
            }
        }
    }
}
