using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
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

    [SerializeField] private GameObject player;
    [SerializeField] private LevelLoader levelLoader;

    public GameObject Player => player;

    
    public void PlayerDead()
    {
        Debug.Log("Player Dead");
        MovementRecorder.Instance.StopRecording();
        levelLoader.RestartScene();
        Debug.Log("Restarting Scene");
    }
}
