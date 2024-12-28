using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI_Manager : MonoBehaviour
{
    [SerializeField] private string nomSceneJeu = null;
    public void Play()
    {
        SceneManager.LoadScene(nomSceneJeu);
    }
    
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
