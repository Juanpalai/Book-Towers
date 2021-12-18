using UnityEngine;
using UnityEngine.UI;

public class CardDeckController : ExcelC {
    public enum CARD_TYPE {
        NOUN,
        VERBS,
        ADJECTIVE,
        MAX,
    };
    public struct CARD_DECK {
        public Image[ ] cards;
    };

    public enum CARD_SLOTS {
        SLOT1,
        SLOT2,
        SLOT3,
        SLOT4,
        MAX,
    };

    public enum PREVIEW_CARD_SLOTS {
        SLOT1,
        SLOT2,
        SLOT3,
        MAX,
    };
    [SerializeField]
    public CARD_DECK[ ] _my_card_deck = new CARD_DECK[ ( int )CARD_TYPE.MAX ];
    private void Start( ) {
    }
    public void cardDeckInitialize( ) {
        cardNounLoadIn( );
        cardVerbsLoadIn( );
        cardAdjectiveLoadIn( );
    }

    private void cardNounLoadIn( ) {
        object[ ] noun = Resources.LoadAll( "Cards/Noun", typeof( Image ) );
        _my_card_deck[ ( int )CARD_TYPE.NOUN ].cards = new Image[ noun.Length ];
        for( int i = 0; i < noun.Length; i++ ) {
            _my_card_deck[ ( int )CARD_TYPE.NOUN ].cards[ i ] = ( Image )noun[ i ];
        }
    }

    private void cardVerbsLoadIn( ) {
        object[ ] verbs = Resources.LoadAll( "Cards/Verbs", typeof( Image ) );
        _my_card_deck[ ( int )CARD_TYPE.VERBS ].cards = new Image[ verbs.Length ];
        for( int i = 0; i < verbs.Length; i++ ) {
            _my_card_deck[ ( int )CARD_TYPE.VERBS ].cards[ i ] = ( Image )verbs[ i ];
        }
    }

    private void cardAdjectiveLoadIn( ) {
        object[ ] adjective = Resources.LoadAll( "Cards/Adjective", typeof( Image ) );
        _my_card_deck[ ( int )CARD_TYPE.ADJECTIVE ].cards = new Image[ adjective.Length ];
        for( int i = 0; i < adjective.Length; i++ ) {
            _my_card_deck[ ( int )CARD_TYPE.ADJECTIVE ].cards[ i ] = ( Image )adjective[ i ];
        }
    }

    public Image getCard( ) {
        //Image image = null;
        //switch( Random.Range( 0, ( int )CARD_TYPE.MAX ) ) {
        //    case ( int )CARD_TYPE.NOUN:
        //        image = _my_card_deck[ ( int )CARD_TYPE.NOUN ].cards[ Random.Range( 0, _my_card_deck[ ( int )//CARD_TYPE.NOUN ].cards.Length ) ];
        //        break;
        //    case ( int )CARD_TYPE.VERBS:
        //        image = _my_card_deck[ ( int )CARD_TYPE.VERBS ].cards[ Random.Range( 0, _my_card_deck/[ ( int )/CARD_TYPE.VERBS ].cards.Length ) ];
        //        break;
        //    case ( int )CARD_TYPE.ADJECTIVE:
        //        image = _my_card_deck[ ( int )CARD_TYPE.ADJECTIVE ].cards[ Random.Range( 0, _my_card_deck//[ ( int )CARD_TYPE.ADJECTIVE ].cards.Length ) ];
        //        break;
        //}

        int index = Random.Range( ( int )CARD_TYPE.NOUN, ( int )CARD_TYPE.MAX );
        int max = _my_card_deck[ index ].cards.Length;

        Image image = _my_card_deck[ index ].cards[ Random.Range( 0, max ) ];
        return image;
    }

}
