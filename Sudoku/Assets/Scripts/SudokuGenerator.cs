/* C# program for Sudoku generator  */
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
public class SudokuGenerator : MonoBehaviour
{
    public static SudokuGenerator Instance;
    public List<Image> sudokuCell;
    public List<GameObject> lstContainer;
    public Image originCell;
    private int[,] mat;
    private int N; // number of columns/rows.
    private int SRN; // square root of N
    private int K; // No. Of missing digits
    private List<Image> lastHighlightCell = new List<Image>();
    private List<Image> lastHighlightSameCurrentInput = new List<Image>();
    [HideInInspector] public int x, y, id, currentInput;
    private int isFixedValue;
    private Color highlightSameCurrentInput = new Color32(138, 193, 229, 255);
    private Color highlightClickedCell = new Color32(138, 193, 229, 255);
    private Color highlightCell = new Color32(213, 232, 255, 255);
    private Color blueText = new Color32(0, 77, 135, 255);
    private Color redText = new Color32(186, 19, 20, 255);
    private Color blackText = new Color32(0, 0, 0, 255);
    private int[] blockFilledCount = new int[9];
    private int[] columnFilledCount= new int[9];
    private int[] rowFilledCount = new int[9];
    [HideInInspector] public bool isDraw;

    private void Awake()
    {
        Instance = this;
        N = 9; K = 42;
        //sudokuCell = new List<Image>(N*N);
        double SRNd = Math.Sqrt(N);
        SRN = (int)SRNd;
        mat = new int[N, N];
        for (int i=0; i<N*N; i++)
        {
            Image cell = Instantiate(originCell);
            cell.transform.parent = lstContainer[i / 9].transform;
            sudokuCell.Add(cell);
        }
        FillValues();
    }
    public void Delete()
    { 
        int currentInput = sudokuCell[id].transform.GetChild(1).gameObject.GetComponent<CellHandler>().currentInput;
        if (currentInput != 0 && currentInput == mat[x, y])
        {
            SubtractFilledCount(id, x, y);
        }
        sudokuCell[id].transform.GetChild(1).gameObject.GetComponent<CellHandler>().DeleteCurentInput();
        sudokuCell[id].transform.GetChild(1).gameObject.GetComponent<CellHandler>().DeleteDraw();
    }

    public void Draw(int number)
    {
        //xoa gia tri da co san tren o + --FilledCount[] neu currentInput=mat[x,y]
        int currentInput = sudokuCell[id].transform.GetChild(1).gameObject.GetComponent<CellHandler>().currentInput;
        if (currentInput != 0 && currentInput == mat[x, y])
        {
            SubtractFilledCount(id, x, y);
        }
        sudokuCell[id].transform.GetChild(1).gameObject.GetComponent<CellHandler>().DeleteCurentInput();

        Transform go = sudokuCell[id].transform.GetChild(1);
        bool state = go.GetChild(number - 1).gameObject.activeSelf;
        if (state)
        {
            go.GetChild(number - 1).gameObject.SetActive(false);
        }
        else
            go.GetChild(number - 1).gameObject.SetActive(true);
    }
    public void NewGame (int k)
    {
        K = k;
        mat = new int[N, N];
        Timer.Instance.time = 0;
        for (int i=0; i<N; i++)
        {
            blockFilledCount[i] = 0;
            columnFilledCount[i] = 0;
            rowFilledCount[i] = 0;
        }
        //UnHighlight
        if (lastHighlightCell.Count != 0)
            lastHighlightCell.ForEach(x => x.transform.GetChild(1).gameObject.GetComponent<CellHandler>().UnHighlightCell());
        lastHighlightCell = new List<Image>();

        if (lastHighlightSameCurrentInput.Count != 0)
            lastHighlightSameCurrentInput.ForEach(x => x.transform.GetChild(1).gameObject.GetComponent<CellHandler>().UnHighlightCell());
        lastHighlightSameCurrentInput = new List<Image>();

        FillValues();
    }
    public void CheckHighlight(int x, int y, int id, int currentInput, int isFixedValue)
    {
        this.x = x;
        this.y = y;
        this.id = id;
        this.currentInput = currentInput;
        this.isFixedValue = isFixedValue;

        //unhighlight
        if (lastHighlightCell.Count != 0)
            lastHighlightCell.ForEach(x => x.transform.GetChild(1).gameObject.GetComponent<CellHandler>().UnHighlightCell());
        lastHighlightCell = new List<Image>();

        if (lastHighlightSameCurrentInput.Count != 0)
            lastHighlightSameCurrentInput.ForEach(x => x.transform.GetChild(1).gameObject.GetComponent<CellHandler>().UnHighlightCell());
        lastHighlightSameCurrentInput = new List<Image>();

        //hightlight nhung o cung hang, cung cot, cung block
        lastHighlightCell = sudokuCell.Where(
            var => (var.transform.GetChild(1).gameObject.GetComponent<CellHandler>().x == this.x
            || var.transform.GetChild(1).gameObject.GetComponent<CellHandler>().y == this.y 
            || var.transform.GetChild(1).gameObject.GetComponent<CellHandler>().id/9 == this.id/9)
        ).ToList();
        lastHighlightCell.ForEach(x => x.transform.GetChild(1).gameObject.GetComponent<CellHandler>().GetComponent<CellHandler>().SetImgColor(highlightCell)); //highlight cell

        //highlight nhung o cung gia tri
        if (currentInput != 0)
        {
            //unhighlight
            if (lastHighlightSameCurrentInput.Count != 0)
                lastHighlightSameCurrentInput.ForEach(x => x.transform.GetChild(1).gameObject.GetComponent<CellHandler>().UnHighlightCell());
            lastHighlightSameCurrentInput = new List<Image>();
            //highlight
            lastHighlightSameCurrentInput = sudokuCell.Where(
                var => var.transform.GetChild(1).gameObject.GetComponent<CellHandler>().currentInput == this.currentInput)
           .ToList();
            if (lastHighlightSameCurrentInput.Count != 0)
                lastHighlightSameCurrentInput.ForEach(x => x.transform.GetChild(1).gameObject.GetComponent<CellHandler>().SetImgColor(highlightSameCurrentInput));
        }
    }

