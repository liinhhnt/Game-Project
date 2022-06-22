using System;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid : MonoBehaviour
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private Vector3 originPosition;
    private TextMesh[,] debugTextArray;
    public Grid(int width, int height, float cellZise, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellZise;
        this.originPosition = originPosition;
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        //Debug.Log(width + " " + height);
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //Debug.Log(x + ", " + y);
                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellZise, cellZise) * 0.5f, 30, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        //SetValue(2, 1, 55);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    private void GetXY (Vector3 worldPosition, out int x, out int y)
    {   
        x = Mathf.FloorToInt((worldPosition - originPosition).x/cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y/cellSize);
    }
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }
    public void SetValue (Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y); 
        SetValue(x, y, value);
    }

    public int GetValue (int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y]; 
        } else
        {
            return -1;
        }
    }
    public int GetValue (Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}

