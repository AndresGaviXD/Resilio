using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuscaGameManager : GameManager
{
    [SerializeField] private Transform tilePrefab;
    [SerializeField] private Transform gameHolder;
    public TextMeshProUGUI scoreText;
    public char currentGrade = 'F';
    public TMP_Text gradeText;
    private List<Tile> tiles = new List<Tile>();
    private int width;
    private int height;
    private int numMines;
    private int oldscore;
    private readonly float tileSize = 0.5f;
    public GameObject restartPopup;
    int newScore = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("HighestScore"))
        {
            PlayerPrefs.SetInt("HighestScore", 0);
            PlayerPrefs.SetString("HighestGrade", "F");
        }

        oldscore = PlayerPrefs.GetInt("HighestScore");
        currentGrade = PlayerPrefs.GetString("HighestGrade")[0];

        gradeText.text = "Tu calificación: " + currentGrade;

        CreateGameBoard(16, 16, 40);
        ResetGameState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
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
        int newScore = CalculateScore();
        char newGrade = CalculateGrade(newScore);

        if (newScore > oldscore)
        {
            PlayerPrefs.SetInt("HighestScore", newScore);
            PlayerPrefs.SetString("HighestGrade", newGrade.ToString());

            oldscore = newScore;
            currentGrade = newGrade;
        }

        if (newScore > oldscore)
        {
            scoreText.text = "Tu calificación: " + currentGrade + "\nTu puntaje: " + newGrade + "\nPuntaje más alto: " + oldscore;
        }
        else
        {
            scoreText.text = "Tu calificación: " + newGrade + "\nTu puntaje: " + newScore + "\nPuntaje más alto: " + oldscore;
        }

        foreach (Tile tile in tiles)
        {
            tile.ShowGameOverState();
        }
        restartPopup.SetActive(true);
    }

    public void RestartGame()
    {
        ClearBoard();
        CreateGameBoard(width, height, numMines);
        ResetGameState();
        restartPopup.SetActive(false);
        scoreText.text = "Tu calificación: " + currentGrade + "\nTu puntaje más alto: " + oldscore;
        newScore = 0;
    }

    private void ClearBoard()
    {
        foreach (Transform child in gameHolder)
        {
            Destroy(child.gameObject);
        }
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

    private int CalculateScore()
    {
        int score = 0;
        foreach (Tile tile in tiles)
        {
            if (tile.flagged == true && tile.isMine == true)
            {
                score++;
            }
        }
        return score;
    }

    private char CalculateGrade(int score)
    {
        char grade;
        if (score < 10)
        {
            grade = 'D';
        }
        else if (score < 20)
        {
            grade = 'C';
        }
        else if (score < 30)
        {
            grade = 'B';
        }
        else if (score < 40)
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
