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
    public GameObject potMinigame;
    public GameObject aimgame;
    public GameObject cardMinigame;
    public GameObject arrowMinigame;
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
        StartCoroutine(CraftPotionCoroutine());
    }

    private IEnumerator CraftPotionCoroutine()
    {
        int Rand = Random.Range(0, 4);

        //optimize this shit later T-T
        //Way to optimize
        //Add new class for minigame
        switch (Rand)
        {
            case 0:
                PotMinigame miniGame1 = potMinigame.GetComponent<PotMinigame>();
                miniGame1.gameObject.SetActive(true);
                miniGame1.resetCountdown();

                while (!miniGame1.isMinigameDone)
                {
                    yield return null;
                }

                if (miniGame1.isWin)
                {
                    List<string> status = new List<string>();
                    for (int i = 0; i < materialSelect.Count; i++)
                    {
                        status.Add(materialSelect[i].status);
                    }

                    int rand = Random.Range(0, materialSelect.Count);
                    int tier = materialSelect[rand].tier;
                    InventoryManager.Instance.AddPotion(status, tier);
                }

                materialSelect.Clear();
                SetMaterialCraft();
                SetMaterialSelect();

                // Reset flag สำหรับเล่นใหม่ครั้งหน้า
                miniGame1.isMinigameDone = false;
                break;
            case 1:
                CardsController miniGame2 = cardMinigame.GetComponent<CardsController>();
                miniGame2.gameObject.SetActive(true);
                miniGame2.resetCountdown();

                while (!miniGame2.isMinigameDone)
                {
                    yield return null;
                }

                if (miniGame2.isWin)
                {
                    List<string> status = new List<string>();
                    for (int i = 0; i < materialSelect.Count; i++)
                    {
                        status.Add(materialSelect[i].status);
                    }

                    int rand = Random.Range(0, materialSelect.Count);
                    int tier = materialSelect[rand].tier;
                    InventoryManager.Instance.AddPotion(status, tier);
                }

                materialSelect.Clear();
                SetMaterialCraft();
                SetMaterialSelect();

                // Reset flag สำหรับเล่นใหม่ครั้งหน้า
                miniGame2.isMinigameDone = false;
                break;
            case 2:
                ArrowInputUI miniGame3 = arrowMinigame.GetComponent<ArrowInputUI>();
                miniGame3.RestartSequenceFromTrigger();
                miniGame3.gameObject.SetActive(true);
                miniGame3.resetCountdown();

                while (!miniGame3.isMinigameDone)
                {
                    yield return null;
                }

                if (miniGame3.isWin)
                {
                    List<string> status = new List<string>();
                    for (int i = 0; i < materialSelect.Count; i++)
                    {
                        status.Add(materialSelect[i].status);
                    }

                    int rand = Random.Range(0, materialSelect.Count);
                    int tier = materialSelect[rand].tier;
                    InventoryManager.Instance.AddPotion(status, tier);
                }

                materialSelect.Clear();
                SetMaterialCraft();
                SetMaterialSelect();

                // Reset flag สำหรับเล่นใหม่ครั้งหน้า
                miniGame3.isMinigameDone = false;
                break;
            case 3:
                AimTrainerManager miniGame4 = aimgame.GetComponent<AimTrainerManager>();
 
          

                miniGame4.gameObject.SetActive(true); // ให้มัน awake และ Update ทำงานก่อน
                yield return null; // รอ 1 frame เพื่อให้ Awake/Start/Update ทำงานทัน

                miniGame4.resetCountdown();
                // รีเซ็ตสถานะก่อน
                miniGame4.isMinigameDone = false;
                miniGame4.isWin = false;

                while (!miniGame4.isMinigameDone)
                {
                    yield return null;
                }

                if (miniGame4.isWin)
                {
                    List<string> status = new List<string>();
                    for (int i = 0; i < materialSelect.Count; i++)
                    {
                        status.Add(materialSelect[i].status);
                    }

                    int rand = Random.Range(0, materialSelect.Count);
                    int tier = materialSelect[rand].tier;
                    InventoryManager.Instance.AddPotion(status, tier);
                }

                materialSelect.Clear();
                SetMaterialCraft();
                SetMaterialSelect();

                // Reset flag สำหรับเล่นใหม่ครั้งหน้า
                miniGame4.isMinigameDone = false;
                break;

        }
    }
}