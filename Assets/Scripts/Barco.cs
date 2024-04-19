using UnityEngine;

public class Barco : MonoBehaviour
{
    public static Barco barcoSeleccionado;
    private Vector3 posici�nInicial;
    private Quaternion rotaci�nInicial;

    void Start()
    {
        posici�nInicial = transform.position;
        rotaci�nInicial = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && barcoSeleccionado == this)
        {
            RotarBarco();
        }
    }

    void OnMouseDown()
    {
        if (barcoSeleccionado == this)
        {
            DesactivarSeleccion();
            barcoSeleccionado = null;
        }
        else
        {
            if (barcoSeleccionado != null)
            {
                barcoSeleccionado.DesactivarSeleccion();
            }

            SeleccionarBarco();
            barcoSeleccionado = this;
        }
    }

    void SeleccionarBarco()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void DesactivarSeleccion()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void RotarBarco()
    {
        transform.Rotate(0f, 0f, 90f);
    }

    public void MoverABarco(Vector2 posici�n)
    {
        // Aseg�rate de que la posici�n est� dentro de los l�mites del tablero
        Collider2D[] colliders = Physics2D.OverlapPointAll(posici�n);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Cuadrado"))
            {
                transform.position = collider.transform.position;
                break;
            }
        }
    }
}
