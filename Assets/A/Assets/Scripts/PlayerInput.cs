using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputField xInputField;
    [SerializeField] private InputField yInputField;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button coordenadaIncorrecta;
    private GameLogic gameLogic;
    private bool[,] attackedCells = new bool[10, 10]; // Matriz para rastrear las casillas atacadas

    private void Start()
    {
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        gameLogic = FindObjectOfType<GameLogic>();
        coordenadaIncorrecta.gameObject.SetActive(false);
        IniciarNoatacadas();
    }


    private void IniciarNoatacadas() 
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                attackedCells[i, j] = false;
            }
        }
    }
    public void OnConfirmButtonClick()
    {
        if (string.IsNullOrEmpty(xInputField.text) || string.IsNullOrEmpty(yInputField.text))
        {
            StartCoroutine(Waiting());
            return;
        }

        int x, y;
        if (!int.TryParse(xInputField.text, out x) || !int.TryParse(yInputField.text, out y))
        {
            StartCoroutine(Waiting());
            return;
        }

        if (!IsValidCoordinate(x, y))
        {
            StartCoroutine(Waiting());
            return;
        }

        if (attackedCells[x, y] != true)
        {
            gameLogic.ChooseTile(x, y);
            attackedCells[x, y] = true;
        }
        else
        {
            StartCoroutine(Waiting());
            return;
        }

    }


    private IEnumerator Waiting()
    {
        coordenadaIncorrecta.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        coordenadaIncorrecta.gameObject.SetActive(false);
    }

    // Método para verificar si la coordenada ingresada es válida
    private bool IsValidCoordinate(int x, int y)
    {
        return x >= 0 && x < 10 && y >= 0 && y < 10;
    }
}
