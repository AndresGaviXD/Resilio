using System.Collections.Generic;
using System;
using UnityEngine;

public class AI : MonoBehaviour
{
    private List<Tuple<int, int>> usedIndices = new List<Tuple<int, int>>();
    private List<Tuple<int, int>> chosenIndices = new List<Tuple<int, int>>();
    private List<Tuple<int, int>> temporaryIndices = new List<Tuple<int, int>>();
    private GameObject[,] enemyTiles = new GameObject[10, 10];
    [SerializeField] GameObject cellPrefab;

    // Tamaño de cada celda del tablero
    private float cellSize = 0.9f;
    private float yOffset = -4.5f;
    private float xOffset = -4.5f;

    void Start()
    {
    
        Generate(4, 1); //generate 4 one-tile-sized ships
        Generate(3, 2); //generate 3 two-tiles-sized ships
        Generate(1, 4); //generate 1 four-tiles-sized ship

        RenderBoard();

    }

    void Generate(int shipsToGenerate, int shipSize)
    {
        for (int i = 0; i < shipsToGenerate; i++)
        {
            int x;
            int y;

            do
            {
                x = UnityEngine.Random.Range(0, 10);
                y = UnityEngine.Random.Range(0, 10);

            } while (usedIndices.Contains(new Tuple<int, int>(x, y)));

            usedIndices.Add(new Tuple<int, int>(x, y));

            temporaryIndices.Clear();

            if (shipSize > 1)
            {
                int direction;
                int nextX;
                int nextY;
                bool isValidPosition;

                do
                {
                    direction = UnityEngine.Random.Range(0, 4); //generate random direction (0 - up, 1 - down, 2 - left, 3 - right)
                    nextX = x;
                    nextY = y;
                    isValidPosition = true;

                    for (int j = 0; j < shipSize - 1; j++)
                    {
                        if (direction == 0 && nextY < 9) //up
                        {
                            nextY++;
                        }
                        else if (direction == 1 && nextY > 0) //down
                        {
                            nextY--;
                        }
                        else if (direction == 2 && nextX > 0) //left
                        {
                            nextX--;
                        }
                        else if (direction == 3 && nextX < 9) // right
                        {
                            nextX++;
                        }
                        else
                        {
                            temporaryIndices.Clear();
                            isValidPosition = false;
                            break;
                        }

                        if (usedIndices.Contains(new Tuple<int, int>(nextX, nextY)))
                        {
                            temporaryIndices.Clear();
                            isValidPosition = false;
                            break;
                        }

                        temporaryIndices.Add(new Tuple<int, int>(nextX, nextY));

                    }
                } while (!isValidPosition);
            }

            usedIndices.AddRange(temporaryIndices);
        }
    }

    void RenderBoard()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                // Calculamos las coordenadas
                float xPos = x * cellSize + xOffset;
                float yPos = y * cellSize + yOffset;  // Ajuste del desplazamiento en Y

                // Creamos la celda en la posición calculada
                GameObject cell = Instantiate(cellPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                enemyTiles[x, y] = cell;

                // Verificamos si la celda tiene un barco
                bool hasShip = usedIndices.Contains(new Tuple<int, int>(x, y));
                cell.GetComponent<CellScript>().hasShip = hasShip;
            }
        }
    }




    public (int x, int y) ChooseTile()
    {
        int x;
        int y;

        do
        {
            x = UnityEngine.Random.Range(0, 10);
            y = UnityEngine.Random.Range(0, 10);

        } while (chosenIndices.Contains(new Tuple<int, int>(x, y)));

        chosenIndices.Add(new Tuple<int, int>(x, y));

        return (x, y);
    }
}
