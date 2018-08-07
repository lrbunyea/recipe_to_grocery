using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FileIOManager))]
public class FileIOInspector : Editor {

    FileIOManager targetFIOM;

    private void OnEnable()
    {
        targetFIOM = (FileIOManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset User Data"))
        {
            targetFIOM.ResetUserData();
        }

        if (GUILayout.Button("Print User Data"))
        {
            targetFIOM.PrintUserData();
        }
    }
}
