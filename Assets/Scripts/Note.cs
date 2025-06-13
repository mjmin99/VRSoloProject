using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private ObjectPool pool;
    public float lifeTime = 5f;

    public void Init(ObjectPool poolRef)
    {
        pool = poolRef;
        StopAllCoroutines();
        StartCoroutine(ReturnToPoolAfterTime());
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        pool.ReturnObject(gameObject);
    }
}
