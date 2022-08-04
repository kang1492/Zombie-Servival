using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] float axisSpeed = 5.0f; // ī�޶� x��� y���� ȸ�� �ӵ�
    [SerializeField] GameObject eye;


    private float eulerAngleX;
    private float eulerAngleY;

    private CharacterController characterControl;
    private Vector3 moveForce;
    [SerializeField] float speed;
    [SerializeField] float gravity = 20.0f;

    void Start()
    {
        characterControl = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        UpdateRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MoveTo
            (
                 new Vector3
                 (
                     Input.GetAxis("Horizontal"), 
                     0, 
                     Input.GetAxis("Vertical")
                  )
             );

        // �ٴڰ� �⵹���� �ʾҴٸ�
        if(characterControl.isGrounded == false)
        {
            moveForce.y -= gravity * Time.deltaTime;
        }
        else
        {
            moveForce.y = 0.1f;
        }

        characterControl.Move(moveForce * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        // ī�޶� ȸ������ ���� ������ ���ϱ� ������ ȸ�� ���� ���ؼ� �����մϴ�.
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        // ��/ �Ʒ��� �ٶ󺸰� �̵��� �� ĳ���� ������Ʈ�� �������� �̵��ϰų� �Ʒ��� ����ɱ� ������
        // direction�� �״�� ������� �ʰ�, moveForce ������ x�� z���� �־ ����մϴ�.
        moveForce = new Vector3(direction.x * speed, moveForce.y, direction.z * speed);
    }

    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * axisSpeed; // ���콺 ��/�� �̵����� ī�޶� y�� ȸ��

        // ���콺 �Ʒ��� ������ -�� �����δ� ������Ʈ�� x���� + �������� ȸ���ؾ� �Ʒ��� ���� �����Դϴ�.
        eulerAngleX -= mouseY * axisSpeed; // ���콺 ��/�Ʒ� �̵����� ī�޶� x�� ȸ��

        // ī�޶� x�� ȸ���� ��� ���� ������ �����մϴ�. ���� 360 ȸ����. �Ʒ� ���� ���� ����.
        eulerAngleX = ClampAngle(eulerAngleX, -80, 50);

        //transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
        transform.rotation = Quaternion.Euler(transform.rotation.x, eulerAngleY, 0); // ������ ó��

        eye.transform.rotation = Quaternion.Euler(eulerAngleX, transform.eulerAngles.y, 0);
    }

    public float ClampAngle(float angle, float min, float max)
    {// float ��ȯ�� �ʿ�
        return Mathf.Clamp(angle, min, max);
    }
}