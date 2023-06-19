using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProperty", menuName = "Scriptable Object Asset/CharacterProperty")]
public class CharacterProperty : ScriptableObject
{
    public int MaxHP = 10;
    public int HP = 10;
    public int hungerGage = 10;         // �����
    public int bodytemperature = 36;    // ü��
    public float Speed = 2f;            // �̵� �ӵ�
    public float jumpPower = 3f;        // ������
    public int atk = 1;                 // ��
    public int def = 0;                 // ����
    public int inventoryNum = 10;       // ���� �����뷮
    public float ActPoint = 1f;         // �ൿ�� ( Ư�� �ൿ�� ������ [ ��: �Ĺ� ] )
}
