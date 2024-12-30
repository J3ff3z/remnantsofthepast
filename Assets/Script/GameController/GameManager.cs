using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject savePoint;
    [SerializeField] GameObject corpsePrefab;

    List<GameObject> corpseList;
    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        savePoint = GameObject.Find("StartPoint");
        instance = this;
        corpseList = new List<GameObject>();
    }

    public void DieHere(Vector3 _currentPosition, bool isCharac = true)
    {
        GameObject newCorpse = Instantiate(corpsePrefab);
        newCorpse.transform.position = _currentPosition;
        corpseList.Add(newCorpse);
        if (isCharac)
        {
            CharacterManager.Instance.Restart(savePoint.transform.position);
        }
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
        savePoint.transform.position = position;
    }
}
