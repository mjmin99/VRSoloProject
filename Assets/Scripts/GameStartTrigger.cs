using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameStartTrigger : MonoBehaviour

{
    public NoteSpawner spawnManager;  // 스폰매니저 연결
    public Text startText;  // Text TMP 연결

    private void Start()
    {
        spawnManager.enabled = false; // 일단 노트 스폰 막음
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        startText.text = "3";
        yield return new WaitForSeconds(1f);

        startText.text = "2";
        yield return new WaitForSeconds(1f);

        startText.text = "1";
        yield return new WaitForSeconds(1f);

        startText.text = "Start!";
        yield return new WaitForSeconds(1f);

        startText.text = ""; // 텍스트 지움
        spawnManager.enabled = true; // 게임 시작 (스폰매니저 켬)
    }
}
