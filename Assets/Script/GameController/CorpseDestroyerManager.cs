using UnityEngine;

public class CorpseDestroyerManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            GameManager.Instance.CleanCorpse(other.gameObject);
        }
    }
}