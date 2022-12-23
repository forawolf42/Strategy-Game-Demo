using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _subTitle;
    [SerializeField] private Image _renderer;

    public void UpdateInformationView(string title,string subtitle,Sprite sprite)
    {
        _title.SetText(title);
        _subTitle.SetText(subtitle);
        _renderer.sprite = sprite;
    }

}