    public void SelectNumber(int input)
    {
        //Debug.Log(x + " " + y + " " + isFixedValue);
        if (isFixedValue == 1) return;
        // UnHighlight
        if (lastHighlightSameCurrentInput.Count != 0)
            lastHighlightSameCurrentInput.ForEach(x => x.transform.GetChild(1).gameObject.GetComponent<CellHandler>().UnHighlightCell());
        
        lastHighlightSameCurrentInput = new List<Image>();
        //highlight o cung so
        lastHighlightSameCurrentInput = sudokuCell.Where(
                var => var.transform.GetChild(1).gameObject.GetComponent<CellHandler>().currentInput == input
                && (var.transform.GetChild(1).gameObject.GetComponent<CellHandler>().id != id))
           .ToList();
        if (lastHighlightSameCurrentInput.Count != 0)
            lastHighlightSameCurrentInput.ForEach(x => x.transform.GetChild(1).gameObject.GetComponent<CellHandler>().SetImgColor(highlightClickedCell));
        
        if (input == mat[x, y])
        {
            GameObject tmp = sudokuCell[id].transform.GetChild(1).gameObject;
            //Debug.Log("Current Input: " + tmp.GetComponent<CellHandler>().currentInput);
            if (tmp.GetComponent<CellHandler>().currentInput != mat[x, y])
            {
                AddFilledCount(id, x, y);
            }
            tmp.GetComponent<TextMeshProUGUI>().color = blueText;  //hien so mau xanh
            tmp.GetComponent<CellHandler>().SetText(input);
            tmp.GetComponent<CellHandler>().SetCurrentInput(input);
        }
        else
        {
            GameObject tmp = sudokuCell[id].transform.GetChild(1).gameObject;
            //Debug.Log("Current Input: " + tmp.GetComponent<CellHandler>().currentInput);
            if (tmp.GetComponent<CellHandler>().currentInput == mat[x, y])
            {
                SubtractFilledCount(id, x, y);
            }
            tmp.GetComponent<TextMeshProUGUI>().color = redText;  //hien so mau do
            tmp.GetComponent<CellHandler>().SetText(input);
            tmp.GetComponent<CellHandler>().SetCurrentInput(input);
            //hightlight do o khien pham loi
            if (lastHighlightSameCurrentInput.Count != 0)
                foreach (var cell in lastHighlightSameCurrentInput)
                {
                    GameObject cellO = cell.transform.GetChild(1).gameObject;
                    if (cellO.GetComponent<CellHandler>().id / 9 == id / 9 
                        || cellO.GetComponent<CellHandler>().x == x 
                        || cellO.GetComponent<CellHandler>().y == y)
                            cellO.GetComponent<CellHandler>().SetImgColor(redText);
                }
        }
        //Debug.Log("Count: " + filledCount);
        CheckFullBlock(id);
        CheckFullColumn(y);
        CheckFullRow(x);
        CheckWin();
    }
    public void Hint()
    {
        Debug.Log("Hint function");
        //Xoa het nhung gi da draw
        sudokuCell[id].transform.GetChild(1).gameObject.GetComponent<CellHandler>().DeleteDraw();
        if (x>=0 && y>=0)
        {
            int currentInput = sudokuCell[id].transform.GetChild(1).gameObject.GetComponent<CellHandler>().currentInput;
            if (currentInput != mat[x, y])
            {
                Debug.Log("Now select number");
                int input = mat[x, y];
                SelectNumber(input);
            }
        }
    }
    public void SubtractFilledCount(int id, int x, int y)
    {
        blockFilledCount[id / 9]--;
        columnFilledCount[y]--;
        rowFilledCount[x]--;
    }
    public void AddFilledCount(int id, int x, int y)
    {
        blockFilledCount[id / 9]++;
        columnFilledCount[y]++;
        rowFilledCount[x]++;
    }
    private void CheckFullBlock(int id)
    {
        id = id / 9 * 9;
        if (blockFilledCount[id/9] == N) 
        {
            Debug.Log("Full Block");
            for (int i = 0; i < N; i++)
            {
                sudokuCell[id+i].transform.GetChild(1).gameObject.GetComponent<CellHandler>().isChange = true;
            }
        }
    }
    private void CheckFullRow (int x)
    {
        if (rowFilledCount[x] == N)
        {
            for (int y = 0; y< N; y++)
            {
                int i = GetIndex(x, y);
                sudokuCell[i].transform.GetChild(1).gameObject.GetComponent<CellHandler>().isChange = true;
            }
            Debug.Log("Full Row");
        }
    }

