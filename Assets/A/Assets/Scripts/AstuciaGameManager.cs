using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class AstuciaGameManager : GameManager
{
    private int numberOfShips = 8;
    private Vector3 chosenPosition;
    private bool isShipChosen = false;
    private bool isProperlyPlaced;

    [SerializeField] ShipScript.ShipType type;
    [SerializeField] ShipScript selectedShip;
    [SerializeField] CellScript selectedCell;
    [SerializeField] Button nextButton;
    [SerializeField] Button textChangePosition;
    [SerializeField] Collider2D dockCollider;

    void Start()
    {
        selectedShip = null;
        selectedCell = null;

        dockCollider = GameObject.Find("Dock").GetComponent<Collider2D>();
        nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        textChangePosition = GameObject.Find("ChangePosition").GetComponent<Button>();

        nextButton.gameObject.SetActive(false);
        textChangePosition.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(nextButton == null || textChangePosition == null)
        {
            nextButton = GameObject.Find("NextButton").GetComponent<Button>();
            textChangePosition = GameObject.Find("ChangePosition").GetComponent<Button>();
        }

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!isShipChosen)
            {
                if (hit.collider != null)
                {
                    ShipScript ship = hit.collider.GetComponent<ShipScript>();

                    if (ship != null)
                    {
                        if (dockCollider.OverlapPoint(hit.point))
                        {
                            InitializeNewPosition(ship, hit);
                        }

                        else
                        {
                            UnityEngine.Debug.Log("You can't change position of your ship already!");
                        }
                    }
                }
            }

            if (isShipChosen)
            {
                if (hit.collider != null && hit.collider.CompareTag("Cell"))
                {

                    //tile logic
                    CellScript tile = hit.collider.GetComponent<CellScript>();
                    selectedCell = tile;

                    if (selectedCell.ReturnAvailability() == false)
                    {
                        chosenPosition = hit.transform.position;
                        selectedCell.SetAvailability();
                        selectedShip.MoveShip(chosenPosition, type);

                        //check if ship doesnt collide with other ships or is outside the border
                        isProperlyPlaced = selectedShip.ReturnPlacement();

                        if(isProperlyPlaced)
                        {
                            numberOfShips = numberOfShips - 1;
                            isShipChosen = false;

                        }
                        else if(!isProperlyPlaced)
                        {
                            selectedCell.ResetAvailability();
                            chosenPosition = Vector3.zero;
                        }

                    }

                    else if(selectedCell.ReturnAvailability() == true)
                    {
                        StartCoroutine(Wait());
                    }

                }

            }
        }

        //rotate ship
        if (Input.GetKeyDown(KeyCode.X) && isShipChosen)
        {
            selectedShip.RotateShip();
        }

        //if every ship is on board start the battle
        if (numberOfShips == 0)
        {
            DisplayButtonNext();
        }
    }

    public void DisplayButtonNext()
    {
        nextButton.gameObject.SetActive(true);
    }

    private void InitializeNewPosition(ShipScript ship, RaycastHit2D hit)
    {
        isShipChosen = true;
        selectedShip = ship;
        chosenPosition = hit.point;

        //get a type of ship
        type = ship.CheckType(hit.collider.gameObject);

        //initialize shining during click 
        ship.ChangeColor(hit.collider.gameObject, type);
    }

    public IEnumerator Wait()
    {
        textChangePosition.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        textChangePosition.gameObject.SetActive(false);
    }

}