using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.DieHere(other.transform.position);
        }
    }

}
