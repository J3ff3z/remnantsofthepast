using System.Collections;
using UnityEngine;

public class DeahtZoneManager : AudioMonoBehaviour
{
    public bool hasDie = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasDie)
        {
            hasDie = true;
            GameManager.Instance.DieHere(other.transform.position);
            StartCoroutine(ResetDeath());
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

    IEnumerator ResetDeath()
    {
        yield return new WaitForSeconds(0.5f);
        hasDie = false;
    }

}
