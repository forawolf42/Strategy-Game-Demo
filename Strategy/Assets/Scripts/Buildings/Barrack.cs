using UnityEngine;

public class Barrack : Building
{
    [SerializeField] private Production Soldier;
    [SerializeField] private string _productionTitle;
    [SerializeField] private string _productionSubTitle;
    [SerializeField] protected Sprite _productionSprite = null;

    public void SetProduction()
    {
        var position = transform.position;
        position.x += 2;
        Soldier.birthVector = position;
        Soldier._productionSprite = _productionSprite;
        Soldier.title = _productionTitle;
        Soldier.subTitle = _productionSubTitle;
    }

    public override void OnDragEnd()
    {
        base.OnDragEnd();

        SetProduction();
        OnMouseUp();
    }
    
    public override void OnMouseUp()
    {
        InformationController.instance.UpdateInformationView(title, subTitle, _builgingSprite);
        InformationController.instance.UpdateProduction(IsHaveProduction,_productionTitle,_productionSubTitle,_productionSprite,Soldier);
        SetProduction();
    }
    
}
