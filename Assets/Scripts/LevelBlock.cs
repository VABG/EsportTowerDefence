using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    public bool isPath = false;
    public int size = 5;
    Tower tower;
    [SerializeField] Transform selectVisuals;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTower(Tower t)
    {
        tower = t;
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
    }

}
