using UnityEngine;

public class Barco : MonoBehaviour
{
    public static Barco barcoSeleccionado;
    private Vector3 posiciónInicial;
    private Quaternion rotaciónInicial;

    void Start()
    {
        posiciónInicial = transform.position;
        rotaciónInicial = transform.rotation;
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

    public void MoverABarco(Vector2 posición)
    {
        // Asegúrate de que la posición esté dentro de los límites del tablero
        Collider2D[] colliders = Physics2D.OverlapPointAll(posición);
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
