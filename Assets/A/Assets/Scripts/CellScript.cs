using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CellScript : MonoBehaviour
{
    [SerializeField] GameObject[] ships;
    public bool hasShip = false;
    private Renderer cellRenderer;

    void Update()
    {
        CheckCollision();
    }

    void CheckCollision()
    {
        Physics.SyncTransforms();

        foreach (GameObject obj in ships)
        {
            if (Physics2D.OverlapBox(transform.position, transform.localScale, 0f, LayerMask.GetMask("Ship")))
            {
                hasShip = true;
                break;
            }
            else
            {
                hasShip = false;
            }

        }
    }

    public bool ReturnAvailability()
    {
        return hasShip;
    }

    public void SetAvailability()
    {
        hasShip = true;
    }
    public void ResetAvailability()
    {
        hasShip = false;
    }


}