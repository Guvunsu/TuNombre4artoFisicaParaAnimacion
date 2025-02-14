using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SineParameters
{
    // a- sen(b*c)+D
    public float A;//vertical stretch
    public float B;//horizontal stretch
    public float C;//horizontal displacement
    public float D;//vertical displacement
}

public class SineGraph : MonoBehaviour
{
    #region Parameters

    [SerializeField] public GameObject prfNode;
    [Range(1f, 100000f)]
    public int nodeResolution = 100;
    [Range(1f, 100f)]
    public float graphWidth = 1f;
    [SerializeField] public SineParameters parameters;
    #endregion

    #region RunTimeVariables

    [SerializeField] protected List<GameObject> instacedNodes;
    protected GameObject nodesInstance;// auxiliar / local variabler to work with 
    protected Vector3 nodePosition;

    #endregion

    #region UnityMethods

    private void OnDrawGizmos() //update() in editor mode for a MonoBeheviour
    {
        for (int i = 0; i < instacedNodes.Count; i++)
        {
            nodesInstance = instacedNodes[i];
            nodePosition = nodesInstance.transform.localPosition;
            nodePosition.y = parameters.A * Mathf.Sin(parameters.B * nodePosition.x + parameters.C) + parameters.D;
            // a- sen(b*c)+D
            nodesInstance.transform.localPosition = nodePosition;
        }
    }

    #endregion

    public void CreateSineGrapher()
    {
        if (instacedNodes != null)
        {
            DestroySineGrapher();
        }
        instacedNodes = new List<GameObject>();
        for (int i = 0; i < nodeResolution; i++)
        {
            nodesInstance = Instantiate(prfNode);
            nodesInstance.transform.parent = this.transform;
            nodesInstance.transform.localPosition = Vector3.right * ((float)i * graphWidth / (float)nodeResolution);
            //resolution es de cuantos nodos somos ,i cada nodo y grapwidth es el largo de cuantos nodos son
            // checar que es itinerador y sus weas :3
            instacedNodes.Add(Instantiate(prfNode));

        }
    }
    public void DestroySineGrapher()
    {
        for (int i = nodeResolution - 1; i >= 0; i--)
        {
            nodesInstance = instacedNodes[i];
            instacedNodes.Remove(nodesInstance);
            DestroyImmediate(nodesInstance);// destroy an instance of an object editor mode 
        }
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
