using UnityEngine;

public class Casilla : MonoBehaviour
{
    public CasillaState state { get; private set; }
    public CasillaCell cell { get; private set; }
    public int number { get; private set; }
}
