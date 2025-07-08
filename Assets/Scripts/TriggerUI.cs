using AnimatedBattleText.Examples;
using PixelBattleText;
using UnityEngine;

public class TriggerUI : MonoBehaviour
{
    [SerializeField] private ExampleTextManager textManager;
    [SerializeField] private string triggerMessage;
    [SerializeField] public TextAnimation textToUse;
    public GameObject UIPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textManager.LastUsed = textToUse; 
            textManager.ShowInputText(triggerMessage); 
            UIPanel.SetActive(true);
        }
    }
    }
