using UnityEngine;

public class CorpseDestroyerManager : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.tag == "Grabbable")
        {
            GameManager.Instance.CleanCorpse(other.gameObject);
        }
    }
}