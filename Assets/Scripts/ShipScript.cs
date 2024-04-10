using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ShipScript : MonoBehaviour
{

    public enum ShipType
    {
        OneCellShip,
        TwoCellsShip,
        ThreeCellsShip,
    }

    private ShipType type;
    private bool isShipPlaced = false;
    private bool isRotated = false;
    private int Counter = 0;
    private Transform rightBondTransform;
    private Transform leftBondTransform;
    private float rightBond = 7f;
    private float leftBond = -1.8f;
    private float downBond = -4.5f;
    private float upBond = 4.5f;

    [SerializeField] GameObject[] ships;
    [SerializeField] float xOffset;
    [SerializeField] float xOffsetRotated;
    [SerializeField] float yOffset;
    [SerializeField] AstuciaGameManager gameManager;
    [SerializeField] CellScript selectedCell;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<AstuciaGameManager>();
    }

    public void ChangeColor(GameObject obj, ShipType type)
    {
        isShipPlaced = false;

        if (type == ShipType.OneCellShip)
        {
            Color customColor = new Color(0.83f, 0.68f, 0.46f, 0.8f);
            StartCoroutine(Blink(obj.GetComponent<Renderer>().material, customColor));
        }
        else if (type == ShipType.TwoCellsShip)
        {
            Color customColor = new Color(0.71f, 0.40f, 0.16f, 1.0f);
            StartCoroutine(Blink(obj.GetComponent<Renderer>().material, customColor));
        }
        else if (type == ShipType.ThreeCellsShip)
        {
            Color customColor = new Color(0.2f, 0.6f, 0.4f, 1.0f);
            StartCoroutine(Blink(obj.GetComponent<Renderer>().material, customColor));
        }
        else
        {
            Color customColor = new Color(0.2f, 0.4f, 0.6f, 0.8f);
            StartCoroutine(Blink(obj.GetComponent<Renderer>().material, customColor));
        }
    }

    public void MoveShip(Vector3 position, ShipType type)
    {
        //add offset if rotated ship need this
        if (isRotated)
        {
            transform.position = new Vector3(position.x + xOffsetRotated, position.y + yOffset, position.z);
        }

        else if (!isRotated)
        {
            transform.position = new Vector3(position.x + xOffset, position.y, position.z);
        }

        rightBondTransform = transform.Find("RightBond");
        leftBondTransform = transform.Find("LeftBond");

        //if ship is outside the borders
        if (rightBondTransform.position.x >= rightBond || rightBondTransform.position.y <= downBond) 
        {
            isShipPlaced = false;
            gameManager.StartCoroutine(gameManager.Wait());
        }
        else if(leftBondTransform.position.x <= leftBond || leftBondTransform.position.y >= upBond)
        {
            isShipPlaced = false;
            gameManager.StartCoroutine(gameManager.Wait());

        }
        else
        {
            isShipPlaced = true;
        }
        
        //if ships overlap
        foreach (GameObject ship in ships)
        {
            if (Physics2D.OverlapBox(transform.position, transform.localScale, 0f, LayerMask.GetMask("Ship")))
            {
                isShipPlaced = false;
                gameManager.StartCoroutine(gameManager.Wait());
                break;
            }

        }

        Physics.SyncTransforms();

    }


    public ShipType CheckType(GameObject ship)
    {
        if (ship.CompareTag("1cell_ship"))
        {
            return ShipType.OneCellShip;
        }
        else if (ship.CompareTag("2cells_ship"))
        {
            return ShipType.TwoCellsShip;
        }
        else
        {
            return ShipType.ThreeCellsShip;
        }
    }

    private IEnumerator Blink(Material material, Color blinkColor)
    {
        Color originalColor = material.color;
        float blinkDuration = 0.2f; 

        while (!isShipPlaced)
        {
            material.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            material.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    public void RotateShip()
    {
        Counter++;

        if (Counter == 2)
        {
            transform.Rotate(Vector3.forward, 90f);
            isRotated = false;
            Counter = 0;
        }
        else
        {
            transform.Rotate(Vector3.forward, -90f);
            isRotated = true;
        }
    }

    public bool ReturnPlacement()
    {
        return isShipPlaced;
    }

}
