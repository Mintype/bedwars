using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class GernadeShooter : MonoBehaviour
{
    public GameObject gernade_prefab;
    public float throwForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print("hey");
        if (Input.GetKeyDown("g"))
        {

          //  print("ewfwef");
            createGernade();
        }
    }

    private void createGernade()
    {
        Vector3 spawnPosition = transform.position + transform.forward * 0.5f;
        //GameObject grenade = Instantiate(gernade_prefab, spawnPosition, transform.rotation);
        GameObject grenade = (GameObject)PhotonNetwork.Instantiate("Gernade", spawnPosition, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        PhotonView photonView = grenade.GetComponent<PhotonView>();
        PhotonRigidbodyView photonRigidbodyView = grenade.GetComponent<PhotonRigidbodyView>();

        photonView.ObservedComponents = new List<Component> { photonRigidbodyView };

/*        photonRigidbodyView.m_SynchronizeVelocity = true;
        photonRigidbodyView.m_SynchronizeAngularVelocity = true;*/

        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
