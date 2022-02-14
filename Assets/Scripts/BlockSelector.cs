using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelector : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] List<Tower> towerPrefabs;
    Camera cam;
    Tower activeTower;
    LevelBlock selectedBlock;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeTower = towerPrefabs[0];
        }
        RayCast();
    }

    void RayCast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedBlock != null)
            {
                selectedBlock.HideSelected();
                selectedBlock = null;
            }

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // PROBLEM FOUND, LAYER MASK WAS NOT BEING USED SOMETIMES FOR WHATEVER REASON!!!
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                Transform objectHit = hit.transform;
                LevelBlock lb = objectHit.GetComponent<LevelBlock>();
                if (lb != null)
                {
                    if (lb.isPath) return;
                    selectedBlock = lb;
                    lb.ShowSelected();

                    if (activeTower != null && !lb.HasTower())
                    {
                        lb.PlaceTower(activeTower);
                        activeTower = null;
                    }

                }
                else
                {
                    objectHit.position += Vector3.up;
                }
            }
        }
    }
}
