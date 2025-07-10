using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PotionCraftManager : MonoBehaviour
{
    public static PotionCraftManager Instance;
    public int maxMaterial;
    public List<MaterialData> materialSelect = new List<MaterialData>();
    public Transform selectMaterialTransform;
    public Transform panelMaterialTransform;
    public Button craftButton;
    public GameObject itemButtonMaterial;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMaterialCraft()
    {
        for (int i = panelMaterialTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(panelMaterialTransform.GetChild(i).gameObject);
        }

        foreach (MaterialData m in InventoryManager.Instance.materialList)
        {
            GameObject item = Instantiate(itemButtonMaterial, panelMaterialTransform);
            ItemButtonMaterial button = item.GetComponent<ItemButtonMaterial>();

            button.material = m;
            button.icon.sprite = m.icon;
            button.bg.sprite = InventoryManager.Instance.bg[m.tier];


            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                button.AddInSelect();
                SetMaterialSelect();
            });
        }
    }

    public void SetMaterialSelect()
    {
        for (int i = selectMaterialTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(selectMaterialTransform.GetChild(i).gameObject);
        }

        foreach (MaterialData m in materialSelect)
        {
            GameObject item = Instantiate(itemButtonMaterial, selectMaterialTransform);
            ItemButtonMaterial button = item.GetComponent<ItemButtonMaterial>();

            button.material = m;
            button.icon.sprite = m.icon;
            button.bg.sprite = InventoryManager.Instance.bg[m.tier];


            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                button.RemoveInSelect();
                SetMaterialCraft();
            });
        }
    }

    public void CraftPotion()
    {
        List<string> status = new List<string>();
        for (int i = 0; i < materialSelect.Count; i++)
        {
            status.Add(materialSelect[i].status);
        }

        int rand = Random.Range(0, materialSelect.Count);
        int tier = materialSelect[rand].tier;

        InventoryManager.Instance.AddPotion(status, tier);
        materialSelect.Clear();
        SetMaterialCraft();
        SetMaterialSelect();
    }
}
