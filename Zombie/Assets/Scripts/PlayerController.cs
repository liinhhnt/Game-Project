using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.gameObject.name);
                if (hit.transform.gameObject.tag == "Zombie")
                    hit.collider.GetComponent<ZombieController>().KillZombie();
            }
        }
    }
}
