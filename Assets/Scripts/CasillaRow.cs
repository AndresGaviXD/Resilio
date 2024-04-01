
using UnityEngine;

public class CasillaRow : MonoBehaviour
{
    public CasillaCell[] cells { get; private set; }

    private void Awake()
    {
        cells = GetComponentsInChildren<CasillaCell>();
    }
}
