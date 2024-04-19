using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadBattle : MonoBehaviour
{
    public void LoadTheBattle()
    {
        SceneManager.LoadScene("Battle");
    }
}
