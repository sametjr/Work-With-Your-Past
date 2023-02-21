using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class RecordPlayer : MonoBehaviour
{
    private ListOfFrames record;
    private bool isPlaying = false;
    [SerializeField] private int simulationSpeedMills = 20;

    public void SetRecord(ListOfFrames _record)
    {

        this.record = _record;
        PlayRecord();
    }

    private void PlayRecord()
    {
        isPlaying = true;
        record.hasCreatedClone = true;
        HandlePlayRecord();
    }

    private void StopRecord()
    {
        isPlaying = false;
    }

    async private void HandlePlayRecord()
    {   
        for (int i = 0; i < record.frames.Count; i++)
        {
            if(!isPlaying) break;
            transform.position = record.frames[i].position;
            transform.rotation = record.frames[i].rotation;
            await Task.Delay(simulationSpeedMills);
        }
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
