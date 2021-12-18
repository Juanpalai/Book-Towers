using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public PathNode ThisNode;
    public PathNode ThatNode;

    public void SetNode( PathNode _node ) {
        if( ThatNode != null ) {
            ThatNode.ThisNode = null;
            ThatNode = _node;
            _node.ThisNode = this;
        }
    }
}
