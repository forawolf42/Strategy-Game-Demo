using System;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Color _basecolor;
    [SerializeField] private Color _offsetcolor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    public void Init(bool isOffest)
    {
        _renderer.color = isOffest ? _offsetcolor : _basecolor;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
}
