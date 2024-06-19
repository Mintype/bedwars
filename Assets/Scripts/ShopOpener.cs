using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOpener : MonoBehaviour
{
    [SerializeField]
    private Vector3 startinglocation;
    public PhotonView pw;
    public GameObject shopPanel;
    public Button shopButton;
    public float shopOpenDistance = 6.0f;

    public bool shopOpen = false;
    void Start()
    {
        pw = GetComponent<PhotonView>();

        if(pw.IsMine)
        {
            startinglocation = transform.position;

            shopPanel = GameObject.Find("Shop Panel");

            if(shopPanel != null )
            {
                shopPanel.SetActive(false);

                shopButton = shopPanel.transform.Find("wool").GetComponent<Button>();

                if (shopButton != null)
                {
                    shopButton.onClick.AddListener(OnShopButtonClicked);
                }
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, startinglocation);
        if (pw.IsMine && distance > shopOpenDistance && shopOpen)
        {
            shopOpen = false;
            shopPanel.SetActive(shopOpen);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if(pw.IsMine && Input.GetKeyDown(KeyCode.E) && !GetComponentInParent<pausemenu>().paused) //GetComponentInParent<ShopOpener>().shopOpen
        {
            if (distance <= shopOpenDistance)
            {
                print("Shop opened! CLICKED");
                if (shopPanel != null)
                {

                    shopOpen = !shopOpen;

                    shopPanel.SetActive(shopOpen); // show the shop panel

                    if(shopOpen)
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    } else
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }
            }
            else
            {
                print("You are too far from the starting location to open the shop!!!!");
            }
        }
    }
    private void OnShopButtonClicked()
    {
        if (GetComponentInParent<ItemCollector>().amountOfIronIHave >= 4)
        {
            GetComponentInParent<ItemCollector>().amountOfIronIHave -= 4;
            GetComponentInParent<ItemCollector>().amountOfBlocksIHave += 8;
        }
    }
}
