using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Win");
            StartCoroutine(ChangeScene());
        }
    }


    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
