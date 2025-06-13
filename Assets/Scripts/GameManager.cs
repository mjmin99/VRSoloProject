using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int combo = 0;

    public Text scoreText; // 점수 표시용 UI 텍스트
    public Text comboText; // 콤보 표시용 UI 텍스트
    public Text multiplierText; // 배율 표시용 UI 텍스트

    private int basePoints = 100; // 노트 하나당 기본 점수

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 게임 시작 시 UI 초기화
        UpdateUI();
    }

    // 노트를 성공적으로 쳤을 때 호출
    public void NoteHit()
    {
        combo++;

        // 콤보에 따른 점수 배율 계산 (0-9: x1, 10-19: x2, 20-29: x3 ...)
        int scoreMultiplier = 1 + (combo / 10);

        score += basePoints * scoreMultiplier;

        UpdateUI();
    }

    // 노트를 놓쳤을 때 호출
    public void NoteMissed()
    {
        combo = 0;
        Debug.Log("Combo Reset!");

        UpdateUI();
    }

    // UI 텍스트 업데이트
    void UpdateUI()
    {
        int scoreMultiplier = 1 + (combo / 10);

        if (scoreText != null) scoreText.text = "Score: " + score.ToString("D8"); // 8자리로 표시
        if (comboText != null) comboText.text = "Combo: " + combo;
        if (multiplierText != null) multiplierText.text = "x" + scoreMultiplier;

        // 콤보가 0일 때는 콤보/배율 텍스트 숨기기 (선택 사항)
        if (comboText != null) comboText.gameObject.SetActive(combo > 1);
        if (multiplierText != null) multiplierText.gameObject.SetActive(scoreMultiplier > 1);
    }
}
