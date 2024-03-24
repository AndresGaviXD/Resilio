using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameLogic : MonoBehaviour
{
    CellScript selectedCell;
    private int Turn = 1;
    private int enemyShipsNumber = 20;
    private int playerShipsNumber = 20;
    private bool isTileChoosen = false;
    private bool enemyChoosenTile = false;
    private bool enemyTilesFound = false;
    AI ai;
    [SerializeField] Text yourTurn;
    [SerializeField] Text enemyTurn;
    [SerializeField] Button youWin;
    [SerializeField] Button youLost;
    [SerializeField] Button backToMenu;
    [SerializeField] GameObject fire;
    [SerializeField] GameObject missed;
    [SerializeField] List<GameObject> playerTiles = new List<GameObject>();
    [SerializeField] List<GameObject> playerUI = new List<GameObject>();
    [SerializeField] List<GameObject> enemyUI = new List<GameObject>();
    [SerializeField] List<GameObject> enemyTiles = new List<GameObject>();
    [SerializeField] List<GameObject> playerShips = new List<GameObject>();
    Dictionary<string, List<GameObject>> shipDictionary = new Dictionary<string, List<GameObject>>();

    void Start()
    {
        selectedCell = null;
        youWin.gameObject.SetActive(false);
        youLost.gameObject.SetActive(false);
        backToMenu.gameObject.SetActive(false);
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

        if(enemyShipsNumber == 0)
        {
            Turn = 0;
            youWin.gameObject.SetActive(true);
            backToMenu.gameObject.SetActive(true);
        }
        else if(playerShipsNumber == 0)
        {
            Turn = 0;
            youLost.gameObject.SetActive(true);
            backToMenu.gameObject.SetActive(true);
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
        shipDictionary.Add("3cells_ship", new List<GameObject>());
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

    void ChooseTile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("EnemyCell"))
            {
                CellScript cell = hit.collider.GetComponent<CellScript>();
                selectedCell = cell;

                CheckIfHit(selectedCell, playerUI, isTileChoosen);

                StartCoroutine(Wait(2));
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

    void CheckIfHit(CellScript selectedCell, List<GameObject> list, bool chosenTile)
    {
        chosenTile = true;

        if (selectedCell.ReturnAvailability() == true)
        {
            GameObject newFire = Instantiate(fire, selectedCell.transform.position, Quaternion.identity);
            list.Add(newFire);

            if(Turn == 1)
            {
                enemyShipsNumber--;
                UnityEngine.Debug.Log(enemyShipsNumber);
            }
            if(Turn == 2)
            {
                playerShipsNumber--;
                UnityEngine.Debug.Log(playerShipsNumber);
            }
        }

        else
        {
            GameObject newMissed = Instantiate(missed, selectedCell.transform.position, Quaternion.identity);
            list.Add(newMissed);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
