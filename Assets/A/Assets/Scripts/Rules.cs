using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rules : MonoBehaviour
{
    [SerializeField] Button rulesButton;

    // Start is called before the first frame update
    void Start()
    {
        rulesButton = GameObject.Find("rulesButton").GetComponent<Button>();
        rulesButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Display()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        rulesButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        rulesButton.gameObject.SetActive(false);

    }
}
