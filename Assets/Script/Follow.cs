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
        // ĳ���� �̸� ã��
        Character = GameObject.Find("Character");
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        // ���� ���� ����
        agent.SetDestination(Character.transform.position);
    }
}
