using System;
using UnityEngine;
public class ProductionManager : MonoBehaviour
{
    public static Action OnMouseClickedLeft;
    public static Action<Transform> OnMouseClickedRight;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClickedLeft?.Invoke();
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = worldPosition;
            worldPosition.z = -1;
            OnMouseClickedRight?.Invoke(transform);
            return;
        }
    }
}
