using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioNivel : MonoBehaviour
{
    public GameObject menuPanel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cambio")
        {
            menuPanel.SetActive(true); // Activa el menú
                                       // Opcionalmente, pausa el juego si lo deseas
                                       // Time.timeScale = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cambio")
        {
            menuPanel.SetActive(false); // Desactiva el menú si el jugador se aleja
                                        // Time.timeScale = 1; // Reanuda el juego si lo pausaste anteriormente
        }
    }
}
