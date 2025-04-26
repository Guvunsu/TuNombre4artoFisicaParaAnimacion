using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SineGrapher))]
public class SineGrapher_Editor : Editor
{
    #region RuntimeVariables

    [SerializeField, HideInInspector] protected SineGrapher _scrSineGrapher;

    #endregion

    #region UnityMethods

    public override void OnInspectorGUI()  //Update() for this editor scrpit
    {
        DrawDefaultInspector(); //draws the native variables from the Mono Bheaviour
        if (_scrSineGrapher == null)
        {
            _scrSineGrapher = (SineGrapher)target;
        }
        if (GUILayout.Button("Create Sine Graph"))
        {
            _scrSineGrapher.CreateSineGrapher();
        }
        else if (GUILayout.Button("Destroy Sine Graph"))
        {
            _scrSineGrapher.DestroySineGrapher();
        }
    }

    #endregion

}
