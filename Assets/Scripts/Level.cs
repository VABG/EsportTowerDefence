using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] float wallHeight = 2.0f;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] LevelBlock levelBlock;
    LevelBlock[,] level;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MakeLevel()
    {
        // Delete old level
        if (level != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (level[x, y] != null) DestroyImmediate(level[x, y].gameObject);
                }
            }
        }

        // Destroys all children
        int children = transform.childCount;
        for (int i = 0; i < children; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        // Initialize level field
        level = new LevelBlock[width, height];

        // Calculate center
        Vector2 center = new Vector2(
            (-width * levelBlock.size) / 2.0f + levelBlock.size/2.0f, 
            (height * levelBlock.size) / 2.0f - levelBlock.size/2.0f);

        // Actually make the level
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                LevelBlock b = Instantiate(levelBlock, gameObject.transform);
                b.transform.position = new Vector3(center.x + x * b.size, 0, center.y - y * b.size);
                level[x, y] = b;
            }
        }
    }

    public void UpdatePath()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = level[x, y].transform.position;
                if (level[x, y].isPath)
                {
                    level[x, y].transform.position = new Vector3(pos.x, -levelBlock.transform.localScale.y/2.0f, pos.z);
                }
                else
                {
                    level[x, y].transform.position = new Vector3(pos.x, -levelBlock.transform.localScale.y / 2.0f + wallHeight, pos.z);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
