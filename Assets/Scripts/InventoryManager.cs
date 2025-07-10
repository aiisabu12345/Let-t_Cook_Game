using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField]
    public List<MaterialData> materialList = new List<MaterialData>();
    public List<MaterialData> materialSheet = new List<MaterialData>();
    public List<PotionData> potionSheet = new List<PotionData>();
    public List<PotionData> potionList = new List<PotionData>();
    public List<Sprite> bg = new List<Sprite>();
    

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
        potionList.Add(newPotion);
        Debug.Log("add " + newPotion.potionName);
        Debug.Log("work");
    }

    public void AddMaterial(int m,int tier)
    {
        MaterialData newMaterial = ScriptableObject.CreateInstance<MaterialData>();
        newMaterial.icon = materialSheet[m].icon;
        newMaterial.materialName = materialSheet[m].materialName;
        newMaterial.tier = tier;
        newMaterial.status = materialSheet[m].status;
        materialList.Add(newMaterial);
        Debug.Log("add " + newMaterial.materialName);
    }
}
