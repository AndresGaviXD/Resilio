using UnityEngine;

public class Cuadrado : MonoBehaviour
{
    private Barco barcoEnCuadrado; // Referencia al barco actualmente en este cuadrado

    void OnMouseDown()
    {
        if (barcoEnCuadrado == null && Barco.barcoSeleccionado != null)
        {
            // Si el cuadrado está vacío y hay un barco seleccionado
            Barco.barcoSeleccionado.MoverABarco(transform.position);
            barcoEnCuadrado = Barco.barcoSeleccionado;
        }
    }
    public bool HasShip()
    {
        return barcoEnCuadrado != null;
    }

    public void LiberarCuadrado()
    {
        barcoEnCuadrado = null;
    }

    // Otras funciones y lógica específica para el cuadrado...
}
