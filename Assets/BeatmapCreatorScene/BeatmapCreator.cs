using UnityEngine;
using System.Collections.Generic;
using System.IO; // 파일을 쓰기 위해 필요합니다.

// 노트 하나의 정보를 담는 구조체 (NoteSpawner.cs와 동일한 구조)


// Unity 에디터에서 비트맵을 제작하는 기능을 담당하는 클래스
public class BeatmapCreator : MonoBehaviour
{
    [Header("설정")]
    [Tooltip("비트맵을 제작할 오디오 클립")]
    public AudioClip musicClip;

    [Tooltip("저장될 CSV 파일 이름 (확장자 제외)")]
    public string saveFileName = "newBeatmap";

    // --- 내부 변수 ---
    private AudioSource musicSource;
    private List<NoteData> recordedNotes = new List<NoteData>();
    private bool isRecording = false;

    // 게임이 시작될 때 호출 (재생 모드 진입 시)
    void Start()
    {
        // AudioSource 컴포넌트가 없으면 추가
        musicSource = gameObject.GetComponent<AudioSource>();
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }

        // 오디오 클립 설정 및 재생
        musicSource.clip = musicClip;
        musicSource.Play();
        isRecording = true;

        Debug.Log("--- 비트맵 녹화 시작 ---");
        Debug.Log("음악에 맞춰 숫자 키(1, 2, 3...)를 눌러 노트를 기록하세요.");
    }

    // 매 프레임마다 호출
    void Update()
    {
        if (!isRecording) return;

        // 키보드 입력 감지 (1번 ~ 9번 키)
        for (int i = 0; i < 9; i++)
        {
            // GetKeyDown은 키가 눌리는 순간 한 번만 true가 됩니다.
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                // 현재 음악 재생 시간과 누른 키(레인) 정보를 기록
                float currentTime = musicSource.time;
                int laneIndex = i;

                recordedNotes.Add(new NoteData(currentTime, laneIndex));
                Debug.Log($"노트 기록됨 - 시간: {currentTime:F2}, 레인: {laneIndex}");
            }
        }
    }

    // 오브젝트가 파괴될 때 호출 (재생 모드 종료 시)
    void OnDestroy()
    {
        if (recordedNotes.Count > 0)
        {
            SaveNotesToCSV();
        }
        else
        {
            Debug.LogWarning("기록된 노트가 없어 파일을 저장하지 않았습니다.");
        }
    }

    // 기록된 노트 데이터를 CSV 파일로 저장하는 함수
    void SaveNotesToCSV()
    {
        // 저장 경로 설정 (Assets/Resources/파일이름.csv)
        string filePath = Path.Combine(Application.dataPath, "Resources", saveFileName + ".csv");

        // StreamWriter를 사용하여 파일에 텍스트를 씀
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("# " + saveFileName + " Beatmap");
            writer.WriteLine("# spawnTime,laneIndex");

            // 시간 순으로 노트 정렬
            recordedNotes.Sort((a, b) => a.spawnTime.CompareTo(b.spawnTime));

            foreach (NoteData note in recordedNotes)
            {
                // "시간,레인" 형식으로 한 줄씩 저장
                writer.WriteLine($"{note.spawnTime},{note.laneIndex}");
            }
        }

        Debug.Log($"<color=lime>비트맵 저장 완료! 총 {recordedNotes.Count}개의 노트가 {filePath}에 저장되었습니다.</color>");

        // Unity 에디터가 파일을 인식하도록 강제 새로고침 (선택사항이지만 권장)
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}
