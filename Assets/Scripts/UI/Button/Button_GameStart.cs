using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_GameStart : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>( ).onClick.AddListener( delegate ( ) {
            SceneManager.LoadScene( "Stage1" );
        } );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
