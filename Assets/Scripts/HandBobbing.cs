using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBobbing : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // attack!!!
            animator.SetTrigger("Attack");
            print("attack!!");
        }
    }
}
