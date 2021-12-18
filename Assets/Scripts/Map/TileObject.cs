using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public static TileObject Instance = null;
    public LayerMask tileLayer;
    public float tileSize = 1;
    public int xTileCount = 2;
    public int zTileCount = 2;
    public int[ ] data;
    [HideInInspector]
    public int dataID = 0;
    [HideInInspector]
    public bool debug = false;
    void Awake( ) {
        Instance = this;
    }
    public void Reset( ){
        data = new int[ xTileCount + zTileCount ];
    }
    public int getDataFromPosition( float pox, float poz ){
        int index = (int)(( pox - transform.position.x) / tileSize ) * zTileCount + ( int )( ( poz - transform.position.z ) / tileSize );

        if( index < 0 || index >= data.Length ){
            return 0;
        }

        return data[ index ];
    }

    public void setDataFromPosition( float pox, float poz, int number ){
        int index = ( int )( ( pox - transform.position.x) / tileSize) * zTileCount + ( int )( ( poz - transform.position.z ) / tileSize );

        if( index < 0 || index >= data.Length ){
            return;
        }
        data[ index ] = number;
    }

    void OnDrawgizmos( ){
        if( !debug ){
            return;
        }

        if( data == null ){
            Debug.Log("Please reset data first");
            return;
        }

        Vector3 pos =transform.position;

        for( int i = 0; i < xTileCount; i++){
            
        }
    }
}
