using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField]
    public List<MaterialData> materialList = new List<MaterialData>();
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

    public void AddPotion(PotionData originPotion)
    {
        PotionData newPotion = ScriptableObject.CreateInstance<PotionData>();
        newPotion.model = originPotion.model;
        newPotion.potionName = originPotion.potionName;
        newPotion.icon = originPotion.icon;
        newPotion.status = originPotion.status;
        newPotion.tier = originPotion.tier;
        potionList.Add(newPotion);
        Debug.Log("add " + newPotion.potionName);
    }

    public void AddMaterial(MaterialData originMaterial)
    {
        MaterialData newMaterial = ScriptableObject.CreateInstance<MaterialData>();
        newMaterial.icon = originMaterial.icon;
        newMaterial.materialName = originMaterial.materialName;
        newMaterial.tier = originMaterial.tier;
        newMaterial.status = originMaterial.status;
        materialList.Add(newMaterial);
        Debug.Log("add " + newMaterial.materialName);
    }
}
