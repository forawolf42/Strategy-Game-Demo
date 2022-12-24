using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductionView : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Image _renderer;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text subTitle;
    private Production _selectedProduction = null;
    public void UpdateProduction(bool isHaveProduction,string _title,string _subtitle,Sprite sprite,Production production)
    {
        if (!isHaveProduction)
        {
            // close panel if no production
            panel.SetActive(false);
            return;
        }
        panel.SetActive(true);
        title.SetText(_title);
        subTitle.SetText(_subtitle);
        _renderer.sprite = sprite;
        _selectedProduction = production;
    }
    public void SpawnProduction()
    {
        if (_selectedProduction==null)
        {
            return;
        }
        // spawn production
        Instantiate(_selectedProduction, _selectedProduction.birthVector, Quaternion.identity);
    }
}
