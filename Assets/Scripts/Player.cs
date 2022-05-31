using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    bool wall;
    [SerializeField] float jumpForce;
    Rigidbody2D myBody;
    Animator myAnim;
    public int enemigos;
    private int contene;
    public TMP_Text cantene;
    private bool isGrounded = true;
    private Gun scriptgun;
    public bool giro;
    public bool gameover = false;
    [SerializeField] AudioClip sonidoMuerteEnemigo;
    [SerializeField] AudioClip sonidoMuerte;
    [SerializeField] AudioClip sonidoSalto;
    AudioSource reproductor;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        reproductor = GetComponent<AudioSource>();
        scriptgun = GameObject.Find("Gun").GetComponent<Gun>();
    }

    IEnumerator TiempoDeGracia()
    {
        while (myAnim.GetLayerWeight(1) == 1)
        {
            yield return new WaitForSeconds(1.5f);
            myAnim.SetLayerWeight(1, 0);
        }
    }

    IEnumerator GameOver()
    {
        while (gameover == true)
        {
            yield return new WaitForSeconds(0.01f);
            reproductor.PlayOneShot(sonidoMuerte);
            yield return new WaitForSeconds(0.01f);
            ResetScene();
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(0.015f);
        SceneManager.LoadScene("Win");
        Time.timeScale = 1f;
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1.3f, Color.red);
        isGrounded = (ray.collider != null);
        Jump();
        Fire();
        win();
        kill();
    }

    void kill()
    {
        if (contene > enemigos)
        {
            reproductor.PlayOneShot(sonidoMuerteEnemigo);
            contene = enemigos;
        } else
        {
            contene = enemigos;
        }
    }
    private void win()
    {
        if (enemigos == 0)
        {
            cantene.text = "Enemigos restantes: " + enemigos;
            Time.timeScale = 0.01f;
            gameover = true;
            StartCoroutine(Win());
        } else
        {
            cantene.text = "Enemigos restantes: "+enemigos;
        }
    }

    void Fire()
    {
        if(Input.GetKeyDown(KeyCode.Z) && scriptgun.fire)
        {
            myAnim.SetLayerWeight(1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            StartCoroutine(TiempoDeGracia());
        }

    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                reproductor.PlayOneShot(sonidoSalto);
            }
        }
        if (myBody.velocity.y != 0 && !isGrounded)
            myAnim.SetBool("isJumping", true);
        else
            myAnim.SetBool("isJumping", false);
    }

    private void FixedUpdate()
    {
        float dirH = Input.GetAxis("Horizontal");

        if (dirH != 0)
        {
            myAnim.SetBool("isRunning", true);
            if(dirH < 0)
            {
                transform.localScale = new Vector2(-1,1);
                giro = true;
            }
            else 
            {
                transform.localScale = new Vector2(1, 1);
                giro = false;
            }
        }

        else
        {
            myAnim.SetBool("isRunning", false);
        }

        if (wall)
        {
            myBody.velocity = new Vector2(0, myBody.velocity.y);
        }
        else
        {
            myBody.velocity = new Vector2(dirH * speed, myBody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo" || collision.gameObject.tag == "Proyectil" || collision.gameObject.tag == "Limite")
        {
            gameover = true;
            myAnim.SetBool("isDead", true);
            Time.timeScale = 0.01f;
            StartCoroutine(GameOver());
        }
        if (collision.gameObject.tag == "Pared")
        {
            wall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pared")
        {
            wall = false;
        }
    }


}
