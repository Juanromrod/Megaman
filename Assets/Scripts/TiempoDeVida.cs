using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempoDeVida : MonoBehaviour
{
    [SerializeField] private float tiempo = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tiempo);
    }

}
