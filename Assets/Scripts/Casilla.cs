using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Casilla : MonoBehaviour
{
    public CasillaEstado state { get; private set; }
    public CasillaCell cell { get; private set; }
    public int number { get; private set; }

    private Image fondo;
    private TextMeshProUGUI text;

    private void Awake()
    {
        fondo = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetEstado(CasillaEstado state, int number)
    {
        this.state = state;
        this.number = number;

        fondo.color = state.backgroundColor;
    }
}
