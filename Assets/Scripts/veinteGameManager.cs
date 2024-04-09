using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class veinteGameManager : GameManager
{
    public CasillaTablero tablero;

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        tablero.ClearTablero();
        tablero.CreateCasilla();
        tablero.CreateCasilla();
        tablero.enabled = true;
    }

    public void GameOver()
    {
        tablero.enabled = false;
    }

}
