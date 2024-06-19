using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwordCollision : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called"); 

        if (photonView.IsMine)
        {
            Debug.Log("Sword is mine");

            if (other.CompareTag("Player"))
            {
                Debug.Log("Collided with player, BECAUse their tag is: " + other.tag);

                PhotonView otherPlayerPhotonView = other.GetComponent<PhotonView>();
                if (otherPlayerPhotonView != null)
                {
                    Debug.Log("Other player has PhotonView, sending RPC to decrease health");

                    otherPlayerPhotonView.RPC("DecreaseHealth", RpcTarget.AllBuffered, 1);
                }
                else
                {
                    Debug.Log("Other player does not have a PhotonView");
                }
            }
            else
            {
                Debug.Log("Collided object is not a player, BECAUse their tag is: " + other.tag);
            }
        }
        else
        {
            Debug.Log("Sword is not mine");
        }
    }
}
