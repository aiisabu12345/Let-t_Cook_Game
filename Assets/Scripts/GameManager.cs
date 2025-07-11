using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject inventoryPanel;
    public PanelManager panelManager;
    public GameObject gatheringPanel;
    public GatheringManager gatheringManager;
    public GameObject potionCraft;
    public PotionCraftManager potionCraftManager;
    public GameObject sellPanel;
    public SellManager sellManager;
    public float cocoCoin = 0;
    public GameObject newsPanel;


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

        if (Input.GetKeyDown(KeyCode.S))
        {
            sellPanel.SetActive(!sellPanel.activeSelf);
            sellManager.OnOpen();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            ShowNews();
        }
    }

    public void ShowNews()
    {
        newsPanel.SetActive(!newsPanel.activeSelf);
    }
}
