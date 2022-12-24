using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer = null;
    [SerializeField] protected string title;
    [SerializeField] protected string subTitle;
    [SerializeField] protected bool isHaveProduction = false;
    protected Sprite BuilgingSprite = null;
    private bool _dragging;
    private bool _isDanger;
    private void OnEnable()
    {
        BuilgingSprite = _renderer.sprite;
    }
    public virtual void OnMouseUp()
    {
        InformationController.Instance.UpdateInformationView(title, subTitle, BuilgingSprite);
        InformationController.Instance.UpdateProduction(false);
    }
    public  void OnDragStarted()
    {
        OnMouseUp();
    }
    public  void OnDragging()
    {
        _dragging = true;
        if (_isDanger)
        {
            return;
        }
        _renderer.color = new Color(255, 255, 255, 0.5f); // to make it semi-transparent
    }
    public virtual void OnDragEnd()
    {
        _renderer.color = new Color(255, 255, 255, 1f);
        _dragging = false;
        var position = transform.position;
        position.z = -1;
        transform.position = position;
        if (!_isDanger)
        {
            return;
        }
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!_dragging)
        {
            return;
        }
        _renderer.color = new Color(255, 0, 0, 0.5f); // to make it red if not suitable
        _isDanger = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _isDanger = false;
    }
}