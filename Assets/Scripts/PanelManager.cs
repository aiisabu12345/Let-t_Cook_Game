using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject itemButtonPotion;
    public GameObject itemButtonMaterial;
    public Transform PanelTransform;

    public void SetPotion()
    {
        for (int i = PanelTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(PanelTransform.GetChild(i).gameObject);
        }

        foreach (PotionData p in InventoryManager.Instance.potionList)
        {
            GameObject item = Instantiate(itemButtonPotion, PanelTransform);
            ItemButtonPotion button = item.GetComponent<ItemButtonPotion>();

            button.potion = p;
            button.icon.sprite = p.icon;
            button.bg.sprite = InventoryManager.Instance.bg[p.tier];
        }
    }
    
    public void SetMaterial()
    {
        for (int i = PanelTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(PanelTransform.GetChild(i).gameObject);
        }

        foreach (MaterialData m in InventoryManager.Instance.materialList)
        {
            GameObject item = Instantiate(itemButtonMaterial, PanelTransform);
            ItemButtonMaterial button = item.GetComponent<ItemButtonMaterial>();

            button.material = m;
            button.icon.sprite = m.icon;
            button.bg.sprite = InventoryManager.Instance.bg[m.tier];
            Debug.Log("tier=================" + m.tier);
        }
    }
}
