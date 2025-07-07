using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "Create/MaterialData")]
public class MaterialData : ScriptableObject
{
    public Image icon;
    public string[] materialName;
    public string status;
    public int tier;
}
