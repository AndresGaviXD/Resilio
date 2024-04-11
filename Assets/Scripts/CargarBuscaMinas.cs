using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarBuscaMinas : MonoBehaviour
{
    public void LoadBuscaminas()
    {
        SceneManager.LoadScene("SegundoTablero");
    }
}
