using UnityEngine;
using UnityEngine.UI;

public class ItemButtonPotion : MonoBehaviour
{
    public PotionData potion;
    public Image icon;
    public Image bg;

    public void AddInSelect()
    {
        SellManager.Instance.potionSelect.Add(potion);
        InventoryManager.Instance.potionList.Remove(potion);
        Destroy(gameObject);
    }

    public void RemoveInSelect()
    {
        SellManager.Instance.potionSelect.Remove(potion);
        InventoryManager.Instance.potionList.Add(potion);
        SellManager.Instance.SetPotionSelect();
        Destroy(gameObject);
    }
}
