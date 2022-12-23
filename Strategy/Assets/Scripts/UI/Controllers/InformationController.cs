using System;
using UnityEngine;

public class InformationController : MonoBehaviour
{
    public static InformationController instance=null;
    [SerializeField] private InformationView _informationView;
    [SerializeField] private ProductionView _productionView;
    private void OnEnable()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void UpdateInformationView(string title,string subtitle,Sprite sprite)
    {
        _informationView.UpdateInformationView(title,subtitle,sprite);
    }
    public void UpdateProduction(bool IsHaveProduction,string title = null, string subtitle=null, Sprite sprite=null, Production production=null)
    {
        _productionView.UpdateProduction(IsHaveProduction,title,subtitle,sprite,production);
    }
}
