using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]float velx = 5;
    [SerializeField] GameObject destroy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction();
    }

    void direction()
    {
        transform.Translate(Vector2.right * 5 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisionando con: " + collision.gameObject.name);
        Destruction();
    }

    private void Destruction()
    {
        Instantiate(destroy, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}