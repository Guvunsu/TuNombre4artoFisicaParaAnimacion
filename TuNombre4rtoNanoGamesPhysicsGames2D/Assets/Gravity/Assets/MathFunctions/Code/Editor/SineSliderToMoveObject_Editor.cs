using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SineSliderToMoveObject))]
public class SineSliderToMoveObject_Editor : Editor
{
    #region RuntimeVariables

    protected SineSliderToMoveObject _scrSineSliderToMoveObject; //Mono Behaviour

    #endregion

    #region UnityMethods

    public override void OnInspectorGUI() //Update()
    {
        DrawDefaultInspector(); //we retrieve the original parameters from the Mono Behaviour Script
        if (_scrSineSliderToMoveObject == null)
        {
            _scrSineSliderToMoveObject = (SineSliderToMoveObject)target;
        }
        if (GUILayout.Button(""))
        {

        }
    }

    #endregion
}
