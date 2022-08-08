using UnityEngine;
using UnityEngine.AI;

public class AIcontrol : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] Transform[] Waypoint;

    private int count;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(MoveNext), 0, 2);
                                    // 0초 후에 2초마다
    }

    public void MoveNext()
    {       // 1번재 포인트 도착시 실행
        if(agent.velocity == Vector3.zero)
        {
            agent.SetDestination(Waypoint[count++].position);
            //    위치 잡아주기

            //예외 처리
            if(count >= Waypoint.Length)
            {
                count = 0;
            }
        }
    }
    
    void Update()
    {
        
    }
}
