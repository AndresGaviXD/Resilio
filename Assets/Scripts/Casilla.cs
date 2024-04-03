using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Casilla : MonoBehaviour
{
    public CasillaEstado state { get; private set; }
    public CasillaCell cell { get; private set; }
    public string number { get; private set; }

    private Image fondo;
    private TextMeshProUGUI text;

    private void Awake()
    {
        fondo = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetEstado(CasillaEstado state, string number)
    {
        this.state = state;
        this.number = number;

        fondo.color = state.backgroundColor;
        text.color = state.textColor;
        text.text = number;
    }

    public void Spawn(CasillaCell cell)
    {
        if(this.cell != null)
        {
            this.cell.casilla = null;
        }

        this.cell = cell;
        this.cell.casilla = this;

        transform.position = cell.transform.position;
    }

    public void MoveTo(CasillaCell cell)
    {
        if (this.cell != null)
        {
            this.cell.casilla = null;
        }

        this.cell = cell;
        this.cell.casilla = this;

        transform.position = cell.transform.position;
    }
}
