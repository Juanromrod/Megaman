using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject efectodemuerte;
    [SerializeField] GameObject destroyed;
    [SerializeField] GameObject bullet;
    [SerializeField] float distance;
    [SerializeField] float vida = 5;
    Rigidbody2D myBody;
    [SerializeField] bool Inverted; 
    [SerializeField] float firerate;
    private float nextfire = 0f;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        if (vida == 0)
        {
            Muerte();
            Debug.Log(this.gameObject.name + " ha sido eliminado.");
        }
    }

    private void Fire()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.left, distance, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.left * distance, Color.red);
        if (ray.collider != null)
        {
            if (ray.collider.gameObject.name == "Player" && Time.time > nextfire)
            {
                nextfire = Time.time + firerate;
                Instantiate(bullet, transform.position, Quaternion.identity);
            } 
        }
    }

    private void Muerte()
    {
        Instantiate(efectodemuerte, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        if (Inverted)
        {
            Instantiate(destroyed, new Vector3(myBody.position.x, myBody.position.y + 0.5f), Quaternion.Inverse(transform.rotation));
        } else
        {
            Instantiate(destroyed, new Vector3(myBody.position.x, myBody.position.y - 0.5f), Quaternion.identity);
        }
        /*myAnim.SetBool("isDestroyed", true);
        myBody.position = new Vector2(myBody.position.x, myBody.position.y - 0.5f);*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            vida = vida - 1;
            Debug.Log("Vidas restantes de " + this.gameObject.name + ": " + vida);
        }
    }

}
