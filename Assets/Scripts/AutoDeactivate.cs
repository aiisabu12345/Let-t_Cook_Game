using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AutoDeactivate : MonoBehaviour
{
    public string sceneToLoad;

    private void Start()
    {
        StartCoroutine(LoadSceneAfterSeconds(77f));
    }

    IEnumerator LoadSceneAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneToLoad);
    }
}
