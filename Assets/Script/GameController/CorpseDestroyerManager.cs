using UnityEngine;

public class CorpseDestroyerManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.CompareTag("Grabbable"))
        {
            GameManager.Instance.CleanCorpse(other.gameObject);
        }
    }
}