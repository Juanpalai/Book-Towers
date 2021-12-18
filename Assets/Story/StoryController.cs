using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour {
	//スクリーンの横と高さ
	public int SCREEN_WIDTH;
	public int SCREEN_HEIGHT;
	//ImageGameObject容器
	public List<Image> _images;
	//Image容器
	public List<Sprite> _sprites;
	//移動スピード
	public float _move_speed = 1000f;
	//イメージインデックス
	private int _idx;
	//ボタンクリック
	private bool _is_click;

	// Start is called before the first frame update
	private void Start( ) {
		//初期化
		_idx = _sprites.Count - 1;
		_is_click = false;
		for( int i = 0; i < _images.Count; i++ ) {
			_images[ i ].sprite = _sprites[ _idx - i ];
		}
		_images[ 0 ].transform.SetAsLastSibling( );
	}

	// Update is called once per frame
	private void Update( ) {
		clickProcess( );
		storyProcess( );
	}

	private void imageAnim( Image image ) {
		if( isOutOfScreen( image ) ) {
			_is_click = false;
			_idx -= 1;
			putLastAndInit( image );
			return;
		}
		Vector3 vet = new Vector3(
			image.rectTransform.position.x - Time.deltaTime * _move_speed,
			image.rectTransform.position.y,
			image.rectTransform.position.z
			);
		image.rectTransform.position = vet;
	}

	//当面のImageが画面外のチェック
	private bool isOutOfScreen( Image image ) {
		if( image.rectTransform.position.x <= -SCREEN_WIDTH ) {
			return true;
		}
		return false;
	}

	//すべての処理
	private void storyProcess( ) {
		if( !_is_click ) {
			return;
		}
		imageAnim( _images[ ( _idx + _sprites.Count - 1 ) % 2 ] );
	}

	private void clickProcess( ) {
		if( Input.GetMouseButton( 0 ) ) {
			if( _is_click ) {
				return;
			}
			if( _idx == 0 ) {
				SceneManager.LoadScene( "Stage1" );
				return;
			}
			_is_click = true;
		}
	}

	//裏へ
	private void putLastAndInit( Image image ) {
		if( _idx == 0 ) {
			return;
		}
		image.transform.SetAsFirstSibling( );
		image.sprite = _sprites[ _idx - 1 ];
		image.rectTransform.position = new Vector3( SCREEN_WIDTH / 2.0f, SCREEN_HEIGHT / 2.0f, 0.0f );
	}
}
