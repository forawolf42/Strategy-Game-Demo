using UnityEngine;
public class TileManager : MonoBehaviour
{
    [SerializeField] private Color _basecolor;
    [SerializeField] private Color _offsetcolor;
    [SerializeField] private SpriteRenderer _renderer;
    public void Init(bool isOffest)
    {
        _renderer.color = isOffest ? _offsetcolor : _basecolor;
    }
}
