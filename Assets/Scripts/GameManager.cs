using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject inventoryPanel;
    public PanelManager panelManager;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            panelManager.SetPotion();
        }
    }
}
