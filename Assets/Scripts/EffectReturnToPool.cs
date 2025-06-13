using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectReturnToPool : MonoBehaviour
{
    public float returnDelay = 1f; // 이펙트 길이 (초)

    private void OnEnable()
    {
        // 이펙트 재생 후 자동으로 풀로 복귀
        StartCoroutine(ReturnToPoolAfterDelay());
    }

    IEnumerator ReturnToPoolAfterDelay()
    {
        yield return new WaitForSeconds(returnDelay);
        gameObject.SetActive(false);
    }
}
