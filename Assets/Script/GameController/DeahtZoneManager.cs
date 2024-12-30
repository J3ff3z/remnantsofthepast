using System.Collections;
using UnityEngine;

public class DeahtZoneManager : AudioMonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.DieHere(other.transform.position);
            PlaySound();
            return;
        }
        if (other.gameObject.CompareTag("Spawner"))
        {
            GameManager.Instance.DieHere(other.transform.position, false);
            other.GetComponent<SpawnerManager>().Restart();
            return;
        }
    }

}
