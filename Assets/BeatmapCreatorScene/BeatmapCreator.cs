using UnityEngine;
using System.Collections.Generic;
using System.IO; // ������ ���� ���� �ʿ��մϴ�.

// ��Ʈ �ϳ��� ������ ��� ����ü (NoteSpawner.cs�� ������ ����)


// Unity �����Ϳ��� ��Ʈ���� �����ϴ� ����� ����ϴ� Ŭ����
public class BeatmapCreator : MonoBehaviour
{
    [Header("����")]
    [Tooltip("��Ʈ���� ������ ����� Ŭ��")]
    public AudioClip musicClip;

    [Tooltip("����� CSV ���� �̸� (Ȯ���� ����)")]
    public string saveFileName = "newBeatmap";

    // --- ���� ���� ---
    private AudioSource musicSource;
    private List<NoteData> recordedNotes = new List<NoteData>();
    private bool isRecording = false;

    // ������ ���۵� �� ȣ�� (��� ��� ���� ��)
    void Start()
    {
        // AudioSource ������Ʈ�� ������ �߰�
        musicSource = gameObject.GetComponent<AudioSource>();
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }

        // ����� Ŭ�� ���� �� ���
        musicSource.clip = musicClip;
        musicSource.Play();
        isRecording = true;

        Debug.Log("--- ��Ʈ�� ��ȭ ���� ---");
        Debug.Log("���ǿ� ���� ���� Ű(1, 2, 3...)�� ���� ��Ʈ�� ����ϼ���.");
    }

    // �� �����Ӹ��� ȣ��
    void Update()
    {
        if (!isRecording) return;

        // Ű���� �Է� ���� (1�� ~ 9�� Ű)
        for (int i = 0; i < 9; i++)
        {
            // GetKeyDown�� Ű�� ������ ���� �� ���� true�� �˴ϴ�.
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                // ���� ���� ��� �ð��� ���� Ű(����) ������ ���
                float currentTime = musicSource.time;
                int laneIndex = i;

                recordedNotes.Add(new NoteData(currentTime, laneIndex));
                Debug.Log($"��Ʈ ��ϵ� - �ð�: {currentTime:F2}, ����: {laneIndex}");
            }
        }
    }

    // ������Ʈ�� �ı��� �� ȣ�� (��� ��� ���� ��)
    void OnDestroy()
    {
        if (recordedNotes.Count > 0)
        {
            SaveNotesToCSV();
        }
        else
        {
            Debug.LogWarning("��ϵ� ��Ʈ�� ���� ������ �������� �ʾҽ��ϴ�.");
        }
    }

    // ��ϵ� ��Ʈ �����͸� CSV ���Ϸ� �����ϴ� �Լ�
    void SaveNotesToCSV()
    {
        // ���� ��� ���� (Assets/Resources/�����̸�.csv)
        string filePath = Path.Combine(Application.dataPath, "Resources", saveFileName + ".csv");

        // StreamWriter�� ����Ͽ� ���Ͽ� �ؽ�Ʈ�� ��
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("# " + saveFileName + " Beatmap");
            writer.WriteLine("# spawnTime,laneIndex");

            // �ð� ������ ��Ʈ ����
            recordedNotes.Sort((a, b) => a.spawnTime.CompareTo(b.spawnTime));

            foreach (NoteData note in recordedNotes)
            {
                // "�ð�,����" �������� �� �پ� ����
                writer.WriteLine($"{note.spawnTime},{note.laneIndex}");
            }
        }

        Debug.Log($"<color=lime>��Ʈ�� ���� �Ϸ�! �� {recordedNotes.Count}���� ��Ʈ�� {filePath}�� ����Ǿ����ϴ�.</color>");

        // Unity �����Ͱ� ������ �ν��ϵ��� ���� ���ΰ�ħ (���û��������� ����)
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}
