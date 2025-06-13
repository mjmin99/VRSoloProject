using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public float speed = 10f;
    public float missZoneZ = -2f; // 이 Z좌표를 지나가면 놓친 것으로 처리

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
        // 플레이어를 향해 등속으로 이동
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        // Miss Zone을 지나쳤다면
        if (transform.position.z < missZoneZ)
        {
            GameManager.Instance.NoteMissed(); // 게임 매니저에 알림
            gameObject.SetActive(false); // 자신을 비활성화 (풀로 돌아감)
        }
    }

    // 다른 Collider와 충돌했을 때 호출되는 함수
    void OnTriggerEnter(Collider other)
    {
        // "PlayerHand" 태그를 가진 오브젝트와 충돌했다면
        if (other.CompareTag("HandCollider"))
        {
            GameManager.Instance.NoteHit(); // 게임 매니저에 알림

            // 이펙트 생성
            ObjectPooler.Instance.SpawnFromPool("HitEffect", transform.position, Quaternion.identity);

            // 자신을 비활성화 (풀로 돌아감)
            gameObject.SetActive(false);
        }
    }
}
