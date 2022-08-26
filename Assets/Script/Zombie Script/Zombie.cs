using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] float maxHealth; // 8-25
    public float health; // 8-22 
    //private RandomSpawn spawn; //8-22 랜덤스폰;
    private Animator animator; //8-22
    private GameObject Character;
    private NavMeshAgent agent;

    void Start()
    {
        // 캐릭터 이름 찾기
        maxHealth = health; // 체력 100 넣기 이걸 가지고 나누셈할꺼임 // 8-25
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

    /// ------------ 8-23 수정


    // 메모리 풀에서 다시 활성화 시킬때 체력과 속도를 초기화 시켜 줍니다.8-24
    private void OnEnable()
    {
        health = 100; // 새로 생성되었을때 체력 100 으로
        //agent.speed = 10; // 속도 
    }


    void Update()
    {
        //총알 맞아서 - 20 되어 체력 80 / 100 = 0.8
        float tempHealth = 1 - (health / maxHealth); // 8-25

        animator.SetLayerWeight(animator.GetLayerIndex("Other Layer"), health / maxHealth); //8-25

        DistanceSensor(); // 호출하기 8-25

        // 도착 지점 지정
        if (health <= 0)
        {
            agent.speed = 0; //8-12 //8-25 이동함
            animator.Play("Death"); //8-12

            // 애니메이터 컨트롤러에서 현재 애니메이터의 상태의 이름이“close”일 때 
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                // 현재 애니메이션의 진행도가 1보다 크거나 같다면 User Interface를 비활성화합니다.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    GameManager.instance.count++; // 8-26
                    ObjectPool.instance.InsertQueue(gameObject); //8-22
                    transform.position = ObjectPool.instance.ActivePostion(); //8-22
                }
            }
            // 죽는 모션 , 바로 메모리에 반납안되고. 모션후 반납되게 함
        }

        else // 8-25
        {
            //agent.speed = 5; // 시피드 다시 설정
            DistanceSensor(); //8-25 좀비 속도 0 만들기
            agent.SetDestination(Character.transform.position);
        }
    }

    //public void Death() // 8-12 이동 //8-25 데스 지우기
   // {
      //  if (health <= 0)
      //  {
            //CancelInvoke(); //8-12
        //    agent.speed = 0; //8-12
      //      animator.Play("Death"); //8-12
       // }                            //Destroy(gameObject, 3); // 게임오브젝트 파괴 8-12
   // }

    // 캐릭터 거리에 따라 
    public void DistanceSensor() //8-25
    {
        // 캐릭터의 위치와 자기 자신의 거리가 5보다 작다면 
        if(Vector3.Distance(Character.transform.position, transform.position) <= 2.5)
        {
            agent.speed = 0; 
            transform.LookAt(Character.transform);
            animator.SetBool("Attack", true);

            //////////////// 8-25 이동 시킴
            // 애니메이터 컨트롤러에서 현재 애니메이터의 상태의 이름이“close”일 때 // 8-24 새로 만듬
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                // 현재 애니메이션의 진행도가 1보다 크거나 같다면 User Interface를 비활성화합니다.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    // other 였다가 Character 로 바낌 8-25
                    Character.GetComponent<Control>().health -= 10;             

                    Character.GetComponent<Control>().ScreenCall(); // 피 튀기기 8-26

                    animator.Rebind();           // 에니메이션 초기화
                    // 초기화 안하면 프로그매머가 거기 있어서 충동때 마다 데미지 들어감
                }
            }
            ////////////////
        }

        else // 캐릭터의 위치와 자기 자신의거리가 5보다 멀어졌다면      
        {
            agent.speed = 3.5f;   
            animator.SetBool("Attack", false);
        }
    }

    // 게임 오브젝트와 충돌 중 일때 호출되는 함수
    private void OnTriggerStay(Collider other) // 8-23
    {
        if(other.CompareTag("Character")) // 캐릭터만 > 장애물 예외처리
        {
            agent.speed = 0; // 캐릭터와 충돌 햇을때 속도 0
            Debug.Log("충돌 중"); // 의심되는 코드 테스트 하기

            transform.LookAt(Character.transform);// 캐릭터 바라보면서

            // 공격할러고 할때 어택 에니메이션 실행
            animator.SetBool("Attack", true);
        }
        
    }

    // 게임 오브젝트와 충돌이 벗어났을 때 호출되는 함수
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            agent.speed = 3.5f; // 충돌 벗어 날때 속도 3.5 조정

            transform.LookAt(Character.transform);// 캐릭터 바라보면서

            animator.SetBool("Attack", false);

            // 애니메이터 컨트롤러에서 현재 애니메이터의 상태의 이름이“close”일 때 // 8-24 새로 만듬
            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            //{
                // 현재 애니메이션의 진행도가 1보다 크거나 같다면 User Interface를 비활성화합니다.
            //    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            //    {
            //        other.GetComponent<Control>().health -= 10;
            //        animator.Rebind();           // 에니메이션 초기화
                    // 초기화 안하면 프로그매머가 거기 있어서 충동때 마다 데미지 들어감
            //    }
            //}
            // 죽는 모션 , 바로 메모리에 반납안되고. 모션후 반납되게 함


        }
    }
}
   

