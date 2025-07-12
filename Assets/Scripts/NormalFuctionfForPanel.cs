using UnityEngine;

public abstract class NormalFunctionForPanel : MonoBehaviour
{
    public void ClosePanel(GameObject p)
    {
        p.SetActive(false);
    }

    public abstract void OnOpen();
}
