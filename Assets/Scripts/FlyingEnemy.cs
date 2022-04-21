using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    AIPath myPath;
    [SerializeField] GameObject efectodemuerte;
    [SerializeField] float vida = 5;
    private Player scriptplayer;

    // Start is called before the first frame update
    void Start()
    {
        myPath = GetComponent<AIPath>();
        scriptplayer = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Perseguir();
        if (vida == 0)
        {
            Muerte();
            Debug.Log(this.gameObject.name + " ha sido eliminado.");
        }
        if (scriptplayer.gameover == true)
        {
            myPath.isStopped = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            vida = vida - 1;
            Debug.Log("Vidas restantes de " + this.gameObject.name + ": " + vida);
        } else if (collision.gameObject.name == "Player")
        {
            myPath.isStopped = true;
        }
    }

    private void Perseguir()
    {
        //Alternativa 1 - vector2.distance
        /*float d = Vector2.Distance(transform.position, player.transform.position);
        if (d < 8)
        {

        }
        //Debug.Log("Distancia al jugador: " + d);*/
        Debug.DrawLine(transform.position, player.transform.position, Color.red);

        //Alternativa 2 - Overlapcircle
        Collider2D col = Physics2D.OverlapCircle(transform.position, 5f,
                                                 LayerMask.GetMask("Player"));

        if (col != null)
        {
            //Debug.Log("Jugador dentro del radio");
            myPath.isStopped = false;
        }
        else
        {
            myPath.isStopped = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
    private void Muerte()
    {
        Instantiate(efectodemuerte, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
