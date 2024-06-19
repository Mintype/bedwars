using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 100;
    public int height = 20;
    public int depth = 100;
    public float scale = 20f;

    public GameObject grassBlock;
    public GameObject dirtBlock;
    public GameObject stoneBlock;

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                int y = Mathf.FloorToInt(Mathf.PerlinNoise(x / scale, z / scale) * height);

                for (int i = 0; i < y; i++)
                {
                    GameObject block;
                    Quaternion rotation = Quaternion.Euler(-90, 0, 0);
                    Vector3 position = new Vector3(x, i, z);

                    if (i < y - 4)
                        block = Instantiate(stoneBlock, position, rotation);
                    else if (i < y - 1)
                        block = Instantiate(dirtBlock, position, rotation);
                    else
                        block = Instantiate(grassBlock, position, rotation);

                    block.transform.parent = this.transform;
                }
            }
        }
    }
}
