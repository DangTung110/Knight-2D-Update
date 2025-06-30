using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyInfo : ScriptableObject
{
    public new string name;
    public float dame;
    public float hp;
    public float speed;
}
