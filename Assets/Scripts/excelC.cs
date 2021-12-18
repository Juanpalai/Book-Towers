using UnityEngine;
using System.IO;
using System.Data;
using ExcelDataReader;
using UnityEngine.UI;

public class ExcelC : MonoBehaviour {
    //excelData of cards
    private static DataSet data_of_cards_excel;
    //excelData of enemy
    private DataSet data_of_enemy_excel;

    public string getTypeOfreplacementCard( Image card ) {
        int card_y = getYOfCardExcel( card );
        string card_type = data_of_cards_excel.Tables[ 0 ].Rows[ card_y ][ 1 ].ToString( );
        return card_type;
    }

    private int getYOfCardExcel( Image card ) {
        int y = data_of_cards_excel.Tables[ 0 ].Rows.Count;
        int card_y = 0;
        for ( int i = 0; i < y; i++ ) {
            if ( card.tag == data_of_cards_excel.Tables[ 0 ].Rows[ i ][ 0 ].ToString( ) ) {
                card_y = i;
                break;
            }
        }

        return card_y;
    }

    public void readExcelStream( ) {
        string filePath = Application.dataPath + "\\Excel\\"+"cards.xlsx";
        FileStream stream = File.Open( filePath, FileMode.Open, FileAccess.Read );
        IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader( stream );

        data_of_cards_excel = excelDataReader.AsDataSet( );

        excelDataReader.Close( );
    }
    public int getCardCost( Image card ) {
        int card_y = getYOfCardExcel( card );
        string num = data_of_cards_excel.Tables[ 0 ].Rows[ card_y ][ 2 ].ToString( );
        int card_cost = System.Int32.Parse( num );
        return card_cost;
    }

    public int getCardHp( Image card ) {
        int card_y = getYOfCardExcel( card );
        string num = data_of_cards_excel.Tables[ 0 ].Rows[ card_y ][ 3 ].ToString( ).ToString( );
        int hp = System.Int32.Parse( num );
        return hp;
    }

    public int getCardAttackPower( Image card ) {
        int card_y = getYOfCardExcel( card );
        string num = data_of_cards_excel.Tables[ 0 ].Rows[ card_y ][ 4 ].ToString( ).ToString( );
        int attack_power = System.Int32.Parse( num );
        return attack_power;
    }

    public void redEnemyExcelStream( ) {
        string filePath = Application.dataPath +"\\Excel\\"+ "enemy.xlsx";
        FileStream stream = File.Open( filePath, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader( stream);

        data_of_enemy_excel = excelDataReader.AsDataSet( );
        excelDataReader.Close( );
	}
    //敵出現のレーン
    public int getNumOfLine( int idx ) {
        string life = data_of_enemy_excel.Tables[1].Rows[idx][1].ToString( );

        return System.Int32.Parse( life );
	}

    public int getTimeOfEnemy( int idx ) {
        string time = data_of_enemy_excel.Tables[1].Rows[idx][2].ToString( );

        return System.Int32.Parse( time );
    }

    public string getEnemyType(int idx ) {
        string type = data_of_enemy_excel.Tables[ 1 ].Rows[ idx ][ 0 ].ToString( );

        return type;
    }

    private int getYForEnemy(string type ) {
        int y = 0;
        int y_max = data_of_enemy_excel.Tables[ 0 ].Rows.Count;
        for ( ; y < y_max; y++ ) {
            if(type == data_of_enemy_excel.Tables[ 0 ].Rows[ y ][ 0 ].ToString( ) ) {
                break;
			}
		}
        return y;
	}

    public int getEnemyLife( string type ) {
        int idx = getYForEnemy( type );
        string life = data_of_enemy_excel.Tables[ 0 ].Rows[ idx ][ 1 ].ToString( );

        return System.Int32.Parse( life );
    }

    public int getEnemyAttackPower( string type ) {
        int idx = getYForEnemy( type );
        string power = data_of_enemy_excel.Tables[ 0 ].Rows[ idx ][ 2 ].ToString( );

        return System.Int32.Parse( power );
    }
}
