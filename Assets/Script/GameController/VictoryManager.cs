
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : AudioMonoBehaviour
{
    [SerializeField]
    public int sceneLoad;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Win");
            DontDestroyOnLoad(GameManager.Instance);
            PlaySound();
            StartCoroutine(ChangeScene());
        }
    }

    IEnumerator ChangeScene()
    {
        TransitionManager.Instance.EndTransition();
        yield return new WaitForSeconds(2.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