    private void CheckFullColumn (int y)
    {
        if (columnFilledCount[y] == N)
        {
            for (int x = 0; x < N; x++)
            {
                int i = GetIndex(x, y);
                sudokuCell[i].transform.GetChild(1).gameObject.GetComponent<CellHandler>().isChange = true;
            }
            Debug.Log("Full Column");
        }
    }
    private void CheckWin()
    {
        bool isWin = true;
        for (int i=0; i<N; i++)
        {
            if (blockFilledCount[i] != N || columnFilledCount[i] != N || rowFilledCount[i] != N)
                isWin = false;
        }
        if (isWin)
        {
            Debug.Log("Win");
            for (int i = 0; i < N*N; i++)
            {
                sudokuCell[i].transform.GetChild(1).gameObject.GetComponent<CellHandler>().isChange = true;
            }
        }
    }
    private int GetIndex (int i, int j)
    {
        return (i / 3 * 27 + j / 3 * 9) + (i % 3 * 3 + j % 3);
    }
    public void FillValues()
    {
        // Fill the diagonal of SRN x SRN matrices
        FillDiagonal();

        // Fill remaining blocks
        FillRemaining(0, SRN);
        for (int i= 0; i < N; i++)  
            for (int j=0; j<N; j++)
            {
                int index = GetIndex(i, j); 
                GameObject tmp = sudokuCell[index].transform.GetChild(1).gameObject;
                tmp.GetComponent<CellHandler>().SetText(mat[i, j]);
                tmp.GetComponent<CellHandler>().SetTextColor(blackText);
                tmp.GetComponent<CellHandler>().SetVar(i, j, index, mat[i, j], 1, sudokuCell[index]);
                tmp.GetComponent<CellHandler>().DeleteDraw();
                AddFilledCount(index, i, j);
            }

        // Remove Randomly K digits to make game
        RemoveKDigits();
        //foreach (var cell in sudokuCell)
        //{
        //    Debug.Log(cell.transform.GetChild(0).gameObject.GetComponent<CellHandler>().isFixedValue);
        //}
    }

