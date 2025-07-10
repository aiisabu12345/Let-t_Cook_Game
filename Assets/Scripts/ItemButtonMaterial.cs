using UnityEngine;
using UnityEngine.UI;

public class ItemButtonMaterial : MonoBehaviour
{
    public MaterialData material;
    public Image icon;
    public Image bg;

    public void AddInSelect()
    {
        if (PotionCraftManager.Instance.materialSelect.Count < PotionCraftManager.Instance.maxMaterial)
        {
            PotionCraftManager.Instance.materialSelect.Add(material);
            InventoryManager.Instance.materialList.Remove(material);
            Destroy(gameObject);
        }
    }

    public void RemoveInSelect()
    {
        PotionCraftManager.Instance.materialSelect.Remove(material);
        InventoryManager.Instance.materialList.Add(material);
        Destroy(gameObject);
    }
}
