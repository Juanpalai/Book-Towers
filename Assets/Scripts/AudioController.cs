using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip ButtonClick;
    public AudioClip GameClear;
    public AudioClip GameOver;

    public static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SEButtonClick()
    {
            audioSource.PlayOneShot(ButtonClick);
     
    }
    public void SEGameClear()
    {
        audioSource.PlayOneShot(GameClear);

    }
    public void SEGameOver()
    {
        audioSource.PlayOneShot(GameOver);

    }
}
