using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private ObjectPool pool;
    private ObjectPool effectPool;

    public void SetPool(ObjectPool notePool, ObjectPool destroyEffectPool)
    {
        pool = notePool;
        effectPool = destroyEffectPool;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandCollider")) // 주먹 콜라이더 Tag = "HandCollider"
        {
            // 이펙트 생성
            GameObject effect = effectPool.GetFromPool();
            effect.transform.position = transform.position;
            effect.GetComponent<DestroyEffect>().SetPool(effectPool);

            // 자신 풀로 복귀
            pool.ReturnToPool(gameObject);
        }
    }
}
