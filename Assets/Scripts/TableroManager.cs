using UnityEngine;

public class TableroManager : MonoBehaviour
{
    public GameObject cuadradoPrefabricado;
    public int tamañoX;
    public int tamañoY;
    public float espacioEntreCasillas = 1f;
    public Cuadrado[,] cuadrosTablero;
    private static TableroManager _instance;
    public static TableroManager Instance => _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject); // Garantiza que solo haya una instancia
        }
    }

    void Start()
    {
        GenerarTablero();
    }

    void GenerarTablero()
    {
        cuadrosTablero = new Cuadrado[tamañoX, tamañoY];

        for (int x = 0; x < tamañoX; x++)
        {
            for (int y = 0; y < tamañoY; y++)
            {
                Vector2 posición = new Vector2(x * espacioEntreCasillas, y * espacioEntreCasillas); // Ajusta la posición según tus necesidades
                GameObject nuevaCasilla = Instantiate(cuadradoPrefabricado, posición, Quaternion.identity);
                nuevaCasilla.name = "Casilla_" + x + "_" + y;
                nuevaCasilla.transform.parent = transform; // Para organizar las casillas en el TableroManager en la jerarquía de Unity

                // Asignar componente CellScript a cada casilla y almacenarlo en la matriz de cuadros del tablero
                Cuadrado cellScript = nuevaCasilla.GetComponent<Cuadrado>();
                cuadrosTablero[x, y] = cellScript;
            }
        }
    }

    public bool CasillaDisponible(int x, int y)
    {
        if (x >= 0 && x < tamañoX && y >= 0 && y < tamañoY)
        {
            return !cuadrosTablero[x, y].HasShip();
        }
        else
        {
            return false;
        }
    }
}