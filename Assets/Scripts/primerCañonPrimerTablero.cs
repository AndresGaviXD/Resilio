using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barco : MonoBehaviour
{
    public int salud = 3;

 
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
}
