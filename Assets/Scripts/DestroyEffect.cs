using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    private ObjectPool pool;
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void SetPool(ObjectPool effectPool)
    {
        pool = effectPool;
        ps.Play(); // ��� ����
    }

    private void Update()
    {
        if (!ps.isPlaying)
        {
            pool.ReturnToPool(gameObject);
        }
    }
}
