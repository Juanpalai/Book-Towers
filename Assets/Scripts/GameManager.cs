using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Slider _strongholds_of_player; //プレイヤーの拠点
    [SerializeField]
    private Slider _strongholds_of_enemy;

    [SerializeField]
    private float MAX_HP = 100.0f;

    public struct HOME_DATA {
        public float hp;
	};

    private HOME_DATA _player;
    private HOME_DATA _enemy;

    private static int _damage_to_enemy;
    private static int _damage_to_player;
    // Start is called before the first frame update
    void Start()
    {
        _strongholds_of_player = GameObject.Find( "HP_Player" ).GetComponent<Slider>( );
        _strongholds_of_enemy = GameObject.Find( "HP_Enemy" ).GetComponent<Slider>( );
        _damage_to_player = 0;
        _damage_to_enemy = 0;
        dataInit( );
        StartCoroutine( homeBloodLoop( ) );
    }

    // Update is called once per frame
    void Update()
    {
        strongHoldsLoop( );
        changeScene( );
        QuitGame( );
    }

    private void dataInit( ) {
        _player.hp = MAX_HP;
        _enemy.hp = MAX_HP;
    }

    private void strongHoldsLoop( ) {
        playerSettlement( );
        enemySettlement( );
	}

    public static void setDamageToPlayer( int damage ) {
        _damage_to_player = damage;
	}

    public static void setDamageToEnemy( int damage ) {
        _damage_to_enemy = damage;
    }

    private void playerSettlement( ) {
        if ( _damage_to_player == 0 ) {
            return;
        }
        _player.hp -= _damage_to_player;
        Debug.Log( _player.hp );
        _damage_to_player = 0;
    }

    private void enemySettlement( ) {
        if(_damage_to_enemy == 0 ) {
            return;
		}
        _enemy.hp -= _damage_to_enemy;
        _damage_to_enemy = 0;
	}

    private void changeScene( ) {
        if(_player.hp <= 0 ) {
            SceneManager.LoadScene( "GameOver" );
		}

        if( _enemy.hp <= 0 ) {
            SceneManager.LoadScene( "GameClear" );
		}
	}

    private IEnumerator homeBloodLoop( ) {
        _strongholds_of_player.value = _player.hp / MAX_HP;
        _strongholds_of_enemy.value = _enemy.hp / MAX_HP;
        yield return 0;
        StartCoroutine( homeBloodLoop( ) );
	}

    private void QuitGame( ) {
        if (Input.GetKey(KeyCode.Escape)) Quit();
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }
}
