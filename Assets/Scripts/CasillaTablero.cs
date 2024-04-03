using System.Collections    ;
using System.Collections.Generic;
using UnityEngine;

public class CasillaTablero : MonoBehaviour
{
    public Casilla casillaPrefab;

    private CasillaGrid grid;
    private List<Casilla> casillas;


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
    }
}
