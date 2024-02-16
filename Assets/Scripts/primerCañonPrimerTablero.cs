using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon1 : MonoBehaviour
{
    public int salud = 3;
    void Start()
    {

    }

    public void RecibirDaño()
    {
        salud--;

 
        if (salud <= 0)
        {
            DestruirBarco();
        }
    }


    void DestruirBarco()
    {
        Debug.Log("Barco destruido");
        Destroy(gameObject);
    }

    public void ColocarEnPosicion(Vector3 posicion)
    {
        transform.position = posicion;
    }
}
