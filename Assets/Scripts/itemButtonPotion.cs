using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButtonPotion : MonoBehaviour, IPointerClickHandler
{
    public PotionData potion;
    public Image icon;
    public Image bg;
    
    public System.Action onRightClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject newPotion = Instantiate(potion.model, InventoryManager.Instance.spawnPoint.position, InventoryManager.Instance.spawnPoint.rotation);

            Potion newPotionData = newPotion.GetComponent<Potion>();
            newPotionData.potion = potion;
            
            InventoryManager.Instance.potionList.Remove(potion);
            InventoryManager.Instance.OnOpen();
            Destroy(gameObject);
        }
    }

    /*public void AddInSelect()
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
    }*/
}
