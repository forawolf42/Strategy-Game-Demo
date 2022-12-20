using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingBehaviour : MonoBehaviour  , IPointerClickHandler // 2
    , IDragHandler
    ,IPointerDownHandler
{
    [SerializeField] GameObject prefab = null;
    private GameObject obj = null;
    

    public void OnPointerClick(PointerEventData eventData) // 3
    {
    }
 
    public void OnDrag(PointerEventData eventData)
    {
        print("I'm being dragged!");
        if (obj != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            

            float xRound = (float)Math.Round(worldPosition.x / 0.5f) * 0.5f;
            if (xRound %1==0)
            {
                xRound += 0.5f;
            }
            float yRound = (float)Math.Round(worldPosition.y / 0.5f) * 0.5f;
            if (yRound %1==0)
            {
                yRound += 0.5f;
            }
            obj.transform.position = new Vector3(xRound,yRound,-1);
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        obj = Instantiate(prefab, Input.mousePosition, quaternion.identity);

    }
}
