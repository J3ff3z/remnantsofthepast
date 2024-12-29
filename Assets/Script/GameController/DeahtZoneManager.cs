using UnityEngine;

public class DeahtZoneManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.DieHere(other.transform.position);
        }
    }

}
