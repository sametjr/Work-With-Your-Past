using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class FrameQuantities
{
    public Vector3 position;
    public Quaternion rotation;
}

public class ListOfFrames
{
    public List<FrameQuantities> frames;
    public bool hasCreatedClone;
}


public class MovementRecorder : MonoBehaviour
{
    #region Singleton
    public static MovementRecorder Instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion


    public GameObject player;
    public bool hasFlagCaptured = false;
    public bool hasFlag2Captured = false;
    [SerializeField] private GameObject clonePrefab;
    private ListOfFrames recordedFrames;
    private Queue<ListOfFrames> allRecords = new();
    private bool isRecording = false;
    private Vector3 spawnPosition;

    public void SetSpawnPoint(Vector3 position)
    {
        spawnPosition = position;
    }

    private void Start()
    {
        SetSpawnPoint(player.transform.position);
        SceneManager.sceneLoaded += OnSceneLoaded;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (ListOfFrames record in allRecords)
        {
            if (record != null)
                record.hasCreatedClone = false;
        }
        player.transform.position = spawnPosition;

        HandleUI();
    }

    private void HandleUI()
    {
        RecordsUIManager.Instance.SceneLoaded(allRecords.Count);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // ToggleRecording();
            StartRecording();
        }



        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateClone(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CreateClone(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CreateClone(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CreateClone(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CreateClone(4);
        }
    }

    private void CreateClone(int cloneIndex)
    {
        try
        {
            allRecords.Peek();
            Debug.Log("allRecords Peeked");
        }
        catch
        {
            Debug.Log("allRecords is empty");
            return;
        }


        if (allRecords.Count == 0 || allRecords.Count < cloneIndex + 1)
        {
            Debug.Log("allRecords is empty or index is out of range");
            return;
        }
        if (allRecords.ToList()[cloneIndex].hasCreatedClone)
        {
            Debug.Log("Clone already created");
            return;
        }

        GameObject clone = Instantiate(clonePrefab);
        Destroy(clone.GetComponent<PlayerMovement>());
        clone.AddComponent<RecordPlayer>();
        clone.AddComponent<CloneControl>();
        Color color = clone.GetComponent<MeshRenderer>().material.color;
        color.a = 0.3f;
        clone.GetComponent<MeshRenderer>().material.color = color;
        clone.GetComponent<RecordPlayer>().SetRecord(allRecords.ToList()[cloneIndex]);

        RecordsUIManager.Instance.RecordAlreadyPlayed(cloneIndex);
    }

    private void ToggleRecording()
    {
        if (isRecording)
        {
            StopRecording();
        }
        else
        {
            StartRecording();
        }
    }
    private void FixedUpdate()
    {

        if (isRecording)
        {
            FrameQuantities frame = new();
            frame.position = player.transform.position;
            frame.rotation = player.transform.rotation;
            recordedFrames.frames.Add(frame);
        }
    }

    private void StartRecording()
    {
        recordedFrames = new();
        recordedFrames.frames = new();
        isRecording = true;
        RecordingScreenHandler.Instance.StartRecording();
    }

    public void StopRecording()
    {
        if(!isRecording)
            return;
        
        isRecording = false;
        RecordingScreenHandler.Instance.StopRecording();
        if (allRecords.Count == 5)
            allRecords.Dequeue();
        
        allRecords.Enqueue(recordedFrames);
        // Update UI -- parametes are : allRecords.Count
        RecordsUIManager.Instance.RecordAdded(allRecords.Count - 1);
        recordedFrames = new();
    }

    public void ClearAllRecords()
    {
        allRecords = new();
        RecordsUIManager.Instance.RecordsCleared();
    }

    public ListOfFrames GetRecord(int index)
    {
        return allRecords.ToList()[index];
    }
}


