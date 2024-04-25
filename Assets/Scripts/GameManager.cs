using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    /// <summary>
    /// BUSCAMINAS
    /// </summary>

    /*
    
        [SerializeField] private Transform tilePrefab;
        [SerializeField] private Transform gameHolder;

        private List<Tile> tiles = new List<Tile>();

        private int width;
        private int height;
        private int numMines;


        private readonly float tileSize = 0.5f;

        public GameObject restartPopup;

        void Start()
        {
            CreateGameBoard(16, 16, 40);
            ResetGameState();
        }

        public void CreateGameBoard(int width, int height, int numMines)
        {
            this.width = width;
            this.height = height;
            this.numMines = numMines;


            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    Transform tileTransform = Instantiate(tilePrefab);
                    tileTransform.parent = gameHolder;
                    float xIndex = col - ((width - 1) / 2.0f);
                    float yIndex = row - ((height - 1) / 2.0f);
                    tileTransform.localPosition = new Vector2(xIndex * tileSize, yIndex * tileSize);

                    Tile tile = tileTransform.GetComponent<Tile>();
                    tiles.Add(tile);
                    tile.gameManager = this;
                }
            }
        }


        private void ResetGameState()
        {
            int[] minePositions = Enumerable.Range(0, tiles.Count).OrderBy(x => Random.Range(0.0f, 1.0f)).ToArray();

            for (int i = 0; i < numMines; i++)
            {
                int pos = minePositions[i];
                tiles[pos].isMine = true;
            }

            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].mineCount = HowManyMines(i);

            }
        }

        private int HowManyMines(int location)
        {
            int count = 0;
            foreach (int pos in GetNeighbours(location))
            {
                if (tiles[pos].isMine)
                {
                    count++;
                }
            }
            return count;
        }

        private List<int> GetNeighbours(int pos)
        {
            List<int> neighbours = new List<int>();
            int row = pos / width;
            int col = pos % width;

            if (row < (height - 1))
            {

                neighbours.Add(pos + width);
                if (col > 0)
                {
                    neighbours.Add(pos + width - 1);
                }
                if (col < (width - 1))
                {
                    neighbours.Add(pos + width + 1);
                }
            }
            if (col > 0)
            {
                neighbours.Add(pos - 1);
            }
            if (col < (width - 1))
            {
                neighbours.Add(pos + 1);
            }
            if (row > 0)
            {
                neighbours.Add(pos - width);
                if (col > 0)
                {
                    neighbours.Add(pos - width - 1);
                }
                if (col < (width - 1))
                {
                    neighbours.Add(pos - width + 1);
                }
            }


            return neighbours;
        }

        public void ClickNeighbours(Tile tile)
        {
            int location = tiles.IndexOf(tile);
            foreach (int pos in GetNeighbours(location))
            {
                tiles[pos].ClickedTile();
            }
        }

        public void GameOver()
        {
            foreach (Tile tile in tiles)
            {
                tile.ShowGameOverState();
            }
            restartPopup.SetActive(true);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }

        public void RestartGame()
        {
            ClearBoard();
            // Reinicia el juego recreando el tablero y reseteando el estado del juego
            CreateGameBoard(width, height, numMines);
            ResetGameState();
            restartPopup.SetActive(false);
        }

        private void ClearBoard()
        {
            // Destruir todas las casillas existentes
            foreach (Transform child in gameHolder)
            {
                Destroy(child.gameObject);
            }

            // Limpiar la lista de casillas
            tiles.Clear();
        }

        public void CheckGameOver()
        {
            int count = 0;
            foreach (Tile tile in tiles)
            {
                if (tile.active)
                {
                    count++;
                }
            }
            if (count == numMines)
            {
                Debug.Log("Has desactivado todas las minas");
                foreach (Tile tile in tiles)
                {
                    tile.active = false;
                    tile.SetFlaggedIfMine();
                }
            }
        }

        public void ExpandIfFlagged(Tile tile)
        {
            int location = tiles.IndexOf(tile);
            int flag_count = 0;
            foreach (int pos in GetNeighbours(location))
            {
                if (tiles[pos].flagged)
                {
                    flag_count++;
                }
            }

            if (flag_count == tile.mineCount)
            {
                ClickNeighbours(tile);
            }
        }

    */


    /// <summary>
    /// 2048
    /// </summary>

    /*
    public CasillaTablero tablero;

    private void Iniciar()
    {
        NewGame();
    }

    public void NewGame()
    {
        tablero.ClearTablero();
        tablero.CreateCasilla();
        tablero.CreateCasilla();
        tablero.enabled = true;
    }

    public void FinDeJuego()
    {
        tablero.enabled = false;
    }

    */

    /// <summary>
    /// Astucia
    /// </summary>

    /*
    
    private int numberOfShips = 10;
    private Vector3 chosenPosition;
    private bool isShipChosen = false;
    private bool isProperlyPlaced;

    [SerializeField] ShipScript.ShipType type;
    [SerializeField] ShipScript selectedShip;
    [SerializeField] CellScript selectedCell;
    [SerializeField] Button nextButton;
    [SerializeField] Button textChangePosition;
    [SerializeField] Collider2D dockCollider;

    void Inicio()
    {
        selectedShip = null;
        selectedCell = null;

        dockCollider = GameObject.Find("Dock").GetComponent<Collider2D>();
        nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        textChangePosition = GameObject.Find("ChangePosition").GetComponent<Button>();

        nextButton.gameObject.SetActive(false);
        textChangePosition.gameObject.SetActive(false);
    }

    private void Actualizar()
    {
        if(nextButton == null || textChangePosition == null)
        {
            nextButton = GameObject.Find("NextButton").GetComponent<Button>();
            textChangePosition = GameObject.Find("ChangePosition").GetComponent<Button>();
        }

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!isShipChosen)
            {
                if (hit.collider != null)
                {
                    ShipScript ship = hit.collider.GetComponent<ShipScript>();

                    if (ship != null)
                    {
                        if (dockCollider.OverlapPoint(hit.point))
                        {
                            InitializeNewPosition(ship, hit);
                        }

                        else
                        {
                            UnityEngine.Debug.Log("You can't change position of your ship already!");
                        }
                    }
                }
            }

            if (isShipChosen)
            {
                if (hit.collider != null && hit.collider.CompareTag("Cell"))
                {

                    //tile logic
                    CellScript tile = hit.collider.GetComponent<CellScript>();
                    selectedCell = tile;

                    if (selectedCell.ReturnAvailability() == false)
                    {
                        chosenPosition = hit.transform.position;
                        selectedCell.SetAvailability();
                        selectedShip.MoveShip(chosenPosition, type);

                        //check if ship doesnt collide with other ships or is outside the border
                        isProperlyPlaced = selectedShip.ReturnPlacement();

                        if(isProperlyPlaced)
                        {
                            numberOfShips = numberOfShips - 1;
                            isShipChosen = false;

                        }
                        else if(!isProperlyPlaced)
                        {
                            selectedCell.ResetAvailability();
                            chosenPosition = Vector3.zero;
                        }

                    }

                    else if(selectedCell.ReturnAvailability() == true)
                    {
                        StartCoroutine(Wait());
                    }

                }

            }
        }

        //rotate ship
        if (Input.GetKeyDown(KeyCode.X) && isShipChosen)
        {
            selectedShip.RotateShip();
        }

        //if every ship is on board start the battle
        if (numberOfShips == 0)
        {
            DisplayButtonNext();
        }
    }

    public void DisplayButtonNext()
    {
        nextButton.gameObject.SetActive(true);
    }

    private void InitializeNewPosition(ShipScript ship, RaycastHit2D hit)
    {
        isShipChosen = true;
        selectedShip = ship;
        chosenPosition = hit.point;

        //get a type of ship
        type = ship.CheckType(hit.collider.gameObject);

        //initialize shining during click 
        ship.ChangeColor(hit.collider.gameObject, type);
    }

    public IEnumerator Wait()
{
    textChangePosition.gameObject.SetActive(true);
    
    // Movemos el barco seleccionado de regreso a su posición original en el dock
    selectedShip.transform.position = selectedShip.originalPosition;

    // Reiniciamos la posición seleccionada para evitar problemas con futuros clics
    chosenPosition = Vector3.zero;

    yield return new WaitForSeconds(1f);
    textChangePosition.gameObject.SetActive(false);
}

    */
}