using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class scenecontrol : MonoBehaviour
{
     [SerializeField] public Animator fadeAnimator;
   

    void Awake()
    {
        StartCoroutine(FadeThenLoad());
    }
    
    IEnumerator FadeThenLoad()
    {
        fadeAnimator.Play("fadein");
        yield return new WaitForSeconds(2f);

    }
    
}
