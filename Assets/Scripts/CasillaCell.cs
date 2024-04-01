using UnityEngine;

public class CasillaCell : MonoBehaviour
{
  public Vector2Int coordenadas { get; set; }
  public Casilla casilla { get; set; }

  public bool empty => casilla == null;
  public bool ocupado => casilla != null;
}



