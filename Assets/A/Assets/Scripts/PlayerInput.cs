using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputField xInputField;
    [SerializeField] private InputField yInputField;
    [SerializeField] private Button confirmButton;
    private GameLogic gameLogic;

    void Start()
    {
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        gameLogic = FindObjectOfType<GameLogic>();
    }

    public void OnConfirmButtonClick()
    {
        int x = int.Parse(xInputField.text);
        int y = int.Parse(yInputField.text);
        
    }
}
