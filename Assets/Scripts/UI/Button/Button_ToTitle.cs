using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_ToTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>( ).onClick.AddListener( delegate ( ) {
            SceneManager.LoadScene( "Title" );
        } );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
