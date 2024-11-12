using UnityEngine;

public class Charecter : MonoBehaviour
{
    //玩家基础速度
    public int baseSpeed;
    //玩家速度
    public int currentSpeed;
    //玩家最大血量
    public int maxHealth;
    //玩家当前血量
    public int currentHealth;

    private void Awake()
    {
        currentSpeed = baseSpeed;
        currentHealth = maxHealth;
    }
}
