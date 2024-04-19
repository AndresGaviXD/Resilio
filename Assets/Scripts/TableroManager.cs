using UnityEngine;

public class TableroManager : MonoBehaviour
{
    public GameObject cuadradoPrefabricado;
    public int tama�oX;
    public int tama�oY;
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
        cuadrosTablero = new Cuadrado[tama�oX, tama�oY];

        for (int x = 0; x < tama�oX; x++)
        {
            for (int y = 0; y < tama�oY; y++)
            {
                Vector2 posici�n = new Vector2(x * espacioEntreCasillas, y * espacioEntreCasillas); // Ajusta la posici�n seg�n tus necesidades
                GameObject nuevaCasilla = Instantiate(cuadradoPrefabricado, posici�n, Quaternion.identity);
                nuevaCasilla.name = "Casilla_" + x + "_" + y;
                nuevaCasilla.transform.parent = transform; // Para organizar las casillas en el TableroManager en la jerarqu�a de Unity

                // Asignar componente CellScript a cada casilla y almacenarlo en la matriz de cuadros del tablero
                Cuadrado cellScript = nuevaCasilla.GetComponent<Cuadrado>();
                cuadrosTablero[x, y] = cellScript;
            }
        }
    }

    public bool CasillaDisponible(int x, int y)
    {
        if (x >= 0 && x < tama�oX && y >= 0 && y < tama�oY)
        {
            return !cuadrosTablero[x, y].HasShip();
        }
        else
        {
            return false;
        }
    }
}