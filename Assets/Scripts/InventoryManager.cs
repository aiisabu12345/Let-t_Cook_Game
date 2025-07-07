using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField]
    private List<MaterialData> materialList;
    public List<PotionData> potionSheet;
    public List<PotionData> potionList;

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

    public void AddPotion()
    {
        potionList.Add(potionSheet[0]);
        Debug.Log("add " + potionSheet[0]);
    }
}
