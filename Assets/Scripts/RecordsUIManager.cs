using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordsUIManager : MonoBehaviour
{
    #region Singleton
    public static RecordsUIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    [SerializeField] private GameObject container;
    [SerializeField] private Sprite okSprite;
    [SerializeField] private Sprite notSprite;
    [SerializeField] private Sprite playedSprite;
    public void RecordAdded(int _recordIndex)
    {
        container.transform.GetChild(_recordIndex).GetComponent<Image>().sprite = okSprite;
    }

    public void SceneLoaded(int count)
    {
        for(int i = 0; i < count; i++)
        {
            container.transform.GetChild(i).GetComponent<Image>().sprite = okSprite;
        }
    }

    public void RecordsCleared()
    {
        foreach (Transform child in container.transform)
        {
            child.GetComponent<Image>().sprite = notSprite;
        }
    }

    public void RecordAlreadyPlayed(int _recordIndex)
    {
        container.transform.GetChild(_recordIndex).GetComponent<Image>().sprite = playedSprite;
    }

    public void PlayerRestarted(int _recordIndex)
    {
        foreach (Transform child in container.transform)
        {
            if(child.GetComponent<Image>().sprite == playedSprite)
            {
                child.GetComponent<Image>().sprite = okSprite;
            }
        }
    }
}
