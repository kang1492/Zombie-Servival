using UnityEngine;
using UnityEngine.AI;

public class AIcontrol : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] Transform[] Waypoint;

    private int count;

    private Transform target; //8-11

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(MoveNext), 0, 2); //
        //InvokeRepeating = CancelInvoke �� ��������// 0�� �Ŀ� 2�ʸ���
    }

    public void NewTarget(Transform p_target) //8-11
    {
        CancelInvoke(nameof(MoveNext));
        target = p_target; // ���ο� Ÿ��
    }

    public void ResetTarget() //8-11
    {
        target = null;
        InvokeRepeating(nameof(MoveNext), 0, 2);
    }

    public void MoveNext()
    {
        if (target == null) //8-11
        {
            // 1���� ����Ʈ ������ ����
            if (agent.velocity == Vector3.zero)
            {
                agent.SetDestination(Waypoint[count++].position);
                //    ��ġ ����ֱ�

                //���� ó��
                if (count >= Waypoint.Length)
                {
                    count = 0;
                }
            }
        }
    }
    
    void Update() //8-11
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
              // �������� �����ϴ� ��ɾ�
        }
    }

    private void OnTriggerEnter(Collider other) //8-11
    {
        //�ε���
        if(other.CompareTag("Character"))
        {
            NewTarget(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Character"))
        {
            ResetTarget();
        }
    }
}
