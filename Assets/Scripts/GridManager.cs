using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int largo = 10;
    public int alto = 9;
    public GameObject[,] cuadrilla;
    public Sprite canonSprite;

    private int tamaño = 3; // Por ejemplo, barco de tamaño 3

    void Start()
    {
        IniciarCuadrilla();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ColocarCanonEnPosicion(mousePosition);
        }
    }

    void IniciarCuadrilla()
    {
        cuadrilla = new GameObject[largo, alto];
    }

    void ColocarCanonEnPosicion(Vector2 posicion)
    {
        int x = Mathf.FloorToInt(posicion.x);
        int y = Mathf.FloorToInt(posicion.y);

        if (x >= 0 && x < largo && y >= 0 && y < alto)
        {
            if (VerificarEspacioDisponible(x, y))
            {
                ColocarCanon(new Vector3(x, y, 0f));
            }
            else
            {
                Debug.Log("No hay suficiente espacio para colocar el barco en esta posición.");
            }
        }
    }

    bool VerificarEspacioDisponible(int x, int y)
    {
        for (int i = 0; i < tamaño; i++)
        {
            if (x + i >= largo || cuadrilla[x + i, y] != null)
            {
                return false;
            }
        }
        return true;
    }

    void ColocarCanon(Vector3 posicion)
    {
        for (int i = 0; i < tamaño; i++)
        {
            GameObject nuevoCanon = new GameObject("CanonParte");
            nuevoCanon.transform.position = new Vector3(posicion.x + i, posicion.y, 0f);

            SpriteRenderer spriteRenderer = nuevoCanon.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = canonSprite;
            // Ajusta el tamaño del sprite según sea necesario.

            cuadrilla[(int)posicion.x + i, (int)posicion.y] = nuevoCanon;
        }
    }
}