    // Fill the diagonal SRN number of SRN x SRN matrices
    void FillDiagonal()
    {

        for (int i = 0; i < N; i = i + SRN)

            // for diagonal box, start coordinates->i==j
            FillBox(i, i);
    }

    // Returns false if given 3 x 3 block contains num.
    bool UnUsedInBox(int rowStart, int colStart, int num)
    {
        for (int i = 0; i < SRN; i++)
            for (int j = 0; j < SRN; j++)
                if (mat[rowStart + i, colStart + j] == num)
                    return false;

        return true;
    }

    // Fill a 3 x 3 matrix.
    void FillBox(int row, int col)
    {
        int num;
        for (int i = 0; i < SRN; i++)
        {
            for (int j = 0; j < SRN; j++)
            {
                do
                {
                    num = RandomGenerator(N);
                }
                while (!UnUsedInBox(row, col, num));

                mat[row + i, col + j] = num;
            }
        }
    }

    // Random generator
    int RandomGenerator(int num)
    {
        System.Random rand = new System.Random();
        return (int)Math.Floor((double)(rand.NextDouble() * num + 1));
    }

    // Check if safe to put in cell
    bool CheckIfSafe(int i, int j, int num)
    {
        return (UnUsedInRow(i, num) &&
                UnUsedInCol(j, num) &&
                UnUsedInBox(i - i % SRN, j - j % SRN, num));
    }

    // check in the row for existence
    bool UnUsedInRow(int i, int num)
    {
        for (int j = 0; j < N; j++)
            if (mat[i, j] == num)
                return false;
        return true;
    }

    // check in the row for existence
    bool UnUsedInCol(int j, int num)
    {
        for (int i = 0; i < N; i++)
            if (mat[i, j] == num)
                return false;
        return true;
    }

    // A recursive function to fill remaining
    // matrix
    bool FillRemaining(int i, int j)
    {
        //  System.out.println(i+" "+j);
        if (j >= N && i < N - 1)   //xuong hang tiep theo
        {
            i = i + 1; 
            j = 0;
        }
        if (i >= N && j >= N)  //fill completely
            return true;

        if (i < SRN)    //3 first rows
        {
            if (j < SRN)   //neu o o 3x3 dau tien, thi nhay sang o ben canh
                j = SRN;
        }
        else if (i < N - SRN)   // 4-6th rows
        {
            if (j == (int)(i / SRN) * SRN)  //neu o o 3x3 o giua
                j = j + SRN;                  // nhay sang o ben canh
        }
        else                     // 7-9th rows
        {
            if (j == N - SRN)      // neu o o cuoi cung, thi nhay xuong hang tiep theo
            {
                i = i + 1;
                j = 0;
                if (i >= N)           // fill completely
                    return true;
            }
        }
         
        for (int num = 1; num <= N; num++)          //sau khi da xac dinh vi tri cua o minh can xet
        {
            if (CheckIfSafe(i, j, num))
            {
                mat[i, j] = num;
                if (FillRemaining(i, j + 1))
                    return true;

                mat[i, j] = 0;  // tra ve gia tri ban dau
            }
        }
        return false;    // khong the gan gia tri nao nua --> khong the tao bang
    }

    // Remove the K no. of digits to
    // complete game
    public void RemoveKDigits()
    {
        //chi chuyen text cua o do = "". Con gia tri mat thi giu nguyen
        int count = K;
        while (count != 0)
        {
            int cellId = RandomGenerator(N * N) - 1;

            // System.out.println(cellId);
            // extract coordinates i  and j
            int i = (cellId / N);
            int j = cellId % 9;
            if (j != 0)
                j = j - 1;   //why?!?

            int index = (i / 3 * 27 + j / 3 * 9) + (i % 3 * 3 + j % 3);  
            GameObject tmp = sudokuCell[index].transform.GetChild(1).gameObject;
            if (tmp.GetComponent<TextMeshProUGUI>().text == mat[i, j].ToString()) //kiem tra chua duoc xoa
            {
                count--;
                SubtractFilledCount(index, i, j);
                tmp.GetComponent<TextMeshProUGUI>().text = "";
                tmp.GetComponent<CellHandler>().SetCurrentInput(0);
                tmp.GetComponent<CellHandler>().SetIsFixedValue(0);
            }
        }
    }
    
}