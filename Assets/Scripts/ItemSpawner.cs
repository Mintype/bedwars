using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Spawns;
    [SerializeField]
    private GameObject item_to_spawn;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Spawns != null && item_to_spawn != null)
            InvokeRepeating("SpawnIron", 1f, 4f);
    }

    void SpawnIron()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < m_Spawns.Length; i++)
            {
                GameObject spawned_item = (GameObject)PhotonNetwork.Instantiate("iron", m_Spawns[i].transform.position, Quaternion.identity);
            }
        }
    }
}
