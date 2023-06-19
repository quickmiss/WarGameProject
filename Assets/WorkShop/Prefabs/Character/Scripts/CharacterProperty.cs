using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProperty", menuName = "Scriptable Object Asset/CharacterProperty")]
public class CharacterProperty : ScriptableObject
{
    public int MaxHP = 10;
    public int HP = 10;
    public int hungerGage = 10;         // 배고픔
    public int bodytemperature = 36;    // 체온
    public float Speed = 2f;            // 이동 속도
    public float jumpPower = 3f;        // 점프력
    public int atk = 1;                 // 힘
    public int def = 0;                 // 방어력
    public int inventoryNum = 10;       // 가방 수납용량
    public float ActPoint = 1f;         // 행동력 ( 특정 행동이 빨라짐 [ 예: 파밍 ] )
}
