using UnityEngine;

public class DeahtZoneManager : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.DieHere(other.transform.position);
        }
    }
}
