using AnimatedBattleText.Examples;
using PixelBattleText;
using UnityEngine;

public class NormalTriggerPanel : MonoBehaviour
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
                Debug.LogWarning("TextManager fail");
            }

            NormalFunctionForPanel np = UIPanel.GetComponent<NormalFunctionForPanel>();
            if (np != null)
            {
                np.OnOpen();
                Debug.Log("Open");
            }

            UIPanel.SetActive(true);
        }
    }
}
