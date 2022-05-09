using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    public bool isPath = false;
    public int size = 5;

    Tower tower;
    [HideInInspector]
    [SerializeField]List<GameObject> trees;

    [SerializeField] Transform selectVisuals;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RemoveTrees()
    {
        if (trees == null) return;
        for (int i = trees.Count-1; i >= 0 ; i--)
        {
            Destroy(trees[i]);
        }
        trees.Clear();
        trees = null;
    }

    public void AddTree(GameObject tree)
    {
        if (trees == null) trees = new List<GameObject>();
        trees.Add(tree);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleVisible(bool visible)
    {

        GetComponent<MeshRenderer>().enabled = visible;
    }

    public bool HasTower()
    {
        if (tower != null) return true;
        else return false;
    }

    public void ShowSelected()
    {
        selectVisuals.gameObject.SetActive(true);
    }

    public void HideSelected()
    {
        selectVisuals.gameObject.SetActive(false);
    }

    public void PlaceTower(Tower t)
    {
        tower = Instantiate(t);
        t = tower;
        tower.transform.position = transform.position + Vector3.up * transform.localScale.y / 2.0f;
        RemoveTrees();
    }

}
