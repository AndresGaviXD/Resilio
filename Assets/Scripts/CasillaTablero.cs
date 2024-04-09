using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasillaTablero : MonoBehaviour
{
    public veinteGameManager gameManager;
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

    public void ClearTablero()
    {

        foreach (var cell in grid.cells)
        {
            cell.casilla = null;
        }

        foreach (var casilla in casillas) 
        { 
            Destroy(casilla.gameObject);

        }

        casillas.Clear();

    }

    public void CreateCasilla()
    {
        Casilla casilla = Instantiate(casillaPrefab, grid.transform);

        casilla.SetEstado(casillaEstados[0], 2);

        casilla.Spawn(grid.GetRandomEmptyCell());

        casillas.Add(casilla);
    } 

    private void Update()
    {
        if (!waiting)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveCasillas(Vector2Int.up, 0, 1, 1, -1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveCasillas(Vector2Int.left, 1, 1, 0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveCasillas(Vector2Int.down, 0, 1, grid.height - 1, -1);
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
                if (CanMerge(casilla, adjacent.casilla))
                {
                    Merge(casilla, adjacent.casilla);
                    return true;
                }
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

    private bool CanMerge(Casilla a, Casilla b)
    {
        return a.number == b.number && !b.locked;
    }

    private void Merge(Casilla a, Casilla b)
    {
        casillas.Remove(a);
        a.Merge(b.cell);

        int index = Mathf.Clamp(IndexOf(b.state) + 1, 0, casillaEstados.Length - 1);
        int number = b.number * 2;

        b.SetEstado(casillaEstados[index], number);
    }

    private int IndexOf(CasillaEstado state)
    {
        for (int i = 0; i < casillaEstados.Length; i++)
        {
            if (state == casillaEstados[i])
            {
                return i;
            }
        }

        return -1;
    }

    private IEnumerator WaitForChanges()
    {
        waiting = true;
        yield return new WaitForSeconds(0.1f);
        waiting = false;

        foreach (var casilla in casillas)
        {
            casilla.locked = false;
        }


        if (casillas.Count != grid.size)
        {
            CreateCasilla();
        }


        if (CheckJuegoFinalizado())
        {
            gameManager.GameOver();
        }
    }

    private bool CheckJuegoFinalizado()
    {
        if (casillas.Count != grid.size)
        {
            return false;
        }

        foreach (var casilla in casillas)
        {
            CasillaCell up = grid.GetAdjacentCell(casilla.cell, Vector2Int.up);
            CasillaCell down = grid.GetAdjacentCell(casilla.cell, Vector2Int.down);
            CasillaCell left = grid.GetAdjacentCell(casilla.cell, Vector2Int.left);
            CasillaCell right = grid.GetAdjacentCell(casilla.cell, Vector2Int.right);

            if (up != null && CanMerge(casilla, up.casilla))
            {
                return false;
            }
            if (down != null && CanMerge(casilla, down.casilla))
            {
                return false;
            }
            if (left != null && CanMerge(casilla, left.casilla))
            {
                return false;
            }

            if (right != null && CanMerge(casilla, right.casilla))
            {
                return false;
            }


        }

        return true;
    }
}
