using AnimatedBattleText.Examples;
using PixelBattleText;
using UnityEngine;

public class NormalTriggerPanel : MonoBehaviour
{
    [SerializeField] private ExampleTextManager textManager;
    [SerializeField] private string triggerMessage;
    [SerializeField] public TextAnimation textToUse;
    public GameObject UIPanel;
    public bool check = false;

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

<<<<<<< HEAD
=======

>>>>>>> origin/dev2.2
            UIPanel.SetActive(true);

            if (!check)
            {
                check = true;
                NormalFunctionForPanel np = UIPanel.GetComponentInParent<NormalFunctionForPanel>();
                if (np != null)
                {
                    np.OnOpen();
                    Debug.Log("Open");
                }
            }
<<<<<<< HEAD
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            check = false;
=======

>>>>>>> origin/dev2.2
        }
    }
}
