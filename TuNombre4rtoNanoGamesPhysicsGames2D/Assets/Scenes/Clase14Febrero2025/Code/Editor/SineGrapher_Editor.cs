using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SineGraph))]
public class SineGrapher_Editor : Editor
{

    #region RunTimeVariables

    [SerializeField, HideInInspector] protected SineGraph _scrSineGrapher;

    #endregion

    #region UnityMethods

    public override void OnInspectorGUI() // upodate() for this editor scrit
    {
        DrawDefaultInspector();// draws the native varibales from the monobehaviour
        if (_scrSineGrapher == null)
        {
            _scrSineGrapher = (SineGraph)target;
        }
        if (GUILayout.Button("Create Sine Graph"))
        {

        }
        else if (GUILayout.Button("Destroy Sine Graph"))
        {
            _scrSineGrapher.DestroySineGrapher();
        }
    }

    #endregion
}
