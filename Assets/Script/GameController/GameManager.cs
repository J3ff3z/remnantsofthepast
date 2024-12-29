using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject startPoint;
    [SerializeField] GameObject corpsePrefab;

    List<GameObject> corpseList;
    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        startPoint = transform.GetChild(0).gameObject;
        instance = this;
        corpseList = new List<GameObject>();
    }

    public void DieHere(Vector3 _currentPosition)
    {
        GameObject newCorpse = Instantiate(corpsePrefab);
        newCorpse.transform.position = _currentPosition;
        corpseList.Add(newCorpse);
        Debug.Log(startPoint
            .transform.position.ToString());
        CharacterManager.Instance.Restart(startPoint.transform.position);
    }

    public void CleanCorpse()
    {
        foreach (GameObject corpse in corpseList) { Destroy(corpse); }
        corpseList.Clear();
    }

    public void CleanCorpse(GameObject corpse)
    {
        corpseList.Remove(corpse);
        Destroy(corpse);
    }

    public void SwitchSavePoint(Vector2 position)
    {
        transform.GetChild(0).position = position;
    }
}
