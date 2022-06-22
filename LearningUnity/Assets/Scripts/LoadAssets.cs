using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssets : MonoBehaviour
{
    public GameObject redObj;
    [SerializeField] private GameObject blueObj;
    private void Awake()
    {
        GameObject instantRedObj = Instantiate(redObj, new Vector3(2, 1, 0), Quaternion.identity);
        GameObject instantBlueObj = Instantiate(blueObj, new Vector3(-2, -1, 0), Quaternion.identity);
    }
}
