using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuItem : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Building prefab = null;
    private Building _selectedBuilding = null;

    public void OnDrag(PointerEventData eventData)
    {
        if (_selectedBuilding != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // dragging position
            float xRound = (float) Math.Round(worldPosition.x / 0.5f) * 0.5f;
            if (xRound % 1 == 0)
            {
                xRound += 0.5f;
            }

            float yRound = (float) Math.Round(worldPosition.y / 0.5f) * 0.5f;
            if (yRound % 1 == 0)
            {
                yRound += 0.5f;
            }

            _selectedBuilding.transform.position = new Vector3(xRound, yRound, -2);  // z -2  because to be seen top
            _selectedBuilding.OnDragging();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _selectedBuilding = Instantiate(prefab, Input.mousePosition, quaternion.identity);
        _selectedBuilding.OnDragStarted();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _selectedBuilding.OnDragEnd();
    }
}