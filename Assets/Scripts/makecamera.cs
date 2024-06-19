using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class makecamera : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject camera;
    public PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();
        if (view == null)
        {
            print("it do be null lol");
        }

        if (view.IsMine)
        {
            print("yesss!");
            camera = transform.Find("Camera").gameObject;
            camera.tag = "MainCamera";
            transform.SetParent(null);
        }
    }
    //efwe
    // Update is called once per frame
    void Update()
    {
        if (view == null)
        {
            print("it do be null lol");
        }

        if (view.IsMine)
        {
            camera.transform.position = transform.position + new Vector3(0, 0.75f, -2.25f);
            camera.transform.rotation = Quaternion.identity;

            camera.transform.position = new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z - 3f);
           /* camera.transform.rotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);*/
            camera.transform.eulerAngles = new Vector3(
                0,
                0,
                0
            );
        }
    }
}
