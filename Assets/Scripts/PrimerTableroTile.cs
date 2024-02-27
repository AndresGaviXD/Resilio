using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrimerTableroTile : MonoBehaviour
{
    [SerializeField] GameObject[] canons;
    public bool tieneCanon = false;
    private Renderer cellRenderer;

    void Update()
    {
        CheckCollision();
    }

    void CheckCollision()
    {
        Physics.SyncTransforms();

        foreach (GameObject obj in canons)
        {
            if (Physics2D.OverlapBox(transform.position, transform.localScale, 0f, LayerMask.GetMask("Canon")))
            {
                tieneCanon = true;
                break;
            }
            else
            {
                tieneCanon = false;
            }

        }
    }

    public bool ReturnAvailability()
    {
        return tieneCanon;
    }

    public void SetAvailability()
    {
        tieneCanon = true;
    }
    public void ResetAvailability()
    {
        tieneCanon = false;
    }

}
