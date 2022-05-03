using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BlockShapeAndGameObject
{
    public GameObject g;
    public bool[] shape;
}

public class Level : MonoBehaviour
{
    [SerializeField] float wallHeight = 2.0f;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] LevelBlock levelBlock;
    LevelBlock[,] level;
    float blockSize = 5.0f;
    [SerializeField] Transform treeParent;
    [SerializeField] Transform visualsParent;

    [SerializeField] BlockShapeAndGameObject[] blocks;

    [SerializeField] GameObject treeModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private GameObject GetBlockType(int xPos, int yPos)
    {
        bool[] blockShape = new bool[9];

        if (level[xPos, yPos].isPath)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (InLevel(xPos - 1 + x, yPos - 1 + y))
                    {
                        if (level[xPos - 1 + x, yPos - 1 + y].isPath) 
                            blockShape[y * 3 + x] = true;
                    }
                }
            }
        }

        return GetBlocktypeFromBlockList(blockShape);
    }


    private GameObject GetBlocktypeFromBlockList(bool[] blockType)
    {
        GameObject g = null;
        bool success = true;
        for (int i = 0; i < blocks.Length; i++)
        {
            success = true;
            for (int j = 0; j < 9; j++)
            {
                if (j == 0 || j == 2 || j == 6 || j == 8) continue;
                if (blocks[i].shape[j] != blockType[j])
                {
                    success = false;
                    break;
                }
            }
            if (success)
            {
                g = blocks[i].g;
                break;
            }
        }
        if (!success) 
        { 
            Debug.LogError("FAILED TO FIND MATCH");
            g = blocks[0].g;
        }
        return g;
    }

    private bool InLevel(int x, int y)
    {
        if (x < 0) return false;
        if (x > width - 1) return false;
        if (y < 0) return false;
        if (y > height - 1) return false;
        return true;
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
        Vector2 center = GetCenter();

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

    private Vector2 GetCenter()
    {
        return new Vector2(
            (-width * blockSize) / 2.0f + blockSize / 2.0f,
            (height * blockSize) / 2.0f - blockSize / 2.0f);
    }

    public void ToggleBlockVisibility(bool visible)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                level[x, y].ToggleVisible(visible);
            }
        }
    }


    public void UpdatePath()
    {
        if (level == null)
        {
            LevelBlock[] blocks = GetComponentsInChildren<LevelBlock>();
            if (blocks.Length == width * height)
            {
                level = new LevelBlock[width, height];
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        level[x, y] = blocks[x * height + y];
                    }
                }
            }
            else
            {
                Debug.LogError("No level or too few blocks! Total of " + blocks.Length+ " blocks out of " + (width * height).ToString());
                return;
            }
        }

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


        int children = visualsParent.childCount;
        for (int i = 0; i < children; i++)
        {
            DestroyImmediate(visualsParent.GetChild(0).gameObject);
        }

        // Visuals
        Vector2 center = GetCenter();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject g = Instantiate(GetBlockType(x, y), visualsParent);
                g.transform.position = new Vector3(center.x + x * blockSize, 0, center.y - y * blockSize);
            }
        }
        PlantTrees();
        //PlantOneTree(0, 0);
        ToggleBlockVisibility(false);
    }

    void PlantTrees()
    {
        int children = treeParent.childCount;
        for (int i = 0; i < children; i++)
        {
            DestroyImmediate(treeParent.GetChild(0).gameObject);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                PlantOneTree(x, y);
            }
        }
    }

    void PlantOneTree(int x, int y)
    {
        float amount = SampleDensity(x, y);
        float amountMulted = amount * 3;

        for (int i = 0; i < amountMulted; i++)
        {
            Vector2 pos = new Vector2(Random.value-.5f, Random.value-.5f) * blockSize;
            GameObject g = Instantiate(treeModel, new Vector3(pos.x, 2.5f, pos.y) + level[x, y].transform.position,
                Quaternion.identity, treeParent);
            g.transform.localScale = Vector3.one * (amount * 2.0f + .25f);
            g.transform.Rotate(new Vector3(0, Random.value * 360, 0));
        }
    }

    float SampleDensity(int xPos, int yPos, int sampleSize = 12)
    {
        float density = 0;
        if (level[xPos, yPos].isPath) return 0;
        for (int x = 0; x < sampleSize; x++)
        {
            for (int y = 0; y < sampleSize; y++)
            {
                Vector2Int p = new Vector2Int(x - (sampleSize / 2) + xPos, y - (sampleSize / 2) + yPos);
                if (InLevel(p.x, p.y))
                {
                    if (level[p.x, p.y].isPath) density++;
                }
            }
        }

        density /= (float)(sampleSize * sampleSize);
        return (1 - (density*4));
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
