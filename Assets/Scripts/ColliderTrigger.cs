using AnimatedBattleText.Examples;
using PixelBattleText;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    [SerializeField] private ExampleTextManager textManager;
    [SerializeField] private string triggerMessage;
    [SerializeField] public TextAnimation textToUse;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textManager.LastUsed = textToUse;
            textManager.ShowInputText(triggerMessage);
         
        }
    }
}