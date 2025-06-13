using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag; // 오브젝트 태그
        public GameObject prefab; 
        public int size; // 처음 생성할 오브젝트 갯수
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // 모든 풀을 순회하며 오브젝트를 생성하고 딕셔너리에 추가
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false); // 비활성화 상태로 생성
                obj.transform.SetParent(this.transform); // 풀러 오브젝트의 자식으로 정리
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // 큐에서 오브젝트를 하나 꺼냄
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // **중요**: 오브젝트를 다시 큐에 넣어 재사용 준비
        // 이 방식은 항상 같은 수의 오브젝트만 사용하게 됨.
        // 만약 풀 크기보다 더 많은 오브젝트가 필요하다면, 큐에 넣기 전 새 오브젝트를 생성하는 로직이 필요.
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
