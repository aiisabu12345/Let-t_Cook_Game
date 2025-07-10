using UnityEngine;

public class PartyButton : MonoBehaviour
{
    public PartyData party;
    public void Click()
    {
        GatheringManager.Instance.partyIdle.Remove(party);
        GatheringManager.Instance.partyHire.Add(party);
        Destroy(gameObject);
    }
}
