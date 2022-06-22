using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinScore : MonoBehaviour
{
    //public Text coinScoreText;
    private int coinNumber = 0;
    private int ringNumber = 0;
    void Start()
    {
        coinNumber = 0;
        //coinScoreText.text = "x " + coinNumber.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            coinNumber++;
            Destroy(other.gameObject);
            UIController.instance.UpdateCoin(coinNumber);
        }
        if (other.tag == "Ring")
        {
            ringNumber++;
            UIController.instance.UpdateRing(ringNumber);
            Destroy(other.gameObject);
        }
    }



}
