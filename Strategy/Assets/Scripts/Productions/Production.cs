using UnityEngine;
public  class Production : MonoBehaviour
{
    [HideInInspector] public Sprite productionSprite = null;
    [HideInInspector] public Vector3 birthVector;
    [HideInInspector] public string title;
    [HideInInspector] public string subTitle;
    protected bool IsSelected;
    public void OnMouseClickedRight(Transform vector)
    {
        if (!IsSelected)
        {
            return;
        }
        Pathfinding.Instance.production = transform;
        Pathfinding.Instance.target = vector;
        Pathfinding.Instance.FollowThePath();
    }
    private void OnEnable()
    {
        ProductionManager.OnMouseClickedLeft += OnMouseClickedLeft;
        ProductionManager.OnMouseClickedRight += OnMouseClickedRight;
    }
    private void OnDisable()
    {
        ProductionManager.OnMouseClickedLeft -= OnMouseClickedLeft;
        ProductionManager.OnMouseClickedRight -= OnMouseClickedRight;
    }
    private void Awake()
    {
        transform.position = birthVector; // set birth vector
    }
    private void OnMouseClickedLeft()
    {
        IsSelected = false;
    }
    private void OnMouseUp()
    {
        IsSelected = true;
        InformationController.Instance.UpdateInformationView(title, subTitle, productionSprite);
        InformationController.Instance.UpdateProduction(false);
    }
}