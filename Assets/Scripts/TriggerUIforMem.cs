using AnimatedBattleText.Examples;
using PixelBattleText;
using UnityEngine;

public class TriggerUIforMem : MonoBehaviour
{

    public GameObject UIPanel;
    public bool isresetCountdown = false;
    playerController playerMovement;
    public CardsController cardsCr;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            playerMovement = GameObject.FindWithTag("Player").GetComponent<playerController>();
            playerMovement.EnableControl(false);
           

            UIPanel.SetActive(true);

     
            isresetCountdown = true;
            resetCountDown();


        }
    }

    public void resetCountDown()
    {

        if(isresetCountdown)
        {
            cardsCr.resetCountdown();
        }
    }
    }
