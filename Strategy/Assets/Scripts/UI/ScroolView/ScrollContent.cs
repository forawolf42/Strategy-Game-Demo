using UnityEngine;
public class ScrollContent : MonoBehaviour
{
    public float ItemSpacing => itemSpacing;
    public bool Horizontal => horizontal;
    public bool Vertical => vertical;
    public float Width => _width;
    public float Height => _height;
    public float ChildWidth => _childWidth;

    private RectTransform _rectTransform;

    /// <summary>
    /// The RectTransform components of all the children of this GameObject.
    /// </summary>
    private RectTransform[] _rtChildren;
    private float _width, _height;
    private float _childWidth, _childHeight;
    [SerializeField] private float itemSpacing;
    [SerializeField] private float horizontalMargin, verticalMargin;
    [SerializeField] private bool horizontal, vertical;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rtChildren = new RectTransform[_rectTransform.childCount];

        for (int i = 0; i < _rectTransform.childCount; i++)
        {
            _rtChildren[i] = _rectTransform.GetChild(i) as RectTransform;
        }

        // Subtract the margin from both sides.
        _width = _rectTransform.rect.width - (2 * horizontalMargin);

        // Subtract the margin from the top and bottom.
        _height = _rectTransform.rect.height - (2 * verticalMargin);

        _childWidth = _rtChildren[0].rect.width;
        _childHeight = _rtChildren[0].rect.height;
        horizontal = !vertical;
    }
}