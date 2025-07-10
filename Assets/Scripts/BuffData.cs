using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "Create/BuffData")]
public class BuffData : ScriptableObject
{
    public string status;
    public float buffPercent = 0;
}
