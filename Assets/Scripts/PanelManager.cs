using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{
    public GameObject itemButtonPotion;
    public GameObject itemButtonMaterial;
    public Transform PanelTransform;
    public DescriptionPanel descriptionPanel;

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

            string tierText = "";
            if (p.tier == 0)
            {
                tierText = "B";
            }
            else if (p.tier == 1)
            {
                tierText = "A";
            }
            else
            {
                tierText = "S";
            }


            Dictionary<string, int> allStatus = CountStatus(p.status);

            string statusText = "\n\t";

            foreach (KeyValuePair<string, int> entry in allStatus)
            {
                statusText += entry.Key + " Lv." + entry.Value.ToString() + "\n\t";
            }

            string text = p.potionName + "\n\nTier : " + tierText + "\nStatus : " + statusText + "\nPrice : " + p.price + " Coco";
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                descriptionPanel.showPotion(button.bg, button.icon, text);
            });
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

            string tierText = "";
            if (m.tier == 0)
            {
                tierText = "B";
            }
            else if (m.tier == 1)
            {
                tierText = "A";
            }
            else
            {
                tierText = "S";
            }

            string text = m.materialName + "\n\nTier : " + tierText + "\nStatus : " + m.status;
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                descriptionPanel.showPotion(button.bg, button.icon, text);
            });
        }
    }

    public Dictionary<string, int> CountStatus(List<string> statusList)
    {
        Dictionary<string, int> result = new Dictionary<string, int>();

        foreach (string status in statusList)
        {
            if (result.ContainsKey(status))
            {
                result[status]++;
            }
            else
            {
                result[status] = 1;
            }
        }

        return result;
    }
}
