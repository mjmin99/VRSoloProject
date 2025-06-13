using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int combo = 0;

    public Text scoreText; // ���� ǥ�ÿ� UI �ؽ�Ʈ
    public Text comboText; // �޺� ǥ�ÿ� UI �ؽ�Ʈ
    public Text multiplierText; // ���� ǥ�ÿ� UI �ؽ�Ʈ

    private int basePoints = 100; // ��Ʈ �ϳ��� �⺻ ����

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // ���� ���� �� UI �ʱ�ȭ
        UpdateUI();
    }

    // ��Ʈ�� ���������� ���� �� ȣ��
    public void NoteHit()
    {
        combo++;

        // �޺��� ���� ���� ���� ��� (0-9: x1, 10-19: x2, 20-29: x3 ...)
        int scoreMultiplier = 1 + (combo / 10);

        score += basePoints * scoreMultiplier;

        UpdateUI();
    }

    // ��Ʈ�� ������ �� ȣ��
    public void NoteMissed()
    {
        combo = 0;
        Debug.Log("Combo Reset!");

        UpdateUI();
    }

    // UI �ؽ�Ʈ ������Ʈ
    void UpdateUI()
    {
        int scoreMultiplier = 1 + (combo / 10);

        if (scoreText != null) scoreText.text = "Score: " + score.ToString("D8"); // 8�ڸ��� ǥ��
        if (comboText != null) comboText.text = "Combo: " + combo;
        if (multiplierText != null) multiplierText.text = "x" + scoreMultiplier;

        // �޺��� 0�� ���� �޺�/���� �ؽ�Ʈ ����� (���� ����)
        if (comboText != null) comboText.gameObject.SetActive(combo > 1);
        if (multiplierText != null) multiplierText.gameObject.SetActive(scoreMultiplier > 1);
    }
}
