using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject potionButton;
    public Transform rightPanelTransform;

    public void OnOpen()
    {
        for (int i = rightPanelTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(rightPanelTransform.GetChild(i).gameObject);
        }

        foreach (PotionData p in InventoryManager.Instance.potionList)
        {
            GameObject item = Instantiate(potionButton, rightPanelTransform);
            itemButton button = item.GetComponent<itemButton>();

            button.potion = p;
            button.icon.sprite = p.icon;
            button.bg.sprite = InventoryManager.Instance.bg[p.tier];
        }
    }
}
