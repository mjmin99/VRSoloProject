using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public float speed = 10f;
    public float missZoneZ = -2f; // �� Z��ǥ�� �������� ��ģ ������ ó��

    void OnEnable()
    {
        Debug.Log($"[OnEnable] {gameObject.name} position: {transform.position}");
    }

    void Start()
    {
        Debug.Log($"[Start] {gameObject.name} position: {transform.position}");
    }

    void Update()
    {
        Debug.Log($"[Update] {gameObject.name} position: {transform.position}");
        // �÷��̾ ���� ������� �̵�
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        // Miss Zone�� �����ƴٸ�
        if (transform.position.z < missZoneZ)
        {
            GameManager.Instance.NoteMissed(); // ���� �Ŵ����� �˸�
            gameObject.SetActive(false); // �ڽ��� ��Ȱ��ȭ (Ǯ�� ���ư�)
        }
    }

    // �ٸ� Collider�� �浹���� �� ȣ��Ǵ� �Լ�
    void OnTriggerEnter(Collider other)
    {
        // "PlayerHand" �±׸� ���� ������Ʈ�� �浹�ߴٸ�
        if (other.CompareTag("HandCollider"))
        {
            GameManager.Instance.NoteHit(); // ���� �Ŵ����� �˸�

            // ����Ʈ ����
            ObjectPooler.Instance.SpawnFromPool("HitEffect", transform.position, Quaternion.identity);

            // �ڽ��� ��Ȱ��ȭ (Ǯ�� ���ư�)
            gameObject.SetActive(false);
        }
    }
}
