using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour , IDragHandler , IBeginDragHandler , IEndDragHandler
{
    public bool isdrage;
    public Transform myPatnt;
    public Transform RefObject;
    public void OnBeginDrag(PointerEventData eventData)
    {
        isdrage = true;
        //
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isdrage)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(myPatnt);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, RefObject.position) < 0.5f)
        {
            Destroy(gameObject);
            transform.SetParent(transform.parent.parent);
        }
    }
}
