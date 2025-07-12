using UnityEngine;

public class InventoryChestTrigger : MonoBehaviour
{
    public bool check = false;
    playerController playerMovement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerMovement = GameObject.FindWithTag("Player").GetComponent<playerController>();

            if (playerMovement.Getholding())
            {
                Potion potion = playerMovement.GetholdingObject().GetComponent<Potion>();
                InventoryManager.Instance.potionList.Add(potion.potion);
                Destroy(playerMovement.GetholdingObject().gameObject);

                InventoryManager.Instance.OnOpen();

                playerMovement.Setholding(false);
                playerMovement.SetisLifting(false);
                playerMovement.SetholdingObject(null);
            }
        }
    }
}
