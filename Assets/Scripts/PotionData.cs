using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "Create/PotionData")]
public class PotionData : ScriptableObject
{
    public GameObject model;
    public Image icon;
    public string potionName;
    public string[] status;
}
