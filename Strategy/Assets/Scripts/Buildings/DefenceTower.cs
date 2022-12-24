using UnityEngine;
public class DefenceTower : Building
{
    [SerializeField] private Production Archer;
    [SerializeField] private string _productionTitle;
    [SerializeField] private string _productionSubTitle;
    [SerializeField] protected Sprite _productionSprite = null;
    
    /// <summary>
    /// Updates production data.
    /// </summary>
    public void SetProductionData()
    {
        var position = transform.position;
        position.x += 2;
        Archer.birthVector = position;
        Archer.productionSprite = _productionSprite;
        Archer.title = _productionTitle;
        Archer.subTitle = _productionSubTitle;
    }
    public override void OnDragEnd()
    {
        base.OnDragEnd();
        SetProductionData();
        OnMouseUp();
    }
    public override void OnMouseUp()
    {
        InformationController.Instance.UpdateInformationView(title, subTitle, BuilgingSprite);
        InformationController.Instance.UpdateProduction(isHaveProduction, _productionTitle, _productionSubTitle,
            _productionSprite, Archer);
        SetProductionData();
    }
}