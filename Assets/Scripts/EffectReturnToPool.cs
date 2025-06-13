using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectReturnToPool : MonoBehaviour
{
    public float returnDelay = 1f; // ����Ʈ ���� (��)

    private void OnEnable()
    {
        // ����Ʈ ��� �� �ڵ����� Ǯ�� ����
        StartCoroutine(ReturnToPoolAfterDelay());
    }

    IEnumerator ReturnToPoolAfterDelay()
    {
        yield return new WaitForSeconds(returnDelay);
        gameObject.SetActive(false);
    }
}
