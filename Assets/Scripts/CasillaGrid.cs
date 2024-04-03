using UnityEngine;

public class CasillaGrid : MonoBehaviour
{
    public CasillaRow[] rows { get; private set; }
    public CasillaCell[] cells { get; private set; }

    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size/height;


    private void Awake()
    {
        rows = GetComponentsInChildren<CasillaRow>();
        cells = GetComponentsInChildren<CasillaCell>();
    }

    private void Start()
    {
        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].cells.Length; x++)
            {
                rows[y].cells[x].coordenadas = new Vector2Int(x, y);
            }
        }
    }

    public CasillaCell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return rows[y].cells[x];
        } else
        {
            return null;
        }
        
    }

    public CasillaCell GetCell(Vector2Int coordenadas)
    {
        return GetCell(coordenadas.x, coordenadas.y);
    }
    

    public CasillaCell GetAdjacentCell(CasillaCell cell, Vector2Int direccion)
    {
        Vector2Int coordenadas = cell.coordenadas;
        coordenadas.x += direccion.x;
        coordenadas.y += direccion.y;

        return GetCell(coordenadas);
    }



    public CasillaCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;

        while (cells[index].ocupado)
        {
            index++;


            if (index >= cells.Length)
            {
                index = 0;
            }

            if (index == startingIndex)
            {
                return null;
            }
        }

        return cells[index];
    }
}
