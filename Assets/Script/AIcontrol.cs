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
                                    // 0�� �Ŀ� 2�ʸ���
    }

    public void MoveNext()
    {       // 1���� ����Ʈ ������ ����
        if(agent.velocity == Vector3.zero)
        {
            agent.SetDestination(Waypoint[count++].position);
            //    ��ġ ����ֱ�

            //���� ó��
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
