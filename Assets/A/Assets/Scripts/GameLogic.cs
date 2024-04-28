using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    CellScript selectedCell;
    private int Turn = 1;
    private int enemyShipsNumber = 16;
    private int playerShipsNumber = 16;
    private bool isTileChoosen = false;
    private bool enemyChoosenTile = false;
    private bool enemyTilesFound = false;
    AI ai;
    [SerializeField] private Text yourTurn;
    [SerializeField] private Text enemyTurn;
    [SerializeField] private Button youWin;
    [SerializeField] private Button youLost;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject missed;
    [SerializeField] private List<GameObject> playerTiles = new List<GameObject>();
    [SerializeField] private List<GameObject> playerUI = new List<GameObject>();
    [SerializeField] private List<GameObject> enemyUI = new List<GameObject>();
    [SerializeField] private List<GameObject> enemyTiles = new List<GameObject>();
    [SerializeField] private List<GameObject> playerShips = new List<GameObject>();
    Dictionary<string, List<GameObject>> shipDictionary = new Dictionary<string, List<GameObject>>();
    [SerializeField] private Text scoreText;
    private int consecutiveHits = 0;
    public int score;


    void UpdateScore(int score)
    {
        string scoreLetter = GetScoreLetter(score);
        scoreText.text = "Puntaje: " + score + ", Clasificación: " + scoreLetter;
    }

    void Start()
    {
        selectedCell = null;
        youWin.gameObject.SetActive(false);
        youLost.gameObject.SetActive(false);
        ai = GameObject.Find("EnemyManager").GetComponent<AI>();
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
        playerTiles.AddRange(cells);
        ShipFinding();
    }

    void Update()
    {
        if (!enemyTilesFound)
        {
            GameObject[] enemyCells = GameObject.FindGameObjectsWithTag("EnemyCell");
            enemyTiles.AddRange(enemyCells);
            enemyTilesFound = true;
        }

        if (Turn == 1)
        {
            SetUI(true);

            if (!isTileChoosen)
            {
                // ChooseTile() se llama desde la entrada del jugador
            }
        }

        if (Turn == 2)
        {
            SetUI(false);

            if (!enemyChoosenTile)
            {
                enemyChoosenTile = true;
                (int x, int y) position = ai.ChooseTile();
                CellScript enemyChosenTile = GetTileAtPosition(position.x, position.y, 10);

                CheckIfHit(enemyChosenTile, enemyUI, enemyChoosenTile);
            }

            StartCoroutine(Wait(1));
        }

        if (enemyShipsNumber == 0)
        {
            // Calcular puntaje y mostrar resultado
            CalculateScore();
            Turn = 0;
            youWin.gameObject.SetActive(true);
            UpdateScore(score);
        }
        else if (playerShipsNumber == 0)
        {
            // Calcular puntaje y mostrar resultado
            CalculateScore();
            Turn = 0;
            youLost.gameObject.SetActive(true);
            UpdateScore(score);

        }

    }

    void SetActive(List<GameObject> list, bool active)
    {
        foreach (GameObject gameObject in list)
        {
            gameObject.SetActive(active);
        }
    }

    void SetUI(bool active)
    {
        yourTurn.enabled = active;
        SetActive(enemyTiles, active);
        SetActive(playerUI, active);
        enemyTurn.enabled = !active;
        SetActive(enemyUI, !active);
        SetActive(playerTiles, !active);
        SetActive(playerShips, !active);
    }

    void ShipFinding()
    {
        shipDictionary.Add("1cell_ship", new List<GameObject>());
        shipDictionary.Add("2cells_ship", new List<GameObject>());
        shipDictionary.Add("4cells_ship", new List<GameObject>());

        GameObject[] allShips = FindObjectsOfType<GameObject>();

        foreach (GameObject ship in allShips)
        {
            string shipTag = ship.tag;

            if (shipDictionary.ContainsKey(shipTag))
            {
                shipDictionary[shipTag].Add(ship);
            }
        }

        foreach (List<GameObject> shipsList in shipDictionary.Values)
        {
            playerShips.AddRange(shipsList);
        }
    }

    // Nuevo método para que el jugador elija una casilla
    public void ChooseTile(int x, int y)
    {
        CellScript selectedTile = GetTileAtPosition(x, y, 10);
        if (selectedTile != null)
        {
            CheckIfHit(selectedTile, playerUI, isTileChoosen);
            StartCoroutine(Wait(2));
        }
        else
        {
            isTileChoosen = false;
        }
    }

    IEnumerator Wait(int number)
    {
        yield return new WaitForSeconds(1.2f);
        Turn = number;
        isTileChoosen = false;
        enemyChoosenTile = false;
    }

    public CellScript GetTileAtPosition(int x, int y, int width)
    {
        int index = x + y * width;
        if (index >= 0 && index < playerTiles.Count)
        {
            return playerTiles[index].GetComponent<CellScript>();
        }
        else
        {
            return null;
        }
    }

    void CheckIfHit(CellScript selectedTile, List<GameObject> list, bool chosenTile)
    {
        chosenTile = true;

        if (selectedTile.ReturnAvailability() == true)
        {
            GameObject newFire = Instantiate(fire, selectedTile.transform.position, Quaternion.identity);
            list.Add(newFire);

            if (Turn == 1)
            {
                enemyShipsNumber--;
                UnityEngine.Debug.Log(enemyShipsNumber);
            }
            if (Turn == 2)
            {
                playerShipsNumber--;
                UnityEngine.Debug.Log(playerShipsNumber);
            }
        }

        else
        {
            GameObject newMissed = Instantiate(missed, selectedTile.transform.position, Quaternion.identity);
            list.Add(newMissed);
            consecutiveHits = 0;
        }
    }

    void CalculateScore()
    {

        // Calcular puntaje
        if (enemyShipsNumber == 0) // Victoria
        {
            score += 500; // Puntuación base por victoria
            score += 160; //barcos destruidos
            score += consecutiveHits * 50; // Puntuación por aciertos consecutivos
            score -= playerShipsNumber * 10;
        }
        else // Derrota
        {

            score += consecutiveHits * 50; // Puntuación por aciertos consecutivos
            score += enemyShipsNumber * 10;
            score -= playerShipsNumber * 10;

        }


    }


    string GetScoreLetter(int score)
    {
        if (score < 800)
        {
            return "Bajo";
        }
        else if (score >= 800 && score < 1000)
        {
            return "Medio";
        }
        else
        {
            return "Alto";
        }
    }

}