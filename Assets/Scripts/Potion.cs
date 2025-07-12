using UnityEngine;

public class Potion : MonoBehaviour
{
    public PotionData potion;

    void Awake()
    {
        potion = ScriptableObject.CreateInstance<PotionData>();
    }
}
