using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum ToolType
{
    Undo,
    Erase,
    Pencil,
    Hint,
    Pause,
    Setting,
    NewGame,
    None
}

public class ToolHandler : MonoBehaviour, IPointerDownHandler
{
    public Image pauseBackground;
    public Image newGameBg;
    public Image settingBg;
    public static ToolHandler Instance;
    [SerializeField] private ToolType toolType;
    public TextMeshProUGUI levelText;

    void Start()
    {
        Instance = this;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        switch (toolType)
        {
            case ToolType.Undo:
                Debug.Log("Undo");
                break;
            case ToolType.Erase:
                Debug.Log("Erase");
                //erase draw
                SudokuGenerator.Instance.Delete();
                //erase number
                /*xoa so + nhung o duoc highlight vi cung so*/
                break;
            case ToolType.Pencil:
                Debug.Log("Pencil");
                TextMeshProUGUI pencilText = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

                if (pencilText.text == "ON")
                {
                    pencilText.text = "OFF";
                    SudokuGenerator.Instance.isDraw = false;
                    Color newColor = GetComponent<Image>().color;
                    newColor.a = 0.5f;
                    GetComponent<Image>().color = newColor;
                }
                else
                {
                    Debug.Log("Draw");
                    pencilText.text = "ON";
                    SudokuGenerator.Instance.isDraw = true;
                    Color newColor = GetComponent<Image>().color;
                    newColor.a = 1;
                    GetComponent<Image>().color = newColor;
                }
                break;
            case ToolType.Hint:
                SudokuGenerator.Instance.Hint();
                break;
            case ToolType.Pause:
                pauseBackground.gameObject.SetActive(true);
                Timer.Instance.timerIsRunning = false;
                break;
            case ToolType.NewGame:
                newGameBg.gameObject.SetActive(true);
                Timer.Instance.timerIsRunning = false;
                break;
            case ToolType.Setting:
                Debug.Log("Setting");
                settingBg.gameObject.SetActive(true);
                Timer.Instance.timerIsRunning = false;
                break;
        }
        //Debug.Log(gameObject.name);
        //if (gameObject.name == "Hint")
        //    SudokuGenerator.Instance.Hint();
        //else if (gameObject.name == "Pause")
        //{
        //    pauseBackground.gameObject.SetActive(true);
        //    Timer.Instance.timerIsRunning = false;
        //}
        //else if (gameObject.name == "NewGame")
        //{
        //    //Debug.Log("New Game");
        //    newGameBg.gameObject.SetActive(true);
        //    Timer.Instance.timerIsRunning = false;
        //}
        //else if (gameObject.name == "Pencil")
        //{
        //    Debug.Log("Pencil");
        //    TextMeshProUGUI pencilText = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        //    if (pencilText.text == "ON")
        //    {
        //        pencilText.text = "OFF";
        //        NumberInput.Instance.draw = false;
        //        Color newColor = GetComponent<Image>().color;
        //        newColor.a = 0.5f;
        //        GetComponent<Image>().color = newColor;
        //    }
        //    else
        //    {
        //        Debug.Log("Draw");
        //        pencilText.text = "ON";
        //        NumberInput.Instance.draw = true;
        //        Color newColor = GetComponent<Image>().color;
        //        newColor.a = 1;
        //        GetComponent<Image>().color = newColor;
        //    }
        //}
        //else if (gameObject.name == "Undo")
        //{
        //    Debug.Log("Undo");
        //}
        //else if (gameObject.name == "Erase")
        //{
        //    Debug.Log("Erase");
        //    //erase draw
        //    CellHandler.Instance.Delete();

        //    //erase number
        //    /*xoa so + nhung o duoc highlight vi cung so*/
        //}
        //else if (gameObject.name == "Setting")
        //{
        //    Debug.Log("Setting");
        //    settingBg.gameObject.SetActive(true);
        //    Timer.Instance.timerIsRunning = false;
        //}
    }

    public void Continue()
    {
        settingBg.gameObject.SetActive(false);
        Timer.Instance.timerIsRunning = true;
    }
    public void Resume()
    {
        //Debug.Log("Resume");
        pauseBackground.gameObject.SetActive(false);
        Timer.Instance.timerIsRunning = true;
    }
    public void ChooseLevel()
    {
        Debug.Log(levelText.text);
        if (gameObject.name == "Easy")
        {
            //Debug.Log("Easy");
            newGameBg.gameObject.SetActive(false);
            SudokuGenerator.Instance.NewGame(42);
            levelText.text = "Easy";
        }
        else if (gameObject.name == "Medium")
        {
            //Debug.Log("Medium");
            newGameBg.gameObject.SetActive(false);
            SudokuGenerator.Instance.NewGame(51);
            levelText.text = "Medium";
        }
        else if (gameObject.name == "Hard")
        {
            //Debug.Log("Hard");
            newGameBg.gameObject.SetActive(false);
            SudokuGenerator.Instance.NewGame(58);
            levelText.text = "Hard";
        }
        else if (gameObject.name == "Cancel")
        {
            newGameBg.gameObject.SetActive(false);
        }
        Timer.Instance.timerIsRunning = true;
    }
}
