using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NoteData
{
    public float spawnTime;
    public int laneIndex;

    public NoteData(float time, int lane)
    {
        spawnTime = time;
        laneIndex = lane;
    }
}