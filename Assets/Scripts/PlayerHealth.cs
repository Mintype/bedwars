using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PhotonView pw;

    [SerializeField]
    private int m_Health;

    private HealthPanel healthPanel;

    [SerializeField]
    private GameObject myBed;

    [SerializeField]
    private GameObject sword;

    // Start is called before the first frame update
    void Start()
    {
        pw = GetComponent<PhotonView>();

        if (pw.IsMine)
        {
            gameObject.name = pw.Controller.NickName;

            m_Health = 20;

            GameObject.Find("Health Panel").GetComponent<HealthPanel>().ResetHealth();

            String myColor = pw.Controller.NickName;

            myBed = findMyBed(myColor);

            StartCoroutine(RegenerateHealth());
        }
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            if (m_Health < 20)
            {
                m_Health++;
                GameObject.Find("Health Panel").GetComponent<HealthPanel>().UpdateHealth(m_Health);
                Debug.Log("Health increased, new health: " + m_Health);
            }
        }
    }


    private GameObject findMyBed(string myColor)
    {
        GameObject bed1;

        bed1 = GameObject.Find(myColor + " Bed");

        return bed1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pw.IsMine && other.CompareTag("Melee"))
        {
            m_Health --;
            if (m_Health < 0) m_Health = 0;
            GameObject.Find("Health Panel").GetComponent<HealthPanel>().UpdateHealth(m_Health);
            Debug.Log("Health decreased, new health: " + m_Health);

            if (m_Health <= 0)
            {
                Debug.Log("Player died");

                if (myBed != null)
                {

                    m_Health = 20;
                    GameObject.Find("Health Panel").GetComponent<HealthPanel>().ResetHealth();
                    GetComponentInParent<PlayerInVoid>().death();
                    string nickname = pw.Controller.NickName;
                    pw.RPC("deathofplayer", RpcTarget.All, nickname);

                } else
                {
                    GetComponentInParent<PlayerInVoid>().isDead = true;

                    turnbrointoghost();
                }
            }
        }
    }

    public void turnbrointoghost()
    {
        string nickname = pw.Controller.NickName;
        pw.RPC("permDeathofPlayer", RpcTarget.All, nickname);


        // turn off player things
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Attack>().enabled = false;
        GetComponent<ItemCollector>().enabled = false;

        // Turn off gravity
        GetComponent<Rigidbody>().useGravity = false;

        // turn off the sword ijffwurnvpu
        if(sword != null)
            Destroy(sword);

        // turn off colliderrswffqe
        DisableAllColliders(gameObject);
        MakeInvisible(gameObject);


        GetComponent<GhostMovement>().enabled = true;

        GameObject.Find("Health Panel").GetComponent<HealthPanel>().DisapearHealth();
        GameObject.Find("Game Panel").SetActive(false);
    }

    private void DisableAllColliders(GameObject obj)
    {
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }
    private void MakeInvisible(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }
}
