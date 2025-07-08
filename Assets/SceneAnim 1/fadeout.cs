using System.Collections;
using UnityEngine;

public class fadeout : MonoBehaviour
{
    public Animator fadeAnimator;

    void Start()
    {
        StartCoroutine(fadeout1());
    }
    IEnumerator fadeout1()
    {
        fadeAnimator.Play("fadeout");
        yield return new WaitForSeconds(3f);
    }
}
