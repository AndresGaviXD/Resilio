using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    CellScript selectedCell;
    private int Turn = 1;
    private int enemyShipsNumber = 12;
    private int playerShipsNumber = 12;
    private bool isTileChoosen = false;
    private bool enemyChoosenTile = false;
    private bool enemyTilesFound = false;
    AI ai;
    [SerializeField] Text yourTurn;
    [SerializeField] Text enemyTurn;
    [SerializeField] Button youWin;
    [SerializeField] Button youLost;
    [SerializeField] GameObject fire;
    [SerializeField] GameObject missed;
    [SerializeField] List<GameObject> playerTiles = new List<GameObject>();
    [SerializeField] List<GameObject> playerUI = new List<GameObject>();
    [SerializeField] List<GameObject> enemyUI = new List<GameObject>();
    [SerializeField] List<GameObject> enemyTiles = new List<GameObject>();
    [SerializeField] List<GameObject> playerShips = new List<GameObject>();
    Dictionary<string, List<GameObject>> shipDictionary = new Dictionary<string, List<GameObject>>();
    int score = 0;
    private int consecutiveHits = 0;
    private int acumuladoshits = 0;
    int barcosenemigosderribados = 0;
    int oldscore;
    public char currentGrade = 'D';
    public TMP_Text gradeText;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        selectedCell = null;
        youWin.gameObject.SetActive(false);
        youLost.gameObject.SetActive(false);
        ai = GameObject.Find("EnemyManager").GetComponent<AI>();
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
        playerTiles.AddRange(cells);
        ShipFinding();

        // Cargar puntaje máximo y grado máximo guardados
        oldscore = PlayerPrefs.GetInt("PuntuacionMaxima", 0);
        currentGrade = PlayerPrefs.GetString("Letra", "D")[0];
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
                ChooseTile();
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
            Turn = 0;
            youWin.gameObject.SetActive(true);
            SetActive(enemyUI, false);
            SetActive(playerUI, false);
            SetActive(enemyTiles, false);
            SetActive(playerTiles, false);
            SetActive(playerShips, false);
            Acabar();
        }
        else if (playerShipsNumber == 0)
        {
            Turn = 0;
            youLost.gameObject.SetActive(true);
            SetActive(enemyUI, false);
            SetActive(playerUI, false);
            SetActive (enemyTiles, false);
            SetActive(playerTiles, false);
            SetActive(playerShips, false);
            Acabar();
        }

    }

    void SetActive(List<GameObject> list, bool active)
    {
        foreach (GameObject gameObject in list)
        {
            gameObject.SetActive(active);
        }
    }

    private void Acabar()
    {
        CalcularScore();
        char scoreGrade = CalcularMargen(score); // Calcula la letra correspondiente a la puntuación actual
        char highScoreGrade = CalcularMargen(oldscore); // Calcula la letra correspondiente a la máxima puntuación guardada

        // Actualiza la máxima puntuación si la puntuación actual supera a la máxima puntuación guardada
        if (score > oldscore)
        {
            oldscore = score;
            PlayerPrefs.SetInt("PuntuacionMaxima", oldscore);
            PlayerPrefs.SetString("Letra", scoreGrade.ToString());
        }

        // Construye el texto para mostrar la puntuación y letra alcanzada antes del game over y la máxima puntuación guardada
        string gameOverText = "Tu calificación: " + scoreGrade + "\nPuntuación: " + score;
        string highScoreText = "\n\nMáxima puntuación: " + oldscore + "\nCalificación máxima: " + highScoreGrade;

        // Actualiza el texto mostrando la información de forma separada
        scoreText.text = gameOverText + highScoreText;
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

    List<CellScript> attackedTiles = new List<CellScript>();

    void ChooseTile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("EnemyCell"))
            {
                CellScript tile = hit.collider.GetComponent<CellScript>();

                // Verificar si la casilla ya ha sido atacada
                if (attackedTiles.Contains(tile))
                {
                    // Casilla ya atacada, no hacer nada
                    return;
                }
                else
                {
                    // Agregar la casilla al arreglo de casillas atacadas
                    attackedTiles.Add(tile);

                    // Realizar el ataque
                    CheckIfHit(tile, playerUI, isTileChoosen);
                    StartCoroutine(Wait(2));
                }
            }
            else
            {
                isTileChoosen = false;
            }
        }
    }

    IEnumerator Wait(int number)
    {
        yield return new WaitForSeconds(1.5f);
        Turn = number;
        isTileChoosen = false;
        enemyChoosenTile = false;
    }

    CellScript GetTileAtPosition(int x, int y, int width)
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

        if (selectedTile.ReturnAvailability())
        {
            GameObject newFire = Instantiate(fire, selectedTile.transform.position, Quaternion.identity);
            list.Add(newFire);

            if (Turn == 1)
            {
                enemyShipsNumber--;
                barcosenemigosderribados++;
                consecutiveHits++;
            }
            if (Turn == 2)
            {
                playerShipsNumber--;
            }
        }
        else
        {
            GameObject newMissed = Instantiate(missed, selectedTile.transform.position, Quaternion.identity);
            list.Add(newMissed);
            if (consecutiveHits > 1) { 
                acumuladoshits =+ consecutiveHits;
            
            }
            consecutiveHits = 0;
        }
    }

    private int CalcularScore()
    {
        score = 0;
        if (enemyShipsNumber == 0)
        {
            // Añadir puntaje por victoria y por impactos consecutivos
            score += (8 + acumuladoshits + barcosenemigosderribados);
        }
        else
        {
            // Añadir puntaje por derrota y por impactos consecutivos
            score += acumuladoshits + barcosenemigosderribados;
        }
        return score;

    }


    private char CalcularMargen(int score)
    {
        char grade;
        if (score >= 0 && score < 6)
        {
            grade = 'E';
        }
        else if (score >= 6 && score < 12)
        {
            grade = 'D';
        }
        else if (score >= 12 && score < 18)
        {
            grade = 'C';
        }
        else if (score >= 18 && score < 24)
        {
            grade = 'B';
        }
        else if (score >= 24 && score < 28)
        {
            grade = 'A';
        }
        else
        {
            grade = 'S';
        }
        return grade;
    }
}
