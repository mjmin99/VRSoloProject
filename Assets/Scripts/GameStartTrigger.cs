using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameStartTrigger : MonoBehaviour

{
    public NoteSpawner spawnManager;  // �����Ŵ��� ����
    public Text startText;  // Text TMP ����

    private void Start()
    {
        spawnManager.enabled = false; // �ϴ� ��Ʈ ���� ����
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

        startText.text = ""; // �ؽ�Ʈ ����
        spawnManager.enabled = true; // ���� ���� (�����Ŵ��� ��)
    }
}
