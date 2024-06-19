using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public PhotonView pw;
    private Vector3 move;
    public float moveSpeed = 5f, jumpforce = 10f;
    public TMP_Text username;
    GameObject spherep;

    GameObject manager;
    NetworkManager gameManager;

    void Start()
    {
        manager = GameObject.Find("Directional Light");
        gameManager = manager.GetComponent<NetworkManager>();


        rb = GetComponent<Rigidbody>();
        pw = GetComponent<PhotonView>();
        move = new Vector3(0, 0, 0);
        username = GetComponentInChildren<TMP_Text>();
        username.text = pw.Controller.NickName;

        if (pw.IsMine)
        {
            spherep = transform.GetChild(0).gameObject;

            //spherep.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown("space") && rb.velocity.y == 0)
        {
            Debug.Log("space key was pressed");
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(move * moveSpeed);
    }
}
