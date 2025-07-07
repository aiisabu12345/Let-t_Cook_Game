using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public Image potionButton;
    public Transform rightPanelTransform;

    public void OnOpen()
    {
        for (int i = rightPanelTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(rightPanelTransform.GetChild(i).gameObject);
        }

        foreach (PotionData p in InventoryManager.Instance.potionList)
        {
            Image item = Instantiate(potionButton, rightPanelTransform);
            itemButton button = item.GetComponent<itemButton>();

            item = p.icon;
            button.potion = p;
        }
    }
}
