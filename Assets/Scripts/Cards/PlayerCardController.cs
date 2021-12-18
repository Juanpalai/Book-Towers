using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlayerCardController : CardDeckController {
	private Image _reduction_memo;
	public struct PLAYER_DATA {
		public Image[ ] headCards;
		public Image[ ] previewCards;
		public int numOfPreviewCards;
		public Image dealCard;
	}

	/*プレーヤーカードの上限*/
	public const int NUM_OF_PLAYER_CARDS = 4;

	/*内覧可能のカードの上限*/
	public const int NUM_OF_PREVIEW_CARDS = 3;

	/*プレーヤー宣言*/
	public static PLAYER_DATA _my_player;

	[SerializeField]
	public Vector2 SIZE_OF_MINI = new Vector2(105f, 140f);
	[SerializeField]
	public Vector2 SIZE_OF_PREVIEW = new Vector2( 105f, 140f );

	/*UIManager*/
	[SerializeField] 
	private Canvas _my_canvas;

	private int Layer_OF_CARD = 9;
	[SerializeField]
	private bool _is_mouse_point_on_card;
	[SerializeField]
	private static bool _is_unit_created;

	/*カードの位置*/
	[SerializeField]
	public Vector3[ ] _my_card_slots = new Vector3[ ( int )CARD_SLOTS.MAX ];
	[SerializeField]
	public Vector3[ ] _preview_card_slots = new Vector3[ ( int )PREVIEW_CARD_SLOTS.MAX ];

	private void Awake( ) {
		_my_canvas = GameObject.Find( "Canvas" ).GetComponent<Canvas>( );
	}
	// Start is called before the first frame update
	private void Start( ) {
		/*プレーヤーのデータを初期化*/
		_my_player.headCards = new Image[ NUM_OF_PLAYER_CARDS ];
		_my_player.previewCards = new Image[ NUM_OF_PREVIEW_CARDS ];
		_my_player.numOfPreviewCards = 0;
		_my_player.dealCard = null;
		_is_unit_created = false;
		/*カードの位置を初期化*/
		//_my_card_slots[ ( int )CARD_SLOTS.SLOT1 ] = //new Vector3( -121f, -163f, 0f );
		//_my_card_slots[ ( int )CARD_SLOTS.SLOT2 ] = //new Vector3( -39f, -163f, 0f );
		//_my_card_slots[ ( int )CARD_SLOTS.SLOT3 ] = //new Vector3( 41f, -163f, 0f );
		//_my_card_slots[ ( int )CARD_SLOTS.SLOT4 ] = //new Vector3( 123f, -163f, 0f );
		//
		//_preview_card_slots[ ( int )//PREVIEW_CARD_SLOTS.SLOT1 ] = new Vector3//( -239f, -177.8f, 0f );
		//_preview_card_slots[ ( int )//PREVIEW_CARD_SLOTS.SLOT2 ] = new Vector3//( -230f, -177.8f, 0f );
		//_preview_card_slots[ ( int )PREVIEW_CARD_SLOTS.SLOT3 ] = new Vector3( -219f, -177.8f, 0f );

		//readExcel
		readExcelStream( );

		//メモ用変数初期化
		_reduction_memo = null;

		cardDeckInitialize( );
		firstTimeDisplaySignage( );
	}

	// Update is called once per frame
	private void Update( ) {
		//cardReduction( );
		dealCards( );
		costSettlement( );
	}


	public void firstTimeDisplaySignage( ) {
		for ( int i = 0; i < NUM_OF_PLAYER_CARDS; i++ ) {
			Image image = getCard( );
			_my_player.headCards[ i ] = Instantiate( image );
			_my_player.headCards[ i ].transform.SetParent( _my_canvas.transform );
			_my_player.headCards[ i ].rectTransform.sizeDelta = SIZE_OF_PREVIEW;
			_my_player.headCards[ i ].transform.localPosition = _my_card_slots[ i ];
		}

		for ( int i = 0; i < NUM_OF_PREVIEW_CARDS; i++ ) {
			Image image = getCard( );
			_my_player.previewCards[ i ] = Instantiate( image );
			_my_player.previewCards[ i ].rectTransform.sizeDelta = SIZE_OF_MINI;
			_my_player.previewCards[ i ].transform.SetParent( _my_canvas.transform );
			_my_player.previewCards[ i ].transform.localPosition = _preview_card_slots[ i ];
			_my_player.numOfPreviewCards++;
		}
	}

	//mousePointがどのUIImageの上にありますか
	public Image getOverUI( Canvas canvas ) {
		PointerEventData pointerEventData = new PointerEventData( EventSystem.current );
		pointerEventData.position = Input.mousePosition;
		GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>( );
		List<RaycastResult> results = new List<RaycastResult>( );
		gr.Raycast( pointerEventData, results );
		if ( results.Count != 0 && results[ 0 ].gameObject.GetComponent<Image>( ).gameObject.layer == Layer_OF_CARD ) {
			_is_mouse_point_on_card = true;
			return results[ 0 ].gameObject.GetComponent<Image>( );
		}
		_is_mouse_point_on_card = false;
		return null;
	}

	//プレーヤーがカードを出す
	public void dealCards( ) {
		Image dealcard = null;
		if ( Input.GetMouseButton( 0 )) {
			dealcard = getOverUI( _my_canvas );
		}

		if ( dealcard == null ||
			!checkPlayerLicence( dealcard ) ||
			!checkCost( dealcard ) ) {
			return;
		}
		_my_player.dealCard = dealcard;
		unitIsNotCreated( );
	}
	public bool checkPlayerLicence( Image image ) {
		for ( int i = 0; i < NUM_OF_PLAYER_CARDS; i++ ) {
			if ( image.transform.position == _my_player.headCards[ i ].transform.position ) {

				return true;
			}
		}
		return false;
	}

	//出したのカードを判断したら、消す
	public void deleteTheDealCard( ) {
		if ( isPlayerDealCardNull( ) ) {
			return;
		}
		Destroy( _my_player.dealCard.gameObject );
		for ( int i = 0; i < NUM_OF_PLAYER_CARDS; i++ ) {
			if ( _my_player.headCards[ i ] == _my_player.dealCard ) {
				_my_player.headCards[ i ] = null;
				replacementCardToPlayerHeadCards( i );
				break;
			}
		}
		//getTypeOfreplacementCard( _my_player.dealCard );
		_my_player.dealCard = null;
	}

	private void replacementCardToPlayerHeadCards( int idx ) {
		_my_player.headCards[ idx ] = _my_player.previewCards[ NUM_OF_PREVIEW_CARDS - 1 ];
		_my_player.previewCards[ NUM_OF_PREVIEW_CARDS - 1 ] = null;
		_my_player.headCards[ idx ].transform.SetParent( _my_canvas.transform );
		_my_player.headCards[ idx ].transform.localPosition = _my_card_slots[ idx ];
		_my_player.headCards[ idx ].rectTransform.sizeDelta = SIZE_OF_PREVIEW;

		for ( int i = 0; i < NUM_OF_PREVIEW_CARDS - 1; i++ ) {
			_my_player.previewCards[ NUM_OF_PREVIEW_CARDS - 1 - i ] = _my_player.previewCards[ ( NUM_OF_PREVIEW_CARDS - 1 ) - ( i + 1 ) ];
		}
		Image image = getCard( );
		_my_player.previewCards[ 0 ] = Instantiate( image );
		_my_player.previewCards[ 0 ].transform.SetParent( _my_canvas.transform );
		_my_player.previewCards[ 0 ].transform.localPosition = _preview_card_slots[ 0 ];
		_my_player.previewCards[ 0 ].rectTransform.sizeDelta = SIZE_OF_MINI;
		_my_player.previewCards[ 0 ].transform.SetAsFirstSibling( );

		for ( int i = 0; i < NUM_OF_PREVIEW_CARDS; i++ ) {
			_my_player.previewCards[ i ].transform.localPosition = _preview_card_slots[ i ];
		}
	}

	private void cardReduction( ) {
		Image image = getOverUI( _my_canvas );
		cardShrink( );
		if ( image == null ) {
			return;
		}
		cardEnlarge( image );
	}

	private void cardEnlarge( Image image ) {
		if ( image == _reduction_memo ) {
			return;
		}
		image.rectTransform.sizeDelta = SIZE_OF_PREVIEW;
		_reduction_memo = image;
	}

	private void cardShrink( ) {
		if ( _reduction_memo == null ) {
			return;
		}
		_reduction_memo.rectTransform.sizeDelta = SIZE_OF_MINI;
		_reduction_memo = null;
	}

	private bool checkCost( Image card ) {
		int card_cost = getCardCost( card );
		if ( CostManager.getWritingAbility( ) >= card_cost ) {
			return true;
		}

		return false;
	}

	private void costSettlement( ) {
		if ( !isUnitCreated( )) {
			return;
		}
		//CostManagerにコストを渡ります
		determineTheCost( _my_player.dealCard );
		//出すのカードを消して
		deleteTheDealCard( );
	}
	public void determineTheCost( Image card ) {
		if( card == null ) {
			return;
		}
		int card_cost = getCardCost( card );
		CostManager.setCost( card_cost );
	}
	public bool isMousePointOnCard( ) {
		if( _is_mouse_point_on_card ) {
			return true;
		}
		return false;
	}

	public void setPlayerDealCardNull( ) {
		_my_player.dealCard = null;
	}

	public bool isPlayerDealCardNull( ) {
		if( _my_player.dealCard == null ) {
			return true;
		}
		return false;
	}
	
	private bool isUnitCreated( ) {
		return _is_unit_created;
	}

	public void unitIsCreated( ) {
		_is_unit_created = true;
	}

	public void unitIsNotCreated( ) {
		_is_unit_created = false;
	}

}