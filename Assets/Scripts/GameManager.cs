using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject inventoryPanel;
    public PanelManager panelManager;
    public GameObject gatheringPanel;
    public GatheringManager gatheringManager;
    public GameObject potionCraft;
    public PotionCraftManager potionCraftManager;


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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            panelManager.SetPotion();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gatheringPanel.SetActive(!gatheringPanel.activeSelf);
            gatheringManager.SetPartyIdle();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            potionCraft.SetActive(!potionCraft.activeSelf);
            potionCraftManager.SetMaterialCraft();
        }
    }
}
