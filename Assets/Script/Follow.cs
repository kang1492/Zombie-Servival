using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    private GameObject Character;
    private NavMeshAgent agent;

    void Start()
    {
        // 캐릭터 이름 찾기
        Character = GameObject.Find("Character");
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        // 도착 지점 지정
        agent.SetDestination(Character.transform.position);
    }
}
