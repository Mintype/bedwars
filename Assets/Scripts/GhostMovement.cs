using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GhostMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float ascendSpeed = 5f;
    public KeyCode ascendKey = KeyCode.Space;
    public KeyCode descendKey = KeyCode.LeftShift;

    public Transform orientation;

    private PhotonView pw;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private float verticalMovement;

    void Start()
    {
        pw = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        if (!pw.IsMine)
        {
            this.enabled = false;
        }

        rb.useGravity = false;
    }

    void Update()
    {
        if (!GetComponentInParent<pausemenu>().paused)
        {
            if (pw.IsMine)
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                float ascend = 0f;
                if (Input.GetKey(ascendKey))
                {
                    ascend = ascendSpeed;
                }
                else if (Input.GetKey(descendKey))
                {
                    ascend = -ascendSpeed;
                }

                Vector3 forward = orientation.forward;
                Vector3 right = orientation.right;
                forward.y = 0;
                right.y = 0;
                forward.Normalize();
                right.Normalize();
                moveDirection = (horizontal * right + vertical * forward).normalized * moveSpeed;
                verticalMovement = ascend;
            }
        } else
        {
            moveDirection = Vector3.zero;
            verticalMovement = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (pw.IsMine)
        {
            Vector3 movement = moveDirection;
            movement.y = verticalMovement;
            rb.velocity = movement;
        }
    }
}
