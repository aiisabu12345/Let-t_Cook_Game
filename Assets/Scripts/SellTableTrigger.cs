using AnimatedBattleText.Examples;
using PixelBattleText;
using UnityEngine;

public class SellTableTrigger : MonoBehaviour
{
    [SerializeField] private ExampleTextManager textManager;
    [SerializeField] private string triggerMessage;
    [SerializeField] public TextAnimation textToUse;


    playerController playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerMovement = GameObject.FindWithTag("Player").GetComponent<playerController>();
            if (playerMovement.Getholding())
            {
                Potion potion = playerMovement.GetholdingObject().GetComponent<Potion>();
                triggerMessage = "+ " + SellManager.Instance.SellPotion(potion.potion);
                Destroy(playerMovement.GetholdingObject().gameObject);

                if (textManager != null && textToUse != null)
                {
                    textManager.LastUsed = textToUse;
                    textManager.ShowInputText(triggerMessage);
                }
                else
                {
                    Debug.LogWarning("TextManager fail");
                }

                playerMovement.Setholding(false);
                playerMovement.SetisLifting(false);
                playerMovement.SetholdingObject(null);
            }

        }
    }
}
