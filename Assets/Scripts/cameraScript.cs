using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject jugador;


    void Update()
    {
        Vector3 position = transform.position;
        position.x = jugador.transform.position.x;
        transform.position = position;
    }
}
