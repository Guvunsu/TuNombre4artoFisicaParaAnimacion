using System.Collections.Generic;
using UnityEngine;

public class SineGrapher : MonoBehaviour
{
    #region Parameters

    [SerializeField] public GameObject prfNode;
    [Range(1, 10000000), SerializeField] public int nodeResolution = 100;
    [Range(1f, 100f), SerializeField] public float graphWidth = 1f;
    [SerializeField] public SineParameters_SO soSP;

    #endregion

    #region RuntimeVariables

    [SerializeField] protected List<GameObject> instancedNodes;
    protected GameObject nodeInstance;  //auxiliar / local variable to work with an instance of an object
    protected Vector3 nodePosition;

    #endregion

    #region UnityMethods

    private void OnDrawGizmos() //Update() in editor mode for a Mono Behaviour
    {
        for (int i = 0; i < instancedNodes.Count; i++)
        {
            nodeInstance = instancedNodes[i];
            nodePosition = nodeInstance.transform.localPosition;
            nodePosition.x = ((float)i * graphWidth / (float)nodeResolution); //nodePosition's original X coordinate
            nodePosition.y =
                soSP.sineParameters.A *
                Mathf.Sin((soSP.sineParameters.B * nodePosition.x) + soSP.sineParameters.C) +
                soSP.sineParameters.D
                ; //A * sen(B * x + C) + D 
            nodePosition.x *= soSP.sineParameters.horizontalScale;
            nodeInstance.transform.localPosition = nodePosition;
        }
    }

    #endregion

    #region PublicMethods

    public void CreateSineGrapher()
    {
        DestroySineGrapher();
        instancedNodes = new List<GameObject>();
        for (int i = 0; i < nodeResolution; i++)
        {
            nodeInstance = Instantiate(prfNode);
            nodeInstance.transform.parent = this.transform;
            nodeInstance.transform.localPosition = Vector3.right * ((float)i * graphWidth / (float)nodeResolution);
            instancedNodes.Add(nodeInstance);
        }
    }

    public void DestroySineGrapher()
    {
        //if (instancedNodes.Count > 0)
        //{
            for (int i = instancedNodes.Count - 1; i >= 0; i--)
            {
                nodeInstance = instancedNodes[i];
                instancedNodes.Remove(nodeInstance);
                DestroyImmediate(nodeInstance); //destroy an instance of an object during editor mode
            }
        //}
    }

    #endregion

}
