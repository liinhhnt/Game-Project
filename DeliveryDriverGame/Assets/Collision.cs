using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    [SerializeField] float destroyDelay = 0.5f;
    [SerializeField] Color32 hasPackageColor = new Color32 (189, 241, 33, 255);
    [SerializeField] Color32 noPackageColor = new Color32 (34, 113, 209, 255);

    SpriteRenderer spriteRenderer;
    void Start() {
        spriteRenderer = GetComponent <SpriteRenderer> ();    
    }
    bool hasPackage;
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Oh, It's crash!!");
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package picked up!");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, destroyDelay);
        }
        if (other.tag == "Customer" && hasPackage)
        {
            Debug.Log ("Package Delivered!");
            spriteRenderer.color = noPackageColor;
            hasPackage = false;
        }
    }
}
