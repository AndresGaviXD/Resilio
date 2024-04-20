using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class veinteGameManager : GameManager
{
    public CasillaTablero tablero;
    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public TextMeshProUGUI gradeText; // Nuevo: Agregamos un campo para mostrar la calificaci�n

    private int score;
    private int maxCellValue;
    private string gradeKey = "PlayerGrade"; // Nuevo: Clave para guardar la calificaci�n en PlayerPrefs

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {

        SetScore(0);
        maxCellValue = 0; // Reiniciar el valor m�ximo de la casilla al iniciar un nuevo juego
        hiscoreText.text = LoadHiscore().ToString();
        LoadGrade(); // Nuevo: Cargamos la calificaci�n al iniciar un nuevo juego
        gameOver.alpha = 0f;
        gameOver.interactable = false;

        tablero.ClearTablero();
        tablero.CreateCasilla();
        tablero.CreateCasilla();
        tablero.enabled = true;
    }

    public void GameOver()
    {
        tablero.enabled = false;
        gameOver.interactable = true;
        StartCoroutine(Fade(gameOver, 1f, 1f));

        string grade = CalculateGrade(maxCellValue); // Nuevo: Calculamos la calificaci�n
        SaveGrade(grade); // Nuevo: Guardamos la calificaci�n obtenida
        gradeText.text = "Grade: " + grade; // Nuevo: Mostramos la calificaci�n en la pantalla de Game Over
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }


    private void SetScore(int score)
    {
        this.score = score;

        scoreText.text = score.ToString();
        SaveHiscore();
    }

    private void SaveHiscore()
    {
        int hiscore = LoadHiscore();

        if (score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    public int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }

    public string CalculateGrade(int maxCellValue)
    {
        // L�gica para calcular la calificaci�n basada en la puntuaci�n
        if (maxCellValue <= 16)
        {
            return "D";
        }
        else if (maxCellValue <= 64)
        {
            return "C";
        }
        else if (maxCellValue <= 254)
        {
            return "B";
        }
        else if (maxCellValue <= 512)
        {
            return "A";
        }
        else
        {
            return "S";
        }
    }

    public void SaveGrade(string grade)
    {
        PlayerPrefs.SetString(gradeKey, grade); // Guardar la calificaci�n en PlayerPrefs
    }

    public void LoadGrade()
    {
        string savedGrade = PlayerPrefs.GetString(gradeKey, ""); // Cargar la calificaci�n guardada
        gradeText.text = "Grade: " + savedGrade; // Mostrar la calificaci�n en la UI
    }
    public void IncreaseMaxCellValue(int value)
    {
        if (value > maxCellValue)
        {
            maxCellValue = value; // Actualizar el valor m�ximo de la casilla si se alcanza un nuevo m�ximo
        }
    }

}
