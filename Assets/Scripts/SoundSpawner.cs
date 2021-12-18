using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpawner : MonoBehaviour
{
    GameObject Controller;
    AudioController script;

    // Start is called before the first frame update
    void Start()
    {
        Controller = GameObject.Find("AudioController");
        script = Controller.GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            script.SEGameOver();
        }

    }
}
