using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite[] spritesArray;
    public int index;
    //public Sprite sprite;
        
    void Start()
    {
        //sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (index < 6) index++;
            GetComponent<SpriteRenderer>().sprite = spritesArray[index];
        }
    }
}
