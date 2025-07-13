using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

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
    playerController playerController;
    public TMP_Text cocoCoinText;

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

    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<playerController>();
    }

    public void ShowNews()
    {
        newsPanel.SetActive(!newsPanel.activeSelf);
    }
    public void ClosePanel(GameObject p)
    {
        p.SetActive(false);
        playerController.EnableControl(true);
    }

    public void AddCoco(float c)
    {
        cocoCoin += c;
        cocoCoinText.text = " X " + cocoCoin.ToString();
    }

    public void UseCoco(float c)
    {
        cocoCoin -= c;
        cocoCoinText.text = " X " + cocoCoin.ToString();
    }
}
