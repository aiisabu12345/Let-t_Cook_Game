using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "Create/PotionData")]
public class PotionData : ScriptableObject
{
    public GameObject model;
    public Sprite icon;
    public string potionName;
    public List<string> status;
    public int tier;
}
