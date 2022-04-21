using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float firerate;
    private float nextfire = 0f;
    [SerializeField] AudioClip sonidoDisparo;
    AudioSource reproductor;
    private Player scriptplayer;
    public bool fire;

    // Start is called before the first frame update
    void Start()
    {
        reproductor = GetComponent<AudioSource>();
        scriptplayer = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            fire = true;
            Instantiate(bullet, transform.position, transform.rotation);
            reproductor.PlayOneShot(sonidoDisparo);
        }

        if (scriptplayer.giro)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
