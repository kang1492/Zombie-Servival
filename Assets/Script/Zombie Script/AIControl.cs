using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    private Zombie zombie; //8-22
    private NavMeshAgent agent;

    [SerializeField] Transform[] Waypoint;

    private int count;
    //public int health = 100; // 기본 체력 //8-12

    private Animator animator; //8-12
    private Transform target; //8-11

    void Start()
    {
        zombie = GetComponent<Zombie>(); //8-22 순찰 좀비 죽이면 에러 뜨는것
        animator = GetComponent<Animator>(); //8-12
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(MoveNext), 0, 2); //
        //InvokeRepeating = CancelInvoke 로 실행중지// 0초 후에 2초마다
    }

    public void NewTarget(Transform p_target) //8-11
    {
        CancelInvoke(nameof(MoveNext));
        target = p_target; // 새로운 타켓
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
            // 1번재 포인트 도착시 실행
            if (agent.velocity == Vector3.zero)
            {
                agent.SetDestination(Waypoint[count++].position);
                //    위치 잡아주기

                //예외 처리
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
                // 방향으로 이전하는 명령어
            }
        }

        

        
    }

    


    private void OnTriggerEnter(Collider other) //8-11
    {
        //부딧힌
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
