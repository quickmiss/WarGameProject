
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
        public CharacterProperty property;
      
        // ī�޶� ������Ʈ ���� ����           ���߿� ���� scriptable object �ϳ� ���� ��
        [Space(10f)]
        public Transform cameraFollow = null;
        [Range(1f, 100f)]
        public float camRotSpeed = 5f;
        public float camAngleMax = 70f;   // ���� +
        public float camAngleMin = -25f;   // �Ʒ��� -

        // ĳ���� �̵� ���� �Ӽ�
        [Space(10f)]

        public float RunSpeedMag = 1.5f; // �޸��� �ӵ��� �ȱ� �ӵ��� ����
        public float isSpeed;  // �հ� ��� �ӵ���
        // ����
        [Space(5f)]
        public bool isJump = false;
        private float jumpReTime = 1f;

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
                isSpeed = property.Speed * RunSpeedMag;
            }
            else isSpeed = property.Speed;

            bool isMove = inputValue.magnitude != 0;

            if( isMove )    // LookAround �Լ��� �Űܾ� �ϴ���??
            {
               Vector3 lookForward = new Vector3(cameraFollow.forward.x, 0f, cameraFollow.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraFollow.right.x, 0f, cameraFollow.right.z).normalized;

                charModel.transform.forward = lookForward;
                Vector3 LookDir = lookForward * inputValue.y + lookRight * inputValue.x;
                LookDir *= isSpeed;
                Vector3 moveDir = new Vector3(LookDir.x, rigidbody.velocity.y, LookDir.z);  // y���� 0���� �����ϸ� ���� AddForce�� ���� ���� 0���� �ʱ�ȭ��
                                                                                           // �׷��� y���� velocity.y�� ���� ���� �Ҵ��ϸ� ��
                
                rigidbody.velocity = moveDir;
            }



            Jump();
#endif        
        }
        public void Jump()
        {
            if (isJump) return;
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

            if (Input.GetKey(KeyCode.Space) && rigidbody)
            {
                rigidbody.AddForce(Vector3.up * property.jumpPower, ForceMode.Impulse);
                //rigidbody.velocity =  Vector3.up * jumpPower;
                this.isJump = true;
                property.HP--;
                print(property.HP);
            }
            
        }
        public void LookAround()
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * camRotSpeed;
            Vector3 CamAngle = cameraFollow.rotation.eulerAngles;

            // ------------------------------------���� ���� ----------------------------------------------
            float limitx = CamAngle.x - mouseDelta.y;
            if (limitx < 180f)
            {
                limitx = Mathf.Clamp(limitx, -1f, camAngleMax);
            }
            else
            {
                limitx = Mathf.Clamp(limitx, camAngleMin + 360, 361f);
            }

            cameraFollow.rotation = Quaternion.Euler(limitx, CamAngle.y + mouseDelta.x, CamAngle.z);
        }
        public void OnCollisionEnter(Collision collision)
        {
            string tagname = collision.transform.tag.ToString();
            if (tagname == "Ground") this.isJump = false;

        }

    }

}