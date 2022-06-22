using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PrintAndHide : MonoBehaviour
{
    private int i, a;
    public Renderer rend;
    void Start()
    {
        i = 0;
        System.Random rnd = new System.Random();
        a = rnd.Next(200, 251);

    }

    // Update is called once per frame
    //void Update()
    //{
    //    ++i;
    //    if (gameObject.tag == "Red" && i == 100)
    //    {
    //        gameObject.SetActive(false);
    //        Debug.Log("Deactive Red Object");
    //    }
    //    else if (gameObject.tag == "Blue" && i==a)
    //    {
    //        gameObject.GetComponent<Renderer>().enabled = false;
    //        Debug.Log("deactive Blue Renderer");
    //    }
    //    //else Debug.Log(gameObject.name + ": " + i);
    //}
}
