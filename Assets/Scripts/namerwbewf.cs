using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class namerwbewf : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObject;
    [SerializeField] TextMeshProUGUI m_Object;
    void Start()
    {
/*        GameObject canvas = GameObject.Find("Canvas");
        string name = canvas.transform.Find("Panel").gameObject.transform.Find("inputname").GetComponent<TMP_InputField>().text;
        print("NAME: " + name);*/
        
        string namee = PhotonNetwork.NickName;
        //m_Object.GetComponent<TextMeshPro>().text = name;
        GameObject grwg = transform.Find("nameee").gameObject;
        GameObject grwge = grwg.transform.GetChild(0).gameObject;
        print("wefew" + grwge.GetComponent<TextMeshProUGUI>().text);
        grwg.GetComponent<TextMeshProUGUI>().text = namee;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
