using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    [SerializeField] private ScrollContent scrollContent;
    [SerializeField] private float outOfBoundsThreshold;
    private ScrollRect _scrollRect;
    private Vector2 _lastDragPosition;
    private bool _positiveDrag;

    private void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _scrollRect.vertical = scrollContent.Vertical;
        _scrollRect.horizontal = scrollContent.Horizontal;
        _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastDragPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (scrollContent.Vertical)
        {
            _positiveDrag = eventData.position.y > _lastDragPosition.y;
        }
        else if (scrollContent.Horizontal)
        {
            _positiveDrag = eventData.position.x > _lastDragPosition.x;
        }

        _lastDragPosition = eventData.position;
    }

    public void OnScroll(PointerEventData eventData)
    {
        outOfBoundsThreshold = Screen.height/2;

        if (scrollContent.Vertical)
        {
            _positiveDrag = eventData.scrollDelta.y > 0;
        }
        else
        {
            _positiveDrag = eventData.scrollDelta.y < 0;
        }
    }

    public void OnViewScroll()
    {
        if (scrollContent.Vertical)
        {
            HandleVerticalScroll();
        }
        else
        {
            HandleHorizontalScroll();
        }
    }

    private void HandleVerticalScroll()
    {
        int currItemIndex = _positiveDrag ? _scrollRect.content.childCount - 1 : 0;
        var currItem = _scrollRect.content.GetChild(currItemIndex);

        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = _positiveDrag ? 0 : _scrollRect.content.childCount - 1;
        Transform endItem = _scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (_positiveDrag)
        {
            newPos.y = endItem.position.y - Screen.height/5; 
        }
        else
        {
            newPos.y = endItem.position.y + Screen.height/5;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    private void HandleHorizontalScroll()
    {
        int currItemIndex = _positiveDrag ? _scrollRect.content.childCount - 1 : 0;
        var currItem = _scrollRect.content.GetChild(currItemIndex);
        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = _positiveDrag ? 0 : _scrollRect.content.childCount - 1;
        Transform endItem = _scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (_positiveDrag)
        {
            newPos.x = endItem.position.x - scrollContent.ChildWidth * 1.5f + scrollContent.ItemSpacing;
        }
        else
        {
            newPos.x = endItem.position.x + scrollContent.ChildWidth * 1.5f - scrollContent.ItemSpacing;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    private bool ReachedThreshold(Transform item)
    {
        if (scrollContent.Vertical)
        {
            float posYThreshold = transform.position.y + scrollContent.Height * 0.5f + outOfBoundsThreshold;
            float negYThreshold = transform.position.y - scrollContent.Height * 0.5f - outOfBoundsThreshold;
            return _positiveDrag
                ? item.position.y - scrollContent.ChildWidth * 0.5f > posYThreshold
                : item.position.y + scrollContent.ChildWidth * 0.5f < negYThreshold;
        }
        
        else
        {
            float posXThreshold = transform.position.x + scrollContent.Width * 0.5f + outOfBoundsThreshold;
            float negXThreshold = transform.position.x - scrollContent.Width * 0.5f - outOfBoundsThreshold;
            return _positiveDrag
                ? item.position.x - scrollContent.ChildWidth * 0.5f > posXThreshold
                : item.position.x + scrollContent.ChildWidth * 0.5f < negXThreshold;
        }
    }
}