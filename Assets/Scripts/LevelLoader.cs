using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;

    public void RestartScene()
    {
        StartCoroutine(RestartSceneCoroutine());
    }
    public IEnumerator RestartSceneCoroutine()
    {
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
