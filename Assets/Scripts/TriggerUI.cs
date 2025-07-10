using AnimatedBattleText.Examples;
using PixelBattleText;
using UnityEngine;

public class TriggerUI : MonoBehaviour
{
    [SerializeField] private ExampleTextManager textManager;
    [SerializeField] private string triggerMessage;
    [SerializeField] public TextAnimation textToUse;
    public GameObject UIPanel;
    public bool isresetCountdown = false;
    playerController playerMovement;
    public ArrowInputUI arrowinput;

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
                Debug.LogWarning("TextManager fail");
            }

            UIPanel.SetActive(true);

            ArrowInputUI arrowUI = FindObjectOfType<ArrowInputUI>();
      
            if (arrowUI != null)
            {
                
                arrowUI.RestartSequenceFromTrigger(); 
            }


            isresetCountdown = true;
            resetCountDown();



        }
    }
    public void resetCountDown()
    {

        if (isresetCountdown)
        {
            arrowinput.resetCountdown();
        }
    }

}
