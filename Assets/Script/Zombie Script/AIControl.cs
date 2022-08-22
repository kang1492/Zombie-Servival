using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    private Zombie zombie; //8-22
    private NavMeshAgent agent;

    [SerializeField] Transform[] Waypoint;

    private int count;
    //public int health = 100; // �⺻ ü�� //8-12

    private Animator animator; //8-12
    private Transform target; //8-11

    void Start()
    {
        zombie = GetComponent<Zombie>(); //8-22 ���� ���� ���̸� ���� �ߴ°�
        animator = GetComponent<Animator>(); //8-12
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
        if(zombie.health <= 0) //8-22
        {
            //Destroy(gameObject);
            CancelInvoke(nameof(MoveNext)); //8-22
        }

        else
        {
            if(target != null)
            {
                agent.SetDestination(target.position);
                // �������� �����ϴ� ��ɾ�
            }
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
