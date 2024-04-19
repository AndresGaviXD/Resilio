using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void QuitFromGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
