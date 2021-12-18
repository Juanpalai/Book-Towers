using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : ExcelC {
	//
	private int NUM_OF_WAVE = 9;
	public GameObject _floor_01;
	//エネミー生成場所の基準
	private Vector3 _reference_pos = new Vector3( -8.14f, 0.11f, 10.21f );
	//次のところまでの距離の差
	private Vector3 _add_pos = new Vector3( 0f, 0f, 3.0f );
	private int _num_of_enemy;
	private float _last_time;
	//public GameObject _test_obj;
	[SerializeField]
	public Character _enemy_normal;
	[SerializeField]
	public Character _enemy_power;
	[SerializeField]
	public Character _enemy_shoter;

	private void Awake( ) {
		//_reference_pos = posInWold( _floor_01 );
	}
	// Start is called before the first frame update
	private void Start( ) {
		redEnemyExcelStream( );
		_num_of_enemy = 0;
		_last_time = Time.time;
	}

	// Update is called once per frame
	private void Update( ) {
		enemyCreate( );
	}

	private void enemyCreate( ) {
		if ( Time.time - _last_time < getTimeOfEnemy( _num_of_enemy ) ) {
			return;
		}
		_last_time = Time.time;
		enemyObjCreate( );
		_num_of_enemy++;
		if ( _num_of_enemy == NUM_OF_WAVE ) {
			_num_of_enemy = NUM_OF_WAVE - NUM_OF_WAVE;
		}
	}

	private void enemyObjCreate( ) {
		string type = getEnemyType( _num_of_enemy );
		Character obj = Instantiate( getEnemyObj( type ) );
		int numOfLine = getNumOfLine( _num_of_enemy ) - 1;
		Vector3 vtr = _reference_pos - ( numOfLine  * _add_pos );
		//EnemyData 設定
		obj.setLife( getEnemyLife( type ) );
		obj.setAttackPower( getEnemyAttackPower( type ) );
		obj.transform.position = vtr;
	}

	private Character getEnemyObj(string enemy ) {
		Character enemy_obj = null;

		switch( enemy ) {
			case "Normal":
				enemy_obj = _enemy_normal;
				break;
			case "Power":
				enemy_obj = _enemy_power;
				break;
			case "Shoter":
				enemy_obj = _enemy_shoter;
				break;
		}

		return enemy_obj;
	}

	/* 世界よりの座標計算*/
	//private Vector3 posInWold( GameObject obj ) {
	//	Vector3 vtr = new Vector3( );
	//	//List<Transform> objs = new List<Transform>( );
	//	//objs.Add( _floor_01.transform );
	//	//while ( objs[ objs.Count - 1 ].parent != null ) {
	//	//	objs.Add( objs[ objs.Count - 1 ].parent );
	//	//}
	//	//int num = objs.Count - 1;
	//	//vtr = objs[ num ].transform.position;
	//	//while ( num > 0 ) {
	//	//	Vector3 offer = new Vector3(
	//	//		objs[ num - 1 ].transform.localPosition.x * objs[ num ].transform.localScale.x,
	//	//		objs[ num - 1 ].transform.localPosition.y * objs[ num ].transform.localScale.y,
	//	//		objs[ num - 1 ].transform.localPosition.z * objs[ num ].transform.localScale.z
	//	//		);
	//	//	vtr = vtr + objs[ num ].rotation * offer;
	//	//	num--;
	//	//}

	//	Vector3 offer = new Vector3(
	//			_floor_01.transform.localPosition.x * _floor_01.transform.parent.localScale.x,
	//			_floor_01.transform.localPosition.y * _floor_01.transform.parent.localScale.y,
	//			_floor_01.transform.localPosition.z * _floor_01.transform.parent.localScale.z
	//			);
	//	vtr = _floor_01.transform.parent.position + _floor_01.transform.parent.rotation * offer;

	//	return vtr;
	//}
}
