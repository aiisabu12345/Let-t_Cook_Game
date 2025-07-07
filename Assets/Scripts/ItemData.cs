using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Create/ItemData")]
public class ItemData : ScriptableObject
{
    public GameObject model;
    public string[] itemName;
    public string[] status;
}
