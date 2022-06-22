using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Text coinScoreText;
    public List<Image> listRing;
    public Sprite fullRingSprite;


    private void Awake()
    {
        instance = this;
    }

    public void UpdateCoin(int count)
    {
        coinScoreText.text = "x " + count.ToString();
    }

    public void UpdateRing(int count)
    {
        listRing[count - 1].sprite = fullRingSprite;
    }

}
