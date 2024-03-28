/*
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Canon1 : MonoBehaviour
{
    private bool estaCanonPuesto = false;
    private Transform rightBondTransform;
    private Transform leftBondTransform;
    private float rightBond = 7f;
    private float leftBond = -1.8f;
    private float downBond = -4.5f;
    private float upBond = 4.5f;

    [SerializeField] GameObject[] canons;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    [SerializeField] GridManager grid;
    [SerializeField] TileScript selectedTile;

    void Start()
    {
        grid = GameObject.Find("GridManager").GetComponent<GridManager>();
    }

    public void MoveCanon(Vector3 position)
    {

        rightBondTransform = transform.Find("RightBond");
        leftBondTransform = transform.Find("LeftBond");


        if (rightBondTransform.position.x >= rightBond || rightBondTransform.position.y <= downBond)
        {
            estaCanonPuesto = false;
            grid.StartCoroutine(grid.Wait());
        }
        else if (leftBondTransform.position.x <= leftBond || leftBondTransform.position.y >= upBond)
        {
            estaCanonPuesto = false;
            grid.StartCoroutine(grid.Wait());

        }
        else
        {
            estaCanonPuesto = true;
        }

        //if ships overlap
        foreach (GameObject obj in canons)
        {
            if (Physics2D.OverlapBox(transform.position, transform.localScale, 0f, LayerMask.GetMask("Canon")))
            {
                estaCanonPuesto = false;
                grid.StartCoroutine(grid.Wait());
                break;
            }

        }

        Physics.SyncTransforms();

    }

}
*/