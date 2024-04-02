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
}
