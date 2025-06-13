using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag; // ������Ʈ �±�
        public GameObject prefab; 
        public int size; // ó�� ������ ������Ʈ ����
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

        // ��� Ǯ�� ��ȸ�ϸ� ������Ʈ�� �����ϰ� ��ųʸ��� �߰�
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false); // ��Ȱ��ȭ ���·� ����
                obj.transform.SetParent(this.transform); // Ǯ�� ������Ʈ�� �ڽ����� ����
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

        // ť���� ������Ʈ�� �ϳ� ����
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // **�߿�**: ������Ʈ�� �ٽ� ť�� �־� ���� �غ�
        // �� ����� �׻� ���� ���� ������Ʈ�� ����ϰ� ��.
        // ���� Ǯ ũ�⺸�� �� ���� ������Ʈ�� �ʿ��ϴٸ�, ť�� �ֱ� �� �� ������Ʈ�� �����ϴ� ������ �ʿ�.
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
