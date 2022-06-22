using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //private List<Transform> transArray = new List<Transform>();
    private Transform[] transArray;
    private GameObject redObj, blueObj;

    void Start()
    {
        transArray = new Transform[2];
        redObj = GameObject.FindWithTag("Red");
        blueObj = GameObject.FindWithTag("Blue");
        transArray[0] = redObj.transform;
        transArray[1] = blueObj.transform;
        //Debug.Log(transArray[0].name);
        //Debug.Log(transArray[1].name);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Rotate");
            redObj.transform.Rotate(0f, 0f, 45f, Space.Self);
            blueObj.transform.Rotate(0f, 0f, -45f, Space.Self);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            //swap X-position
            Vector3 tmpblue = blueObj.transform.position;
            tmpblue.y = redObj.transform.position.y;
            Vector3 tmpred = redObj.transform.position;
            tmpred.y = blueObj.transform.position.y;
            redObj.transform.position = tmpred;
            blueObj.transform.position = tmpblue;

            //Change color
            Renderer ren = redObj.GetComponent<PrintAndHide>().rend;
            ren.material.color = new Color(Random.Range(51 / 255f, 1f), 0f, 0f, 0f);
            //Debug.Log("Red: " + ren.material.color);
            ren = blueObj.GetComponent<PrintAndHide>().rend;
            ren.material.color = new Color(0f, 0f, Random.Range(51 / 255f, 1f), 0f);
            //Debug.Log("Blue: " + ren.material.color);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (redObj.GetComponent<PrintAndHide>())
                Destroy(redObj.GetComponent<PrintAndHide>());
            else
            {
                redObj.AddComponent<PrintAndHide>();
                redObj.GetComponent<PrintAndHide>().rend = redObj.transform.GetChild(0).GetComponent<Renderer>();
            }
            if (blueObj.GetComponent<PrintAndHide>())
                Destroy(blueObj.GetComponent<PrintAndHide>());
            else
            {
                blueObj.AddComponent<PrintAndHide>();
                blueObj.GetComponent<PrintAndHide>().rend = blueObj.transform.GetChild(0).GetComponent<Renderer>();
            }
        }
        //Debug.Log(Time.time);
        //Debug.Log(Time.deltaTime);
    }
}
