using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCraftButton : MonoBehaviour
{
    public Button button;
    public float priceUpgrade = 500;
    public TMP_Text priceText;
    void Start()
    {
        priceText.text = "-" + priceUpgrade.ToString();
    }
    void Update()
    {
        if (GameManager.Instance.cocoCoin < priceUpgrade)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void Upgrade()
    {
        GameManager.Instance.cocoCoin -= priceUpgrade;
        PotionCraftManager.Instance.maxMaterial += 1;
        priceUpgrade = priceUpgrade * Mathf.Pow(100f, PotionCraftManager.Instance.maxMaterial);
        priceText.text = "-" + priceUpgrade.ToString();
    }
}
