using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm_controller : MonoBehaviour
{
    private AudioSource _my_source;
    // Start is called before the first frame update
    void Start()
    {
        _my_source = GetComponent<AudioSource>( );
        _my_source.Play( );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
