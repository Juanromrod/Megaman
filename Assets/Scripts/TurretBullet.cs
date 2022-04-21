using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    [SerializeField] float velx;
    Rigidbody2D myBody;
    [SerializeField] GameObject destroy;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myBody.velocity = new Vector2(velx, myBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destruction();
    }

    private void Destruction()
    {
        Instantiate(destroy, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
