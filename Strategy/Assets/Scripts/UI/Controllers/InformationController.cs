using System;
using UnityEngine;

public class InformationController : MonoBehaviour
{
    public static InformationController Instance=null;
    [SerializeField] private InformationView informationView;
    [SerializeField] private ProductionView productionView;
    private void OnEnable()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }
    /// <summary>
    /// Updates Information Panel Data.
    /// </summary>
    public void UpdateInformationView(string title,string subtitle,Sprite sprite)
    {
        informationView.UpdateInformationView(title,subtitle,sprite);
    }
    /// <summary>
    /// Updates Production Panel Data.
    /// </summary>
    public void UpdateProduction(bool IsHaveProduction,string title = null, string subtitle=null, Sprite sprite=null, Production production=null)
    {
        productionView.UpdateProduction(IsHaveProduction,title,subtitle,sprite,production);
    }
}
