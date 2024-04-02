using UnityEngine;

public class Casilla : MonoBehaviour
{
    public CasillaEstado state { get; private set; }
    public CasillaCell cell { get; private set; }
    public int number { get; private set; }
}
