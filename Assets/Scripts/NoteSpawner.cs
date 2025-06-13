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
        // CSV ���� �Ľ�
        allNotes = CSVReader.Parse(csvFileName);
        // Ȥ�� �� ���� ������ �����ϱ� ���� �ð� ������ ����
        allNotes.Sort((a, b) => a.spawnTime.CompareTo(b.spawnTime));
    }

    void Update()
    {
        // ���� ������ ��Ʈ�� �ְ�, ���� ��� �ð��� ��Ʈ ���� �ð� �̻��̶��
        if (nextNoteIndex < allNotes.Count && musicSource.time >= allNotes[nextNoteIndex].spawnTime)
        {
            NoteData noteToSpawn = allNotes[nextNoteIndex];

            // ���� �ε����� �´� ��ġ ���
            Vector3 spawnPos = new Vector3(lanePositionsX[noteToSpawn.laneIndex], spawnY, spawnZ);

            // ������Ʈ Ǯ���� ��Ʈ ��������
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
    // CSV ������ �о� NoteData ����Ʈ�� ��ȯ�ϴ� ���� �Լ�
    public static List<NoteData> Parse(string csvFileName)
    {
        List<NoteData> noteDataList = new List<NoteData>();
        // Resources �������� �ؽ�Ʈ ���� �ε�
        TextAsset csvData = Resources.Load<TextAsset>(csvFileName);

        if (csvData == null)
        {
            Debug.LogError("CSV file not found in Resources folder: " + csvFileName);
            return noteDataList;
        }

        // �� ������ �ؽ�Ʈ�� �и�
        string[] lines = csvData.text.Split('\n');

        foreach (string line in lines)
        {
            // �� ���̰ų� #���� �����ϴ� �ּ��� �ǳʶ�
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;

            // ��ǥ�� ���� �и�
            string[] values = line.Trim().Split(',');
            if (values.Length >= 2)
            {
                // Parse�� ������ ��츦 ����� TryParse ��� ����
                if (float.TryParse(values[0], out float time) && int.TryParse(values[1], out int lane))
                {
                    noteDataList.Add(new NoteData(time, lane));
                }
            }
        }
        return noteDataList;
    }
}

