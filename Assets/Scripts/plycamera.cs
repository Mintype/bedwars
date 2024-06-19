using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plycamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.tag = "MainCamera";
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position + new Vector3(0, 0.75f, -2.25f);
        transform.rotation = Quaternion.identity;
    }
}
