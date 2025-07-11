using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionPanel : MonoBehaviour
{
    public Image showItem;
    public Image bg;
    public TMP_Text descriptionText;

    public void showPotion(Image bgP, Image iconP, string text)
    {
        bg.sprite = bgP.sprite;
        showItem.sprite = iconP.sprite;
        descriptionText.text = text;
    }

    public void showMaterial(Image bgM, Image iconM, string text)
    {
        bg.sprite = bgM.sprite;
        showItem.sprite = iconM.sprite;
        descriptionText.text = text;
    }
}
