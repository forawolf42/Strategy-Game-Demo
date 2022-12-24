using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InformationView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text subTitle;
    [SerializeField] private Image _renderer;
    public void UpdateInformationView(string _title,string _subtitle,Sprite sprite)
    {
        title.SetText(_title);
        subTitle.SetText(_subtitle);
        _renderer.sprite = sprite;
    }
}
