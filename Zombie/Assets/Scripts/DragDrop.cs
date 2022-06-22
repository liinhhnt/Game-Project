using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject clonedObj;
    private RectTransform rectTransform;
    private void Awake()
    {
        clonedObj = Instantiate(gameObject);
        rectTransform = clonedObj.GetComponent<RectTransform>();
    }
    public void OnBeginDrag (PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("OnEndDrag");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On Pointer Down");
        //throw new System.NotImplementedException();
    }
}
