using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class NumberInput : MonoBehaviour, IPointerDownHandler
{
    TextMeshProUGUI text;
    public static NumberInput Instance;
    private void Start()
    {
        Instance = this;
        //draw = false;
        text = GetComponent<TextMeshProUGUI>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Cursor Entering " + name);
        if (!SudokuGenerator.Instance.isDraw)
        {
            SudokuGenerator.Instance.Delete();
            SudokuGenerator.Instance.SelectNumber(Int32.Parse(text.text));
        }
        else
        {
            SudokuGenerator.Instance.Draw(Int32.Parse(text.text));
        }
    }
}
