using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public int health; // 8-22
    //private RandomSpawn spawn; //8-22 ��������;
    private Animator animator; //8-22
    private GameObject Character;
    private NavMeshAgent agent;

    void Start()
    {
        // ĳ���� �̸� ã��
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
    

    void Update()
    {
        // ���� ���� ����
        agent.SetDestination(Character.transform.position);

        if (health <= 0)
        {
            // �ִϸ����� ��Ʈ�ѷ����� ���� �ִϸ������� ������ �̸��̡�close���� �� 
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    ObjectPool.instance.InsertQueue(gameObject); //8-22
                    transform.position = ObjectPool.instance.ActivePostion(); //8-22
                }
            }
            // �״� ��� , �ٷ� �޸𸮿� �ݳ��ȵǰ�. ����� �ݳ��ǰ� ��
        }
    }

    public void Death() // 8-12 �̵�
    {
        if (health <= 0)
        {
            //CancelInvoke(); //8-12
            agent.speed = 0; //8-12
            animator.Play("Death"); //8-12
        }                            //Destroy(gameObject, 3); // ���ӿ�����Ʈ �ı� 8-12
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
            animator.SetBool("Attack", false);
        }
    }
}
   

