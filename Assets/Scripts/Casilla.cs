using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Casilla : MonoBehaviour
{
    public CasillaEstado state { get; private set; }
    public CasillaCell cell { get; private set; }
    public int number { get; private set; }
    public bool locked { get;  set; }

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
        text.color = state.textColor;
        text.text = number.ToString();
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

        StartCoroutine(Animate(cell.transform.position, false));
    }

    public void Merge(CasillaCell cell)
    {
        if (this.cell != null)
        {
            this.cell.casilla = null;
        }

        this.cell = null;
        cell.casilla.locked = true;


        StartCoroutine(Animate(cell.transform.position, true));

    }

    private IEnumerator Animate(Vector3 to, bool merging)
    {
        float elapsed = 0f;
        float duracion = 0.1f;

        Vector3 from = transform.position;

        while (elapsed < duracion)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duracion);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = to;

        if (merging)
        {
            Destroy(gameObject);
        }
    }
}
