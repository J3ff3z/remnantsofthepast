using UnityEngine;

public class DeahtZoneManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.DieHere(other.transform.position);
        }
    }

}
