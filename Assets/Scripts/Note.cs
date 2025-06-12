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
        if (other.CompareTag("HandCollider")) // �ָ� �ݶ��̴� Tag = "HandCollider"
        {
            // ����Ʈ ����
            GameObject effect = effectPool.GetFromPool();
            effect.transform.position = transform.position;
            effect.GetComponent<DestroyEffect>().SetPool(effectPool);

            // �ڽ� Ǯ�� ����
            pool.ReturnToPool(gameObject);
        }
    }
}
