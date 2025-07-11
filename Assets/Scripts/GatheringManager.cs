using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GatheringManager : MonoBehaviour
{
    public static GatheringManager Instance;
    public int partyAmout = 5;
    public Transform panelTransform;
    public List<PartyData> partySheet = new List<PartyData>();
    public List<PartyData> partyIdle = new List<PartyData>();
    public List<PartyData> partyHire = new List<PartyData>();
    public float timeMax;
    public float timeCurrent;

    public TMP_Text timeText;
    public TMP_Text goldText;

    public bool onIdlePanel = false;

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
        timeCurrent = timeMax;

        UpdateParty();
        SetPartyIdle();
    }

    void Update()
    {
        timeText.text = "Time : " + Mathf.FloorToInt(timeCurrent).ToString();

        timeCurrent -= Time.deltaTime;

        if (timeCurrent <= 0)
        {
            timeCurrent = timeMax;
            UpdateParty();
            
            if (onIdlePanel)
                SetPartyIdle();
        }

        for (int i = partyHire.Count - 1; i >= 0; i--)
        {
            partyHire[i].timeReward -= Time.deltaTime;
            if (!onIdlePanel)
                partyHire[i].timerText.text = "Time Count : " + (Mathf.Round(partyHire[i].timeReward * 10.0f) * 0.1f).ToString();

            if (partyHire[i].timeReward <= 0)
            {
                int n = Random.Range(partyHire[i].min, partyHire[i].max + 1);
                for (int j = n; j >= 0; j--)
                {
                    int m = Random.Range(0, 4);
                    float randTier = Random.Range(1f, 101f);
                    int tier;
                    if (randTier <= partyHire[i].rate1)
                    {
                        tier = partyHire[i].minTier;
                    }
                    else
                    {
                        tier = partyHire[i].maxTier;
                    }
                    InventoryManager.Instance.AddMaterial(m, tier);
                }
                partyHire.RemoveAt(i);

                if (!onIdlePanel)
                    SetPartyHire();
                else if (GameManager.Instance.potionCraft.activeSelf)
                    PotionCraftManager.Instance.SetMaterialCraft();

            }
        }
    }

    public void UpdateParty()
    {
        partyIdle.Clear();

        for (int i = 0; i < partyAmout; i++)
        {
            int rand = Random.Range(0, 3);
            Debug.Log(rand);
            PartyData newParty = ScriptableObject.CreateInstance<PartyData>();
            newParty.partyUI = partySheet[rand].partyUI;
            newParty.minTier = partySheet[rand].minTier;
            newParty.maxTier = partySheet[rand].maxTier;
            newParty.min = partySheet[rand].min;
            newParty.max = partySheet[rand].max;
            newParty.rate1 = partySheet[rand].rate1;
            newParty.rate2 = partySheet[rand].rate2;
            newParty.timeReward = partySheet[rand].timeReward;
            newParty.partyHireUI = partySheet[rand].partyHireUI;

            partyIdle.Add(newParty);
        }
    }

    public void SetPartyIdle()
    {
        onIdlePanel = true;

        ClearPanel();

        foreach (PartyData p in partyIdle)
        {
            GameObject newPartyUI = Instantiate(p.partyUI, panelTransform);
            PartyButton pButton = newPartyUI.GetComponent<PartyButton>();
            pButton.party = p;
        }
    }

    public void SetPartyHire()
    {
        onIdlePanel = false;

        ClearPanel();

        foreach (PartyData p in partyHire)
        {
            GameObject newPartyUI = Instantiate(p.partyHireUI, panelTransform);
            PartyHireButton partyHireButton = newPartyUI.GetComponent<PartyHireButton>();
            p.timerText = partyHireButton.timerText;
        }
    }

    public void ClearPanel()
    {
        for (int i = panelTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(panelTransform.GetChild(i).gameObject);
        }
    }
}
