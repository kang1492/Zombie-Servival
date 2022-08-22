using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public int health; // 8-22
    //private RandomSpawn spawn; //8-22 랜덤스폰;
    private Animator animator; //8-22
    private GameObject Character;
    private NavMeshAgent agent;

    void Start()
    {
        // 캐릭터 이름 찾기
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Character = GameObject.Find("Character"); //8-22
        //spawn = GameObject.Find("Random Spawn").GetComponent<RandomSpawn>();// 8-22
            // 게임 왼쪽 위에 
    }

    // 게임 오브젝트가 비활성화되었을 때 호출되는 함수입니다.
    //private void OnEnable() // 8-22
    //{
        // 위치값 다시 잡기
        //transform.position = spawn.RandomPosition();
        
    //} // 계속 걸려서 에러


    void Update()
    {
        // 도착 지점 지정
        agent.SetDestination(Character.transform.position);
    }

    public void Death() // 8-12 이동
    {
        if (health <= 0)
        {
            //CancelInvoke(); //8-12
            agent.speed = 0; //8-12

            animator.Play("Death"); //8-12
                                    //Destroy(gameObject, 3); // 게임오브젝트 파괴 8-12

            transform.position = ObjectPool.instance.ActivePostion(); //8-22

            // 애니메이터 컨트롤러에서 현재 애니메이터의 상태의 이름이“close”일 때 
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                // 현재 애니메이션의 진행도가 1보다 크거나 같다면 User Interface를 비활성화합니다.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    ObjectPool.instance.InsertQueue(gameObject); //8-22
                }
            }
            // 죽는 모션 , 바로 메모리에 반납안되고. 모션후 반납되게 함

            
            
        }
    }
}
