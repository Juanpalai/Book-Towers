using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UnitCreateManager : SelectedCardUIC {
	[SerializeField]
	public Character _noun; //キャラクター名詞

	[SerializeField]
	public Character _verbs; //キャラクター動詞

	[SerializeField]
	public Character _adijective; //キャラクター形容詞

	[SerializeField]
	public LayerMask _clickobjLayer;

	private List<Character> _noun_pool;
	private List<Character> _verbs_pool;
	private List<Character> _adiective_pool;

	private GameObject _preview_obj;

	private Transform _efficient_point;

	private void Awake( ) {
		_noun_pool = new List<Character>( );
		_verbs_pool = new List<Character>( );
		_adiective_pool = new List<Character>( );
		initUnit( );
	}
	// Start is called before the first frame update
	private void Start( ) {
		setPreviewObjNull( );
	}

	// Update is called once per frame
	private void Update( ) {
		if ( !isPointOnUI( ) &&
			!isPlayerDealCardNull( ) &&
			Input.GetMouseButtonDown( 0 ) ) {
			if ( isClickOnEfficientPos( ) ) {
				createUnit( );
			}
		}

		unitPreview( );
	}

	private void unitPreview( ) {
		if ( isHide( ) ) {
			return;
		}
		previewUpd( );
	}
	private void delPreviewObj( ) {
		Destroy( _preview_obj );
	}
	private void setPreviewObjNull( ) {
		_preview_obj = null;
	}

	private bool isEfficientPos( ) {
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hit;
		if ( Physics.Raycast( ray, out hit, 50, _clickobjLayer.value ) ) {
			if ( hit.collider != null && hit.collider.tag == "GridNode" ) {
				_efficient_point = hit.collider.transform;
				return true;
			}
		}
		_efficient_point = null;
		//_preview_obj = null;隐藏比较好
		setPreviewObjHide( );
		return false;
	}

	private bool isClickOnEfficientPos( ) {
		if ( isEfficientPos( ) ) {
			return true;
		}
		setPlayerDealCardNull( );
		setPreviewObjNull( );
		return false;
	}

	private void previewUpd( ) {
		if( isPlayerDealCardNull( ) ) {
			setPreviewObjNull( );
		}
		createPreviewObj( );
		if ( isEfficientPos( ) && !isPreviewObjNull( ) ) {
			setPreviewObjPos( );
		}
	}


	private void createPreviewObj( ) {
		if ( isPlayerDealCardNull( ) || !isPreviewObjNull( ) ) {
			return;
		}

		Character unit = getUnit( _my_player.dealCard );
		Character obj = Instantiate( unit );
		obj.transform.rotation = Quaternion.Euler( 0.0f, 90.0f, 0.0f );
		obj.setLife( getCardHp( _my_player.dealCard ) );
		obj.setAttackPower( getCardAttackPower( _my_player.dealCard ) );
		_preview_obj = obj.gameObject;
	}

	private void setPreviewObjPos( ) {
		if ( _efficient_point == null ) {
			return;
		}

		_preview_obj.transform.position = _efficient_point.position;
		setPreviewObjShow( );
	}

	private bool isPreviewObjNull( ) {
		if ( _preview_obj == null ) {
			return true;
		}

		return false;
	}

	private void setPreviewObjShow( ) {
		if ( isPreviewObjNull( ) ) {
			return;
		}
		_preview_obj.SetActive( true );
	}

	private void setPreviewObjHide( ) {
		if ( isPreviewObjNull( ) ) {
			return;
		}
		_preview_obj.SetActive( false );
	}

	private bool isPointOnUI( ) {
		if( EventSystem.current.IsPointerOverGameObject( ) ) {
			return true;
		}
		return false;
	}

	private void initUnit( ) {
		nounInit( );
		verbsInit( );
		adjectiveInit( );
	}
	private void nounInit( ) {
		Object[ ] obj = Resources.LoadAll( "Prefabs/UnitMeisi" );
		//Debug.Log( obj.Length );
		foreach ( GameObject chara in obj ) {
			_noun_pool.Add( chara.GetComponent<Character>( ) );
		}
	}

	private void verbsInit( ) {
		Object[ ] obj = Resources.LoadAll( "Prefabs/UnitDousi" );
		//Debug.Log( obj.Length );
		foreach ( GameObject chara in obj ) {
			_verbs_pool.Add( chara.GetComponent< Character >( ) );
		}
	}

	private void adjectiveInit( ) {
		Object[ ] obj = Resources.LoadAll( "Prefabs/UnitKeiyousi" );
		//Debug.Log( obj.Length );
		foreach ( GameObject chara in obj ) {
			_adiective_pool.Add( chara.GetComponent< Character >( ) );
		}
	}

	private void createUnit( ) {
		Character obj = _preview_obj.GetComponent<Character>( );
		obj.setUnitEffective( );
		obj.transform.position = _efficient_point.position;
		delPreviewObj( );
		setPreviewObjNull( );
		unitIsCreated( );
	}

	private Character getUnit( Image card ) {
		Character character = null;
		string card_type = getTypeOfreplacementCard( card );
		switch( card_type ) {
			case "NOUN":
				character = findUnitFormNounPool( card );
				break;
			case "VERBS":
				character = findUnitFormVerbsPool( card );
				break;
			case "ADJECTIVE":
				character = findUnitFormAdjectivePool( card );
				break;
		}
		return character;
	}

	private Character findUnitFormNounPool( Image card ) {
		//Character noun = null;
		foreach ( Character chara in _noun_pool ) {
			if( chara.name == card.sprite.name ) {
				return chara;
			}
		}
		return null;
	}

	private Character findUnitFormVerbsPool( Image card ) {
		//Character verbs = null;
		foreach ( Character chara in _verbs_pool ) {
			if ( chara.name == card.sprite.name ) {
				return chara;
			}
		}
		return null;
	}
	private Character findUnitFormAdjectivePool( Image card ) {
		//Character adjective = null;
		foreach ( Character chara in _adiective_pool ) {
			if ( chara.name == card.sprite.name ) {
				return chara;
			}
		}
		return null;
	}
}
