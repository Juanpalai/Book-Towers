using UnityEngine;
using UnityEngine.UI;

public class CostManager : MonoBehaviour {
	private float WRITING_ABILITY_MAX = 10.0f;
	private float RECOVERY_TIME = 2.0f;
	//文章力
	static private int _writing_ability;
	//文章力を表示
	private Text _text_writing_ability;

	private int _writing_ability_before;

	private float _last_time;
	static private int _cost;

	private void Start( ) {
		_writing_ability = 5;
		_writing_ability_before = _writing_ability;

		_text_writing_ability = GameObject.Find( "Cost" ).GetComponent<Text>( );
		_text_writing_ability.text = _writing_ability.ToString( );
		//時間計算（前回の回復の時間）
		_last_time = Time.time;
		//コストをもらうために変数を定義
		_cost = 0;
	}

	private void Update( ) {
		showWritingAbility( );
		autoGrowth( );
		expenseSettlement( );

	}

	void autoGrowth( ) {
		//文章力の上限は10
		if ( _writing_ability == WRITING_ABILITY_MAX ) {
			return;
		}

		if ( Time.time - _last_time >= RECOVERY_TIME ) {
			_writing_ability++;
			_last_time = Time.time;
		}
	}
	//文章力を表示する
	void showWritingAbility( ) {
		if( _writing_ability == _writing_ability_before ) {
			return;
		}
		_text_writing_ability.text = _writing_ability.ToString( );
		_writing_ability_before = _writing_ability;
	}
	//プレーヤーの文章力の総量からコストを引きます
	void expenseSettlement( ) {
		if ( _cost == 0 ) {
			return;
		}
		_writing_ability -= _cost;
		_cost = 0;
	}

    public static void setCost( int cost ) {
		_cost = cost;
	}

	public static int getWritingAbility( ) {
		return _writing_ability;
	}
}
