using AnimatedBattleText.Examples;
using PixelBattleText;
using UnityEngine;

public class TriggerUI : MonoBehaviour
{
    [SerializeField] private ExampleTextManager textManager;
    [SerializeField] private string triggerMessage;
    [SerializeField] public TextAnimation textToUse;
    public GameObject UIPanel;

    playerController playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = GameObject.FindWithTag("Player").GetComponent<playerController>();
            playerMovement.EnableControl(false);
            if (textManager != null && textToUse != null)
            {
                textManager.LastUsed = textToUse;
                textManager.ShowInputText(triggerMessage);
            }
            else
            {
                Debug.LogWarning("TextManager หรือ TextToUse ยังไม่ถูกเซ็ตใน Inspector");
            }

            UIPanel.SetActive(true);

            ArrowInputUI arrowUI = FindObjectOfType<ArrowInputUI>();
            CardsController cardsUI = FindObjectOfType<CardsController>();
            if (arrowUI != null)
            {
                
                arrowUI.RestartSequenceFromTrigger(); 
            }
            if(cardsUI != null)
            {
                cardsUI.RestartSequenceFromTrigger();
            }
            
        }
    }
    }
