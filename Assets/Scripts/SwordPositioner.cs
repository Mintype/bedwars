using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SwordPositioner : MonoBehaviour
{
    public PhotonView pw;
    public GameObject sword;
    public GameObject camera;

    public Vector3 swordOffset = new Vector3(0.5f, -0.5f, 1.0f);

    void Start()
    {
        pw = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pw.IsMine)
        {
            Vector3 desiredPosition = camera.transform.position + camera.transform.rotation * swordOffset;
            Quaternion desiredRotation = camera.transform.rotation;

           // print(sword.transform.position);
            //sword.transform.position = new Vector3(0.5f, 0.75f, 10f);
           // print(sword.transform.position);
            sword.transform.rotation = desiredRotation;
            sword.transform.Rotate(71.114f, -375.4f, -275.027f, Space.Self);

            //desiredRotation += new Vector3(71.114, -375.4, -275.027f);
            //
            //sword.transform.position = desiredPosition;
            //sword.transform.rotation = desiredRotation;
        }
    }
}
