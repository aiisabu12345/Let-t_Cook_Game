using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : NormalFunctionForPanel
{
    public static InventoryManager Instance;

    [SerializeField]
    public List<MaterialData> materialList = new List<MaterialData>();
    public List<MaterialData> materialSheet = new List<MaterialData>();
    public List<PotionData> potionSheet = new List<PotionData>();
    public List<PotionData> potionList = new List<PotionData>();
    public List<Sprite> bg = new List<Sprite>();
    public GameObject panel;
    public PanelManager panelManager;
    public Transform spawnPoint;

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
        panel.SetActive(true);
        panelManager.SetPotion();

    }

    public void AddPotion(List<string> status, int tier)
    {
        PotionData newPotion = ScriptableObject.CreateInstance<PotionData>();
        PotionData originPotion = ScriptableObject.CreateInstance<PotionData>();
        originPotion = potionSheet.Find(p => p.status[0] == status[0]);
        newPotion.model = originPotion.model;
        newPotion.potionName = originPotion.potionName;
        newPotion.icon = originPotion.icon;
        newPotion.status = status;
        newPotion.tier = tier;

        if (tier == 0)
        {
            newPotion.price = Random.Range(50, 60);
        }
        else if (tier == 1)
        {
            newPotion.price = Random.Range(75, 90);
        }
        else
        {
            newPotion.price = Random.Range(100, 120);
        }

        newPotion.price = Mathf.Pow(newPotion.price, newPotion.status.Count);
        //newPotion.price = newPotion.price^newPotion.status.Count;

        potionList.Add(newPotion);
        Debug.Log("add " + newPotion.potionName + " price: " + newPotion.price);
    }

    public void AddMaterial(int m, int tier)
    {
        MaterialData newMaterial = ScriptableObject.CreateInstance<MaterialData>();
        newMaterial.icon = materialSheet[m].icon;
        newMaterial.materialName = materialSheet[m].materialName;
        newMaterial.tier = tier;
        newMaterial.status = materialSheet[m].status;
        materialList.Add(newMaterial);
    }

    public void CreatePotion(List<string> status, int tier, Transform spawnPoint)
    {
        PotionData originPotion = ScriptableObject.CreateInstance<PotionData>();
        originPotion = potionSheet.Find(p => p.status[0] == status[0]);

        GameObject newPotionObject = Instantiate(originPotion.model, spawnPoint.position, spawnPoint.rotation);
        Potion newPotion = newPotionObject.GetComponent<Potion>();

        newPotion.potion.model = originPotion.model;
        newPotion.potion.potionName = originPotion.potionName;
        newPotion.potion.icon = originPotion.icon;
        newPotion.potion.status = status;
        newPotion.potion.tier = tier;

        if (tier == 0)
        {
            newPotion.potion.price = Random.Range(10, 20);
        }
        else if (tier == 1)
        {
            newPotion.potion.price = Random.Range(30, 40);
        }
        else
        {
            newPotion.potion.price = Random.Range(50, 60);
        }

        newPotion.potion.price = Mathf.Pow(newPotion.potion.price, newPotion.potion.status.Count);

    }
}
