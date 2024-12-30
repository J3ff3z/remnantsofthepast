using System.Collections;
using UnityEngine;

public class DeahtZoneManager : MonoBehaviour
{
    public bool hasDie = false;




    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !hasDie)
        {
            hasDie = true;
            GameManager.Instance.DieHere(other.transform.position);
            StartCoroutine(ResetDeath());
        }
    }

    IEnumerator ResetDeath()
    {
        yield return new WaitForSeconds(0.5f);
        hasDie = false;
    }

}
