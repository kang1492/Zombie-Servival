using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] float maxHealth; // 8-25
    public float health; // 8-22 
    //private RandomSpawn spawn; //8-22 ��������;
    private Animator animator; //8-22
    private GameObject Character;
    private NavMeshAgent agent;

    void Start()
    {
        // ĳ���� �̸� ã��
        maxHealth = health; // ü�� 100 �ֱ� �̰� ������ �������Ҳ��� // 8-25
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Character = GameObject.Find("Character"); //8-22
                                                  //spawn = GameObject.Find("Random Spawn").GetComponent<RandomSpawn>();// 8-22
                                                  // ���� ���� ���� 
    }

    // ���� ������Ʈ�� ��Ȱ��ȭ�Ǿ��� �� ȣ��Ǵ� �Լ��Դϴ�.
    //private void OnEnable() // 8-22
    //{
    // ��ġ�� �ٽ� ���
    //transform.position = spawn.RandomPosition();

    //} // ��� �ɷ��� ����

    /// ------------ 8-23 ����


    // �޸� Ǯ���� �ٽ� Ȱ��ȭ ��ų�� ü�°� �ӵ��� �ʱ�ȭ ���� �ݴϴ�.8-24
    private void OnEnable()
    {
        health = 100; // ���� �����Ǿ����� ü�� 100 ����
        //agent.speed = 10; // �ӵ� 
    }


    void Update()
    {
        //�Ѿ� �¾Ƽ� - 20 �Ǿ� ü�� 80 / 100 = 0.8
        float tempHealth = 1 - (health / maxHealth); // 8-25

        animator.SetLayerWeight(animator.GetLayerIndex("Other Layer"), health / maxHealth); //8-25

        DistanceSensor(); // ȣ���ϱ� 8-25

        // ���� ���� ����
        if (health <= 0)
        {
            agent.speed = 0; //8-12 //8-25 �̵���
            animator.Play("Death"); //8-12

            // �ִϸ����� ��Ʈ�ѷ����� ���� �ִϸ������� ������ �̸��̡�close���� �� 
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    GameManager.instance.count++; // 8-26
                    ObjectPool.instance.InsertQueue(gameObject); //8-22
                    transform.position = ObjectPool.instance.ActivePostion(); //8-22
                }
            }
            // �״� ��� , �ٷ� �޸𸮿� �ݳ��ȵǰ�. ����� �ݳ��ǰ� ��
        }

        else // 8-25
        {
            //agent.speed = 5; // ���ǵ� �ٽ� ����
            DistanceSensor(); //8-25 ���� �ӵ� 0 �����
            agent.SetDestination(Character.transform.position);
        }
    }

    //public void Death() // 8-12 �̵� //8-25 ���� �����
   // {
      //  if (health <= 0)
      //  {
            //CancelInvoke(); //8-12
        //    agent.speed = 0; //8-12
      //      animator.Play("Death"); //8-12
       // }                            //Destroy(gameObject, 3); // ���ӿ�����Ʈ �ı� 8-12
   // }

    // ĳ���� �Ÿ��� ���� 
    public void DistanceSensor() //8-25
    {
        // ĳ������ ��ġ�� �ڱ� �ڽ��� �Ÿ��� 5���� �۴ٸ� 
        if(Vector3.Distance(Character.transform.position, transform.position) <= 2.5)
        {
            agent.speed = 0; 
            transform.LookAt(Character.transform);
            animator.SetBool("Attack", true);

            //////////////// 8-25 �̵� ��Ŵ
            // �ִϸ����� ��Ʈ�ѷ����� ���� �ִϸ������� ������ �̸��̡�close���� �� // 8-24 ���� ����
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    // other ���ٰ� Character �� �ٳ� 8-25
                    Character.GetComponent<Control>().health -= 10;             

                    Character.GetComponent<Control>().ScreenCall(); // �� Ƣ��� 8-26

                    animator.Rebind();           // ���ϸ��̼� �ʱ�ȭ
                    // �ʱ�ȭ ���ϸ� ���α׸ŸӰ� �ű� �־ �浿�� ���� ������ ��
                }
            }
            ////////////////
        }

        else // ĳ������ ��ġ�� �ڱ� �ڽ��ǰŸ��� 5���� �־����ٸ�      
        {
            agent.speed = 3.5f;   
            animator.SetBool("Attack", false);
        }
    }

    // ���� ������Ʈ�� �浹 �� �϶� ȣ��Ǵ� �Լ�
    private void OnTriggerStay(Collider other) // 8-23
    {
        if(other.CompareTag("Character")) // ĳ���͸� > ��ֹ� ����ó��
        {
            agent.speed = 0; // ĳ���Ϳ� �浹 ������ �ӵ� 0
            Debug.Log("�浹 ��"); // �ǽɵǴ� �ڵ� �׽�Ʈ �ϱ�

            transform.LookAt(Character.transform);// ĳ���� �ٶ󺸸鼭

            // �����ҷ��� �Ҷ� ���� ���ϸ��̼� ����
            animator.SetBool("Attack", true);
        }
        
    }

    // ���� ������Ʈ�� �浹�� ����� �� ȣ��Ǵ� �Լ�
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            agent.speed = 3.5f; // �浹 ���� ���� �ӵ� 3.5 ����

            transform.LookAt(Character.transform);// ĳ���� �ٶ󺸸鼭

            animator.SetBool("Attack", false);

            // �ִϸ����� ��Ʈ�ѷ����� ���� �ִϸ������� ������ �̸��̡�close���� �� // 8-24 ���� ����
            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            //{
                // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
            //    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            //    {
            //        other.GetComponent<Control>().health -= 10;
            //        animator.Rebind();           // ���ϸ��̼� �ʱ�ȭ
                    // �ʱ�ȭ ���ϸ� ���α׸ŸӰ� �ű� �־ �浿�� ���� ������ ��
            //    }
            //}
            // �״� ��� , �ٷ� �޸𸮿� �ݳ��ȵǰ�. ����� �ݳ��ǰ� ��


        }
    }
}
   

