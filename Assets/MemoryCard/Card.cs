using UnityEngine;
using UnityEngine.UI;
using PrimeTween;
public class Card : MonoBehaviour
{
    [SerializeField] private Image Imageicon;

    public Sprite hiddenIconSprite;
    public Sprite iconSprite;

    public bool isSelected;

    public CardsController controller;

    public void OnCardClick()
    {
        controller.SetSelected(this);
    }

  public void SetIconSprite(Sprite Is)
    {
        iconSprite = Is;
    }

    public void Show()
    {
        Tween.Rotation(transform,
            new Vector3(0f, 180f, 0f),
            0.2f);
        Tween.Delay(0.1f , ()=> Imageicon.sprite = iconSprite);
        isSelected = true;
    }

    public void Hide()
    {
        Tween.Rotation(transform,
           new Vector3(0f, 0f, 0f),
           0.2f);

        Tween.Delay(0.1f, () =>
        {
            Imageicon.sprite = hiddenIconSprite;
            isSelected = false;
        });
       
    }

        
}
