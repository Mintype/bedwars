using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changetext : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position + new Vector3(0, 1.387f, 0);
        transform.rotation = Quaternion.identity;
    }
}
