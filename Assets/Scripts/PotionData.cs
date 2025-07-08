using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Create/PotionData")]
public class PotionData : ScriptableObject
{
    public GameObject model;
    public Sprite icon;
    public string potionName;
    public string[] status;
    public int tier;
}
