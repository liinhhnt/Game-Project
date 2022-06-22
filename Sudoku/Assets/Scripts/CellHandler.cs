using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CellHandler : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector] public int x, y, id;
    public int currentInput;
    public Image cellImg;
    public static CellHandler Instance;
    public int isFixedValue;
    public bool isChange = false;
    public bool isIncrease;
    public Image borderImg;
    void Start()
    {
        borderImg = cellImg.transform.GetChild(0).gameObject.GetComponent<Image>();
        isIncrease = true;
    }    
    void FixedUpdate ()
    {
        if (isChange)
        {
            if (isIncrease)
            {
                Color colorBorder = borderImg.color;
                colorBorder.a += 0.05f;
                borderImg.color = colorBorder;
                if (colorBorder.a >= 0.9f)
                    isIncrease = false;
            }
            if (!isIncrease)
            {
                Color colorBorder = borderImg.color;
                colorBorder.a -= 0.05f;
                borderImg.color = colorBorder;
                if (borderImg.color.a <= 0.1f)
                {
                    isChange = false;
                    isIncrease = true;
                }
            }
        }
    }
    public void Draw(int number)
    {
        number--;
        GameObject go = transform.GetChild(number).gameObject;
        if (go.activeSelf) go.SetActive(false);
        else go.SetActive(true);
    }
    public void DeleteCurentInput()
    {
        if (isFixedValue == 0)
        {
            if (currentInput != 0)
            {
                currentInput = 0;
                GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
    public void DeleteDraw()
    {
        for (int i = 0; i < 9; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }    
    public void SetImgColor(Color color)
    {
        cellImg.color = color;
    }
    public void UnHighlightCell()
    {
        cellImg.color = new Color32(255, 255, 255, 255);
    }
    public void SetText (int value)
    {
        GetComponent<TextMeshProUGUI>().text = value.ToString();
    }   
    public void SetTextColor (Color color)
    {
        GetComponent<TextMeshProUGUI>().color = color;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SudokuGenerator.Instance.CheckHighlight(x, y, id, currentInput, isFixedValue);
        SetImgColor(new Color32(81, 142, 188, 255));
    }

    public void SetVar (int x, int y, int id, int currentInput, int isFixedValue, Image cellImg)
        {
            this.x = x;
            this.y = y;
            this.id = id;
            this.currentInput = currentInput;
            this.cellImg = cellImg;
            this.isFixedValue = isFixedValue;
    }
    public void SetCurrentInput (int currentInput)
        {
             this.currentInput = currentInput;
        }
    public void SetIsFixedValue (int isFixedValue)
    {
        this.isFixedValue = isFixedValue;
    }
}
