using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    Rigidbody2D myBody;
    Animator myAnim;
    private bool isGrounded = true;
    private Gun scriptgun;
    public bool giro;
    public bool gameover = false;
    [SerializeField] AudioClip sonidoMuerte;
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
            yield return new WaitForSeconds(2);
            reproductor.PlayOneShot(sonidoMuerte);
            yield return new WaitForSeconds(1);
            ResetScene();
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1.3f, Color.red);
        isGrounded = (ray.collider != null);
        Jump();
        Fire();
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

        if (gameover)
        {
            myBody.velocity = new Vector2(0, myBody.velocity.y);
            transform.localScale = new Vector2(1, 1);
            myBody.bodyType = RigidbodyType2D.Static;
        } else
        {
            myBody.velocity = new Vector2(dirH * speed, myBody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisionando con: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Enemigo" || collision.gameObject.tag == "Proyectil")
        {
            gameover = true;
            myAnim.SetBool("isDead", true);
            StartCoroutine(GameOver());
        }
    }
}
