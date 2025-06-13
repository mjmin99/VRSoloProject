using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Lane
{
    public Transform spawnPoint;
    public List<NoteData> notes = new List<NoteData>();
}
