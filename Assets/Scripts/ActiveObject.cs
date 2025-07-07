using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveObject : MonoBehaviour
{
    private float waitBeforeQuit = 1.5f;
    public GameObject targetObject;
   
    void Awake()
    {
        targetObject.SetActive(false);
    }
    public void TurnOn()
    {
        targetObject.SetActive(true);
    }


    public void QuitGame()
    {
        StartCoroutine(QuitAfterDelay());

     

    }
    public void nextScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }
    IEnumerator LoadScene(string sceneName)
    {
   
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(sceneName);
    }
    private IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSeconds(waitBeforeQuit);
        Application.Quit();

    }
}
