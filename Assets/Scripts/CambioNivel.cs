using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CambioNivel : MonoBehaviour
{
    public GameObject menuPanel;
    public TextMeshProUGUI hiscoreText; // Asegúrate de asignar esto desde el editor con el objeto TextMeshProUGUI que muestra el Hiscore

    private void Start()
    {
        UpdateHiscoreText(); // Actualiza el Hiscore cada vez que se inicia el nivel
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "jugador") // Modificado para comprobar si el jugador entra en contacto con la puerta
        {
            menuPanel.SetActive(true); // Activa el menú
                   // Opcionalmente, pausa el juego si lo deseas
                                                                                                // Time.timeScale = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("jugador")) // Modificado para comprobar si el jugador sale del contacto con la puerta
        {
            menuPanel.SetActive(false); // Desactiva el menú si el jugador se aleja
                                        // Time.timeScale = 1; // Reanuda el juego si lo pausaste anteriormente
        }
    }

    void UpdateHiscoreText()
    {
        int hiscore = PlayerPrefs.GetInt("hiscore", 0);
        hiscoreText.text = hiscore.ToString();
    }

}
