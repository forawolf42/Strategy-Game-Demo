using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductionView : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _renderer;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _subTitle;
    private Production _selectedProduction = null;
    
    public void UpdateProduction(bool IsHaveProduction,string title,string subtitle,Sprite sprite,Production production)
    {
        if (!IsHaveProduction)
        {
            _panel.SetActive(false);
            return;
        }
        _panel.SetActive(true);

        _title.SetText(title);
        _subTitle.SetText(subtitle);
        _renderer.sprite = sprite;
        _selectedProduction = production;
    }
    


    public void SpawnProduction()
    {
        if (_selectedProduction==null)
        {
            return;
        }

        Instantiate(_selectedProduction, _selectedProduction.birthVector, Quaternion.identity);
    }
}
