using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelGenerator : Editor
{
    // Start is called before the first frame update
    void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        Level l = (Level)target;

        if (GUILayout.Button("Create Level(CLEARS EXISTING LEVEL!)"))
        {
            l.MakeLevel();
        }

        if (GUILayout.Button("Update Path"))
        {
            l.UpdatePath();
        }

        base.OnInspectorGUI();
    }
}
