using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerTween : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private Tweener tweener;
    private List<GameObject> itemList;

    private void Start()
    {
        tweener = GetComponent<Tweener>();
        itemList = new List<GameObject>();
        itemList.Add(item);
        //tweener.AddTween(item.transform, item.transform.position, item.transform.position, 0.1f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject itemx = Instantiate(item, new Vector3(0, 0.5f, 0), Quaternion.identity);
            itemList.Add(itemx);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                Debug.Log("InputManagerTween + Key A: " + itemList[i].name);
                if (tweener.AddTween(itemList[i].transform, itemList[i].transform.position, new Vector3(-2.0f, 0.5f, 0.0f), 1.5f))
                {

                }
                   // break;
            }
            //Debug.Log("A");
            //tweener.AddTween(item.transform, item.transform.position, new Vector3 (-2.0f, 0.5f, 0.0f), 1.5f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                Debug.Log("InputManagerTween + Key D: " + itemList[i].name);

                if (tweener.AddTween(itemList[i].transform, itemList[i].transform.position, new Vector3(2.0f, 0.5f, 0.0f), 1.5f))
                {

                }
                   // break;
            }
            //tweener.AddTween(item.transform, item.transform.position, new Vector3(2.0f, 0.5f, 0.0f), 1.5f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                Debug.Log("InputManagerTween + Key S: " + itemList[i].name);

                if (tweener.AddTween(itemList[i].transform, itemList[i].transform.position, new Vector3(0.0f, 0.5f, -2.0f), 1.5f))
                {

                }
                   // break;
            }
            //tweener.AddTween(item.transform, item.transform.position, new Vector3(0.0f, 0.5f, -2.0f), 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                Debug.Log("InputManagerTween + Key W: " + itemList[i].name);

                if (tweener.AddTween(itemList[i].transform, itemList[i].transform.position, new Vector3(0.0f, 0.5f, 2.0f), 1.5f))
                {

                }
                    //break;
            }
            //tweener.AddTween(item.transform, item.transform.position, new Vector3(0.0f, 0.5f, 2.0f), 0.5f);
        }
    }
}
