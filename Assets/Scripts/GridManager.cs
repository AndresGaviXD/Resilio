using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GridManager : MonoBehaviour
{
    private int canons = 2;
    private Vector3 posicionElegida;
    private bool estaCanonElegido = false;
    private bool estaColocadoCorrectamente;

    [SerializeField] Canon1 canonElegido;
    [SerializeField] PrimerTableroTile t;
    [SerializeField] Button nextButton;
    [SerializeField] Button textChangePosition;
    [SerializeField] Collider2D dockCollider;

    void Start()
    {
        canonElegido = null;
        t = null;

        dockCollider = GameObject.Find("Dock").GetComponent<Collider2D>();
        nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        textChangePosition = GameObject.Find("ChangePosition").GetComponent<Button>();

        nextButton.gameObject.SetActive(false);
        textChangePosition.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (nextButton == null || textChangePosition == null)
        {
            nextButton = GameObject.Find("NextButton").GetComponent<Button>();
            textChangePosition = GameObject.Find("ChangePosition").GetComponent<Button>();
        }

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!estaCanonElegido)
            {
                if (hit.collider != null)
                {
                    Canon1 canon = hit.collider.GetComponent<Canon1>();

                    if (canon != null)
                    {
                        if (dockCollider.OverlapPoint(hit.point))
                        {
                            InitializeNewPosition(canon, hit);
                        }

                        else
                        {
                            UnityEngine.Debug.Log("You can't change position of your ship already!");
                        }
                    }
                }
            }

            if (estaCanonElegido)
            {
                if (hit.collider != null && hit.collider.CompareTag("Cell"))
                {

                    //tile logic
                    PrimerTableroTile tile = hit.collider.GetComponent<PrimerTableroTile>();
                    t = tile;

                    if (t.ReturnAvailability() == false)
                    {
                        posicionElegida = hit.transform.position;
                        t.SetAvailability();
                        canonElegido.MoveCanon(chosenPosition);

                        //check if ship doesnt collide with other ships or is outside the border
                        estaColocadoCorrectamente = canonElegido.ReturnPlacement();

                        if (estaColocadoCorrectamente)
                        {
                            canons = canons - 1;
                            estaCanonElegido = false;

                        }
                        else if (!estaColocadoCorrectamente)
                        {
                            t.ResetAvailability();
                            posicionElegida = Vector3.zero;
                        }

                    }

                    else if (t.ReturnAvailability() == true)
                    {
                        StartCoroutine(Wait());
                    }

                }

            }
        }

        //if every ship is on board start the battle
        if (canons == 0)
        {
            DisplayButtonNext();
        }
    }

    public void DisplayButtonNext()
    {
        nextButton.gameObject.SetActive(true);
    }


    public IEnumerator Wait()
    {
        textChangePosition.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        textChangePosition.gameObject.SetActive(false);
    }

}