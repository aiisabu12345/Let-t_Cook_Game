using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Data", menuName = "Create/PartyData")]
public class PartyData : ScriptableObject
{
    public GameObject partyUI;
    public GameObject partyHireUI;
    public int minTier;
    public int maxTier;
    public int min;
    public int max;
    public float rate1;
    public float rate2;
    public float timeReward;
    public TMP_Text timerText;
    public float cost;
}