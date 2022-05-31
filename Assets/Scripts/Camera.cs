using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Player scriptplayer;
    AudioSource reproductor;
    // Start is called before the first frame update
    void Start()
    {
        scriptplayer = GameObject.Find("Player").GetComponent<Player>();
        reproductor = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Music();
    }

    void Music()
    {
        if (scriptplayer.gameover == true)
        {
            reproductor.Pause();
        } 
    }
}
