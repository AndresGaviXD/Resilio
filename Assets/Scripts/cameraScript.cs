using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject jugador;

    private float target_posX;
    private float target_posY;

    private float posX;
    private float posY;

    public float derechaMax;
    public float izquierdaMax;

    public float alturaMax;
    public float alturaMin;

    public float speed;
    public bool encendida = true;

    void Awake()
    {
        posX = target_posX + derechaMax;
        posY = target_posY + alturaMin;
        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1),1);
    }

    void Move_Cam()
    {
        if (encendida)
        {
            if (jugador)
            {
                target_posX = jugador.transform.position.x;
                target_posY = jugador.transform.position.y;

                if (target_posX > derechaMax && target_posX < izquierdaMax)
                {
                    posX = target_posX;

                }
                if (target_posY < alturaMax && target_posY > alturaMin)
                {
                    posY = target_posY;
                }
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), speed*Time.deltaTime);
        }
    }


    void Update()
    {
        Move_Cam();
    }
}
