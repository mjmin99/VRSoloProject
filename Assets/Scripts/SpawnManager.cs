using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public ObjectPool notePool;
    public ObjectPool effectPool;
    public Transform spawnPoint;
    public float spawnInterval = 2f;
    public float noteSpeed = 5f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnNote), 2f, spawnInterval);
    }

    void SpawnNote()
    {
        GameObject note = notePool.GetFromPool();
        note.transform.position = spawnPoint.position;
        note.transform.rotation = Quaternion.identity;

        Rigidbody rb = note.GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * noteSpeed;

        // Pool 정보 넘기기
        note.GetComponent<Note>().SetPool(notePool, effectPool);
    }
}