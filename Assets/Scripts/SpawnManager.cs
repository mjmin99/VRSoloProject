using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public ObjectPool notePool; // ObjectPool 연결
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

        // 노트 스스로 일정 시간 뒤 풀로 돌아가게 처리 가능
        note.GetComponent<Note>().Init(notePool);
    }

    void LoadPatternFromCSV()
    {
        string[] lines = csvFile.text.Split('\n');
        Debug.Log("CSV 라인 수: " + lines.Length); // 몇 줄인지 확인

        for (int i = 1; i < lines.Length; i++) // 헤더 제외
        {
            string[] values = lines[i].Split(',');
            if (values.Length < 2) continue;

            int laneIndex = int.Parse(values[0]);
            float time = float.Parse(values[1]);

            Debug.Log($"로드됨: 레인 {laneIndex}, 시간 {time}");
            lanes[laneIndex].notes.Add(new NoteData { time = time });
        }
    }
}