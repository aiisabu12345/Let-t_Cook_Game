using UnityEngine;
using UnityEngine.UI;

public class PartyButton : MonoBehaviour
{
    public PartyData party;
    public Button hireButton;
    void Update()
    {
        if (GameManager.Instance.cocoCoin < party.cost)
        {
            hireButton.interactable = false;
        }
        else
        {
            hireButton.interactable = true;
        }
    }
    public void Click()
    {
        GameManager.Instance.UseCoco(party.cost);
        GatheringManager.Instance.partyIdle.Remove(party);
        GatheringManager.Instance.partyHire.Add(party);
        Destroy(gameObject);
    }
}
