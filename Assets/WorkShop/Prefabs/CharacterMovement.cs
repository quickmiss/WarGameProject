
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterMovement : MonoBehaviour
    {
        //�̵� ���� ��� �ൿ�� �����ϴ� ��ũ��Ʈ

        // ĳ���� ������Ʈ ���� ����
        [SerializeField]
        private GameObject charModel;

        // ī�޶� ������Ʈ ���� ����
        [Space(10f)]
        public Transform cameraFollow = null;
        [Range(1f, 100f)]
        public float camRotSpeed = 5f;

        // ĳ���� �̵� ���� �Ӽ�
        [Space(10f)]
        public float Speed = 1; // �ȱ� �ӵ�
        public float RunSpeedMag = 1.5f; // �޸��� �ӵ��� �ȱ� �ӵ��� ����
        public float isSpeed;  // �հ� ��� �ӵ���

        public Vector2 inputValue;  // Ű���� �Է°� ( �ִϸ��̼� ��ȯ�� ���� ���� )
        public Rigidbody rigidbody;

        private void Start()
        {
            
        }
        private void FixedUpdate()
        {
            LookAround();
            MoveControl(gameObject, charModel);
        }






        
        public void MoveControl(GameObject moveObj, GameObject charModel)
        {   // ĳ���� �̵� �Լ�
            
#if UNITY_EDITOR_WIN || UNITY_EDITOR

            rigidbody = moveObj.GetComponent<Rigidbody>();
            inputValue = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            

            // �޸���  ( Ű�� LeftShift )
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSpeed = Speed * RunSpeedMag;
            }
            else isSpeed = Speed;

            bool isMove = inputValue.magnitude != 0;

            if( isMove )    // LookAround �Լ��� �Űܾ� �ϴ���??
            {
               Vector3 lookForward = new Vector3(cameraFollow.forward.x, 0f, cameraFollow.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraFollow.right.x, 0f, cameraFollow.right.z).normalized;

                charModel.transform.forward = lookForward;
                Vector3 moveDir = lookForward * inputValue.y + lookRight * inputValue.x;

                rigidbody.velocity = moveDir * isSpeed;
            }
            
            

            
#endif        
        }

        public void LookAround()
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * camRotSpeed;
            Vector3 CamAngle = cameraFollow.rotation.eulerAngles;

            float x = CamAngle.x - mouseDelta.y;
            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
            }
            else
            {
                x = Mathf.Clamp(x, 335f, 361f);
            }
            cameraFollow.rotation = Quaternion.Euler(CamAngle.x - mouseDelta.y, CamAngle.y + mouseDelta.x, CamAngle.z);
        }
        
    }

}