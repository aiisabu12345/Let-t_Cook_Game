using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class SellManager : NormalFunctionForPanel
{
    public static SellManager Instance;
    public List<PotionData> potionSelect = new List<PotionData>();
    public Transform selectPotionTransform;
    public Transform panelPotionTransform;
    public Button sellButton;
    public GameObject itemButtonPotion;
    public TMP_Text priceText;
    public TMP_Text cocoCoinText;
    public float sumPrice = 0;
    public List<BuffData> buff = new List<BuffData>();
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

    public override void OnOpen()
    {
        SetPotionSelect();
        SetPotionSell();
        cocoCoinText.text = "Coco : " + GameManager.Instance.cocoCoin.ToString();
    }

    public void SetPotionSell()
    {
        for (int i = panelPotionTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(panelPotionTransform.GetChild(i).gameObject);
        }

        foreach (PotionData p in InventoryManager.Instance.potionList)
        {
            GameObject item = Instantiate(itemButtonPotion, panelPotionTransform);
            ItemButtonPotion button = item.GetComponent<ItemButtonPotion>();

            button.potion = p;
            button.icon.sprite = p.icon;
            button.bg.sprite = InventoryManager.Instance.bg[p.tier];


            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                button.AddInSelect();
                SetPotionSelect();
            });
        }
    }

    public void SetPotionSelect()
    {
        sumPrice = 0;

        for (int i = selectPotionTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(selectPotionTransform.GetChild(i).gameObject);
        }

        foreach (PotionData p in potionSelect)
        {
            GameObject item = Instantiate(itemButtonPotion, selectPotionTransform);
            ItemButtonPotion button = item.GetComponent<ItemButtonPotion>();

            button.potion = p;
            button.icon.sprite = p.icon;
            button.bg.sprite = InventoryManager.Instance.bg[p.tier];

            float temPrice = p.price;
            if (buff.Count > 0)
            {
                foreach (BuffData b in buff)
                {
                    float sumBuff = 0;
                    for (int i = 0; i < p.status.Count; i++)
                    {
                        if (p.status[i] == b.status)
                        {
                            sumBuff += b.buffPercent;
                        }
                    }

                    temPrice = temPrice * (1 + sumBuff / 100);
                }
            }

            sumPrice += temPrice;


            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                button.RemoveInSelect();
                SetPotionSell();
            });
        }

        priceText.text = "Price : " + sumPrice.ToString();
    }

    public void SellPotion()
    {
        GameManager.Instance.cocoCoin += sumPrice;
        cocoCoinText.text = "Coco : " + GameManager.Instance.cocoCoin.ToString();
        potionSelect.Clear();
        SetPotionSell();
        SetPotionSelect();
    }
}
