using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionsManager : MonoBehaviour
{
    #region Singleton
    public static ActionsManager Instance;
    bool firstButtonPressed = false;
    bool secondButtonPressed = false;
    private void Awake()
    {
        // DontDestroyOnLoad(gameObject);
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

    private List<ObjectsWithPositions> objectsWithPositions = new List<ObjectsWithPositions>();
    [SerializeField] private ObjectsWithPositions sword;
    [SerializeField] private ObjectsWithPositions road1;
    [SerializeField] private ObjectsWithPositions platform2Star;
    [SerializeField] private ObjectsWithPositions road2;
    [SerializeField] private ObjectsWithPositions spawnPointFlag;
    [SerializeField] private ObjectsWithPositions spawnPointFlag2;
    [SerializeField] private ObjectsWithPositions road3;
    [SerializeField] private ObjectsWithPositions road4;
    [SerializeField] private ObjectsWithPositions road5;
    [SerializeField] private ObjectsWithPositions road6;
    [SerializeField] private ObjectsWithPositions targetBoard;
    [SerializeField] private ObjectsWithPositions arrow;
    [SerializeField] private ObjectsWithPositions glassRoad;

    private int glassRoadCount = 0;
    [SerializeField] private TextMeshPro glassRoadText;
    [SerializeField] private GameObject gameFinishedText;



    private void Start()
    {

        if (MovementRecorder.Instance.hasFlagCaptured)
        {
            ChangeFlagColor();
            OpenThirdRoad();
        }

        if (MovementRecorder.Instance.hasFlag2Captured)
        {
            ChangeFlag2Color();
            OpenSixthRoad();
        }

        objectsWithPositions.Add(sword);
        objectsWithPositions.Add(road1);
        objectsWithPositions.Add(road4);
        objectsWithPositions.Add(targetBoard);
        foreach (ObjectsWithPositions objectWithPosition in objectsWithPositions)
        {
            objectWithPosition.SetStartPosition();
        }
    }

    // private void Update() {
    //     if(Input.GetKeyDown(KeyCode.O)) {
    //         OpenGlassRoad();
    //     }
    // }
    private void FixedUpdate()
    {
        ControlButtons();
    }
    public void MakeAction(Actions action, bool _entered)
    {
        switch (action)
        {
            case Actions.FirstButtonPressed:
                HandleFirstButtonPressed(_entered);
                break;
            case Actions.SecondButtonPressed:
                HandleSecondButtonPressed(_entered);
                break;
            case Actions.Platform2ButtonPressed:
                DropPlatform2Star();
                break;
            case Actions.Platform2StarCollected:
                OpenSecondRoad();
                break;
            case Actions.SpawnPointFlagCaptured:
                if(MovementRecorder.Instance.hasFlagCaptured) return;
                SetSpawnPoint();
                ClearRecords();
                ChangeFlagColor();
                OpenThirdRoad();
                break;

            case Actions.SpawnPointFlag2Captured:
                if(MovementRecorder.Instance.hasFlag2Captured) return;
                SetSpawnPoint2();
                ClearRecords();
                ChangeFlag2Color();
                OpenSixthRoad();
                break;
            case Actions.JumpedOverObstacle:
                ControlObstacles(_entered);
                break;
            case Actions.TargetBoardButtonPressed:
                ToggleTargetBoard(_entered);
                break;
            case Actions.ThrowArrowButtonPressed:
                ThrowArrow();
                break;
            case Actions.ArrowOnTarget:
                OpenFifthRoad();
                break;
            case Actions.TouchedGlassRoad:
                CountGlassRoads(_entered);
                break;
            case Actions.GameFinished:
                gameFinishedText.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void CountGlassRoads(bool entered)
    {
        glassRoadCount += entered ? 1 : -1;
        UpdateGlassCountText();
        if(glassRoadCount == 3)
        {
            OpenGlassRoad();
        }
    }

    private void UpdateGlassCountText()
    {
        glassRoadText.text = glassRoadCount.ToString();
    }

    private void OpenGlassRoad()
    {
        LeanTween.rotateLocal(glassRoad.gameObject, Vector3.right * 90, 1.3f).setEaseOutBounce();
    }

    private void ThrowArrow()
    {
        LeanTween.move(arrow.gameObject, arrow.endPosition, 0.3f).setEase(LeanTweenType.easeOutQuad);
    }

    private void OpenFifthRoad()
    {
        LeanTween.move(road5.gameObject, road5.endPosition, 0.7f).setEaseOutBounce();
    }

    private void ToggleTargetBoard(bool entered)
    {
        if (entered)
        {
            LeanTween.move(targetBoard.gameObject, targetBoard.endPosition, 0.7f).setEaseOutBounce();
        }
        else
        {
            LeanTween.move(targetBoard.gameObject, targetBoard.startPosition, 0.7f).setEaseOutBounce();
        }
    }

    private void ControlObstacles(bool entered)
    {
        if (entered)
        {
            OpenForthRoad();
        }
        else
        {
            CloseForthRoad();
        }
    }

    private void CloseForthRoad()
    {
        LeanTween.move(road4.gameObject, road4.startPosition, 0.7f).setEaseOutBounce();
    }

    private void OpenForthRoad()
    {
        LeanTween.move(road4.gameObject, road4.endPosition, 0.7f).setEaseOutBounce();
    }

    private void OpenThirdRoad()
    {
        LeanTween.move(road3.gameObject, road3.endPosition, 0.7f).setEaseOutBounce();
    }

    private void OpenSixthRoad()
    {
        LeanTween.move(road6.gameObject, road6.endPosition, 0.7f).setEaseOutBounce();
    }

    private void HandleSecondButtonPressed(bool entered)
    {
        secondButtonPressed = entered;
    }

    private void HandleFirstButtonPressed(bool entered)
    {
        LeanTween.move(sword.gameObject, sword.endPosition, 0.7f).setEase(LeanTweenType.easeOutQuad);
        firstButtonPressed = entered;
    }

    private void ChangeFlagColor()
    {
        spawnPointFlag.gameObject.GetComponent<MeshRenderer>().materials[2].color = Color.green;
    }

    private void ChangeFlag2Color()
    {
        spawnPointFlag2.gameObject.GetComponent<MeshRenderer>().materials[2].color = Color.green;
    }

    private void ClearRecords()
    {
        MovementRecorder.Instance.ClearAllRecords();
    }

    private void SetSpawnPoint()
    {
        MovementRecorder.Instance.SetSpawnPoint(spawnPointFlag.gameObject.transform.position);
        MovementRecorder.Instance.hasFlagCaptured = true;
    }

    private void SetSpawnPoint2()
    {
        MovementRecorder.Instance.SetSpawnPoint(spawnPointFlag2.gameObject.transform.position);
        MovementRecorder.Instance.hasFlag2Captured = true;
    }

    private void OpenSecondRoad()
    {
        Debug.Log("OpenSecondRoad");
        LeanTween.move(road2.gameObject, road2.endPosition, 0.7f).setEaseOutBounce();
    }

    private void DropPlatform2Star()
    {
        LeanTween.move(platform2Star.gameObject, platform2Star.endPosition, 4f).setEase(LeanTweenType.easeOutQuad);
    }

    private void ControlButtons()
    {
        if (firstButtonPressed && secondButtonPressed)
        {
            LeanTween.move(road1.gameObject, road1.endPosition, 0.7f).setEaseOutBounce();
        }
        else
        {
            LeanTween.move(road1.gameObject, road1.startPosition, 0.7f).setEaseOutBounce();
        }
    }
}
















public enum Actions
{
    FirstButtonPressed,
    SecondButtonPressed,
    Platform2ButtonPressed,
    Platform2StarCollected,
    SpawnPointFlagCaptured,
    JumpedOverObstacle,
    TargetBoardButtonPressed,
    ThrowArrowButtonPressed,
    SpawnPointFlag2Captured,
    TouchedGlassRoad,
    GameFinished,
    ArrowOnTarget,

}


[Serializable]
public class ObjectsWithPositions
{
    public GameObject gameObject;
    public Vector3 startPosition;
    public Vector3 endPosition;

    public void SetStartPosition()
    {
        startPosition = this.gameObject.transform.position;
    }
}
