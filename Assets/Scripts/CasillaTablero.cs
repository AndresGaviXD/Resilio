using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasillaTablero : MonoBehaviour
{
    public Casilla casillaPrefab;
    public CasillaEstado[] casillaEstados;

    private CasillaGrid grid;
    private List<Casilla> casillas;
    private bool waiting;

    private void Awake()
    {
        grid = GetComponentInChildren<CasillaGrid>();
        casillas = new List<Casilla>(16);
    }

    private void Start()
    {
        CreateCasilla();
        CreateCasilla();
    }

    private void CreateCasilla()
    {
        Casilla casilla = Instantiate(casillaPrefab, grid.transform);

        casilla.SetEstado(casillaEstados[0], "-");

        casilla.Spawn(grid.GetRandomEmptyCell());

        casillas.Add(casilla);
    } 

    private void Update()
    {
        if (!waiting)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveCasillas(Vector2Int.up, 0, 1, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveCasillas(Vector2Int.left, 1, 1, 0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveCasillas(Vector2Int.down, 0, 1, grid.height - 2, -1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveCasillas(Vector2Int.right, grid.width - 2, -1, 0, 1);
            }
        }
        
    }

    private void MoveCasillas(Vector2Int direccion, int startX, int incrementX, int startY, int incrementY)
    {
        bool changed = false;
        for (int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {
            for (int  y = startY; y >= 0 && y < grid.height; y += incrementY)
            {
                CasillaCell cell = grid.GetCell(x, y);

                if (cell.ocupado)
                {
                    changed |= MoveCasilla(cell.casilla, direccion);
                }
            }
        }

        if (changed)
        {
            StartCoroutine(WaitForChanges());
        }
    }

    private bool MoveCasilla(Casilla casilla, Vector2Int direccion)
    {
        CasillaCell newCell = null;
        CasillaCell adjacent = grid.GetAdjacentCell(casilla.cell, direccion);


        while (adjacent != null)
        {
            if (adjacent.ocupado)
            {
                // TODO: merging
                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direccion);
        }

        if (newCell != null)
        {
            casilla.MoveTo(newCell);
            return true;
        }

        return false;
    }

    private IEnumerator WaitForChanges()
    {
        waiting = true;
        yield return new WaitForSeconds(0.1f);
        waiting = false;

        //TODO: Crear nueva casilla
        //TODO: GAME OVER
    }
}
