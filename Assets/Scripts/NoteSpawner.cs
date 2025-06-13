using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class NoteSpawner : MonoBehaviour
{
    public string csvFileName;
    public float[] lanePositionsX;
    public float spawnZ = 20f;
    public float spawnY = 1.6f;
    public AudioSource musicSource;

    private List<NoteData> allNotes;
    private int nextNoteIndex = 0;

    void Start()
    {
        // CSV 파일 파싱
        allNotes = CSVReader.Parse(csvFileName);
        // 혹시 모를 순서 오류를 방지하기 위해 시간 순으로 정렬
        allNotes.Sort((a, b) => a.spawnTime.CompareTo(b.spawnTime));
    }

    void Update()
    {
        // 다음 생성할 노트가 있고, 음악 재생 시간이 노트 생성 시간 이상이라면
        if (nextNoteIndex < allNotes.Count && musicSource.time >= allNotes[nextNoteIndex].spawnTime)
        {
            NoteData noteToSpawn = allNotes[nextNoteIndex];

            // 레인 인덱스에 맞는 위치 계산
            Vector3 spawnPos = new Vector3(lanePositionsX[noteToSpawn.laneIndex], spawnY, spawnZ);

            // 오브젝트 풀에서 노트 꺼내오기
            ObjectPooler.Instance.SpawnFromPool("Note", spawnPos, Quaternion.identity);

            nextNoteIndex++;
        }
    }
}


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

public class CSVReader
{
    // CSV 파일을 읽어 NoteData 리스트로 반환하는 정적 함수
    public static List<NoteData> Parse(string csvFileName)
    {
        List<NoteData> noteDataList = new List<NoteData>();
        // Resources 폴더에서 텍스트 에셋 로드
        TextAsset csvData = Resources.Load<TextAsset>(csvFileName);

        if (csvData == null)
        {
            Debug.LogError("CSV file not found in Resources folder: " + csvFileName);
            return noteDataList;
        }

        // 줄 단위로 텍스트를 분리
        string[] lines = csvData.text.Split('\n');

        foreach (string line in lines)
        {
            // 빈 줄이거나 #으로 시작하는 주석은 건너뜀
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;

            // 쉼표로 값을 분리
            string[] values = line.Trim().Split(',');
            if (values.Length >= 2)
            {
                // Parse에 실패할 경우를 대비해 TryParse 사용 권장
                if (float.TryParse(values[0], out float time) && int.TryParse(values[1], out int lane))
                {
                    noteDataList.Add(new NoteData(time, lane));
                }
            }
        }
        return noteDataList;
    }
}

