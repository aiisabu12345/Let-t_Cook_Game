using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Create/MaterialData")]
public class MaterialData : ScriptableObject
{
    public Sprite icon;
    public string materialName;
    public string status;
    public int tier;
}
