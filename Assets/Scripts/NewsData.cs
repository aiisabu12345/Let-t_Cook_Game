using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "Create/NewsData")]
public class NewsData : ScriptableObject
{
    public BuffData buff;
    public string title;
    public string head;
}
