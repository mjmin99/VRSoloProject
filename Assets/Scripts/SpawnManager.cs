using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public ObjectPool notePool; // ObjectPool ����
    public ObjectPool effectPool;
    public Lane[] lanes;
    public AudioSource music;
    public TextAsset csvFile;
    public float noteSpeed = 5f;

    private List<NoteData>[] laneNotes;
    private int[] nextNoteIndex;

    void Start()
    {
        LoadPatternFromCSV();
        nextNoteIndex = new int[lanes.Length];
    }

    void Update()
    {
        float timer = music.time;

        for (int laneIndex = 0; laneIndex < lanes.Length; laneIndex++)
        {
            var lane = lanes[laneIndex];
            int noteIndex = nextNoteIndex[laneIndex];

            if (noteIndex < lane.notes.Count && timer >= lane.notes[noteIndex].time)
            {
                SpawnNoteAtLane(lane);
                nextNoteIndex[laneIndex]++;
            }
        }
    }

    void SpawnNoteAtLane(Lane lane)
    {
        GameObject note = notePool.GetObject();
        note.transform.position = lane.spawnPoint.position;
        note.transform.rotation = Quaternion.identity;

        Rigidbody rb = note.GetComponent<Rigidbody>();
        rb.velocity = Vector3.back * noteSpeed;

        // ��Ʈ ������ ���� �ð� �� Ǯ�� ���ư��� ó�� ����
        note.GetComponent<Note>().Init(notePool);
    }

    void LoadPatternFromCSV()
    {
        string[] lines = csvFile.text.Split('\n');
        Debug.Log("CSV ���� ��: " + lines.Length); // �� ������ Ȯ��

        for (int i = 1; i < lines.Length; i++) // ��� ����
        {
            string[] values = lines[i].Split(',');
            if (values.Length < 2) continue;

            int laneIndex = int.Parse(values[0]);
            float time = float.Parse(values[1]);

            Debug.Log($"�ε��: ���� {laneIndex}, �ð� {time}");
            lanes[laneIndex].notes.Add(new NoteData { time = time });
        }
    }
}