using System.Collections;
using UnityEngine;
public  class Production : MonoBehaviour
{
    [HideInInspector] public Sprite productionSprite = null;
    [HideInInspector] public Vector3 birthVector;
    [HideInInspector] public string title;
    [HideInInspector] public string subTitle;
    protected bool IsSelected;
    private bool _isWalking;
    
    private Coroutine currentCoroutine;

    public void OnMouseClickedRight(Transform vector)
    {
        if (!IsSelected)
        {
            return;
        }
        Pathfinding.Instance.production = transform;
        Pathfinding.Instance.target = vector;


        _isWalking = false;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(FollowThePathEnum(vector));

    }
    
    
    
    IEnumerator FollowThePathEnum(Transform target)
    {
        _isWalking = true;
        var targetTransform = target;
        var productionPos = transform;
        
        foreach (var currentNote in Pathfinding.Instance.FindPath(productionPos.position,
                     new Vector3(targetTransform.position.x + 1, targetTransform.position.y,
                         targetTransform.position.z)))
        {
            var currentPos = currentNote.WorldPosition;
            currentPos.z = -1;
            currentPos.x -= -.5f; // to center the cell
            currentPos.y -= -.5f;

         
            while (productionPos.position != currentPos)
            {
                if (!_isWalking)
                {
                    yield break;
                }
                // smooth transform
                productionPos.position = Vector3.MoveTowards(productionPos.position, currentPos, 5 * Time.deltaTime);
                yield return null;
            }
        }
        _isWalking = false;

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