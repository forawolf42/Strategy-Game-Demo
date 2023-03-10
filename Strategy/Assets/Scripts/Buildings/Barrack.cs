using UnityEngine;
public class Barrack : Building
{
    [SerializeField] private Production Soldier;
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
        Soldier.birthVector = position;
        Soldier.productionSprite = _productionSprite;
        Soldier.title = _productionTitle;
        Soldier.subTitle = _productionSubTitle;
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
            _productionSprite, Soldier);
        SetProductionData();
    }
}