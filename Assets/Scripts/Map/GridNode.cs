using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GridNode : MonoBehaviour
{
    public enum NodeType{
        Canplace,
        CantPlace,
    }

    public NodeType GridNodeType = NodeType.Canplace;
}