using UnityEngine;

public class GridMap : MonoBehaviour {
	public static GridMap Instance = null;
	public int MapSizeX;
	public int MapSizeZ;

	[HideInInspector]
	private GameObject[ ] _mNodes;
	[HideInInspector]
	private GameObject[ ] _mPaths;

	[SerializeField]
	public GameObject _stage;

	private void Awake( ) {
		Instance = this;
		_mNodes = GameObject.FindGameObjectsWithTag( "GridNode" );
		_mPaths = GameObject.FindGameObjectsWithTag( "PathNode" );
	}

	void DrawGrid( ) {
		Gizmos.color = Color.black;
		float x = _stage.transform.localPosition.x;
		float z = _stage.transform.localPosition.z;
		for( float i = x; i <= MapSizeX; i++ ) {
			Gizmos.DrawLine( new Vector3( i, 0, 0 ), new Vector3( i, 0, MapSizeZ ) );
		}
		for( float j = z; j <= MapSizeZ; j++ ) {
			Gizmos.DrawLine( new Vector3( 0, 0, j ), new Vector3( MapSizeX, 0, j ) );
		}
	}

	void DrawColor( ) {
		if( _mNodes == null )
			return;
		foreach( GameObject go in _mNodes ) {
			Vector3 mPos = go.transform.position;
			if( go.GetComponent<GridNode>() != null ) {
				if( go.GetComponent<GridNode>().GridNodeType == GridNode.NodeType.Canplace ) {
					Gizmos.color = Color.green;
				} else if( go.GetComponent<GridNode>().GridNodeType == GridNode.NodeType.CantPlace ) {
					Gizmos.color = Color.red;
				}
				Gizmos.DrawCube( mPos, new Vector3( 1, 1, 1 ) );
			}
		}
	}

	void DrawPath( ) {
		Gizmos.color = Color.white;
		if( _mPaths == null )
			return;
		foreach( GameObject go in _mPaths ) {
			if( go.GetComponent<PathNode>() != null ) {
				PathNode node = go.GetComponent<PathNode>();
				if( node.ThatNode != null ) {
					Gizmos.DrawLine( node.transform.position, node.ThatNode.transform.position );
				}
			}
		}
	}

	private void OnDrawGizmos( ) {
		DrawGrid();
		//DrawColor();
		//DrawPath();
	}
}
