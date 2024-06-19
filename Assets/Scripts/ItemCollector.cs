using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public PhotonView pw;

    [SerializeField]
    public int amountOfBlocksIHave;
    [SerializeField]
    public int amountOfIronIHave;
    [SerializeField]
    public int amountOfGoldIHave;
    // Start is called before the first frame update
    void Start()
    {
        pw = GetComponent<PhotonView>();
        if (pw.IsMine)
        {
            amountOfBlocksIHave = 0;
            amountOfIronIHave = 0;
            amountOfGoldIHave = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pw.IsMine)
        {
            GameObject.Find("HotBar").GetComponent<HotBar>().UpdateWoolAmount(amountOfBlocksIHave);
            GameObject.Find("HotBar").GetComponent<HotBar>().UpdateIronAmount(amountOfIronIHave);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (pw.IsMine)
        {
            if (collision.gameObject.tag == "Iron")
            {
                PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
                pv.TransferOwnership(PhotonNetwork.LocalPlayer);

                // Wait until the ownership transfer is completed
                StartCoroutine(DestroyAfterOwnershipTransfer(pv, collision.gameObject, "Iron"));
            }
            if (collision.gameObject.tag == "Gold")
            {
                PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
                pv.TransferOwnership(PhotonNetwork.LocalPlayer);

                // Wait until the ownership transfer is completed
                StartCoroutine(DestroyAfterOwnershipTransfer(pv, collision.gameObject, "Gold"));
            }
        }
    }

    private IEnumerator DestroyAfterOwnershipTransfer(PhotonView pv, GameObject obj, string type)
    {
        // Wait until ownership is transferred
        while (!pv.AmOwner)
        {
            yield return null;
        }

        PhotonNetwork.Destroy(obj);

        if (type == "Iron")
        {
            amountOfIronIHave++;
            GameObject.Find("HotBar").GetComponent<HotBar>().UpdateIronAmount(amountOfIronIHave);
            Debug.Log("Iron Touched! Now I have: " + amountOfIronIHave);
        }
        else if (type == "Gold")
        {
            amountOfGoldIHave++;
            Debug.Log("Gold Touched! Now I have: " + amountOfGoldIHave);
        }
    }

    public void ResetItems()
    {
        amountOfBlocksIHave = 0;
        amountOfIronIHave = 0;
        //amountOfGoldIHave = 0;

        //GameObject.Find("HotBar").GetComponent<HotBar>().UpdateWoolAmount(amountOfBlocksIHave);
        //GameObject.Find("HotBar").GetComponent<HotBar>().UpdateIronAmount(amountOfIronIHave);
        //GameObject.Find("HotBar").GetComponent<HotBar>().UpdateGoldAmount(amountOfGoldIHave);
    }
}
