using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;
    private int damage;
    private int speed;
    private int enemyID;
    private string enemyName;
    private float attackRange; // Phạm vi tấn công của quái
    private float attackCooldown; // Thời gian chờ giữa các lần tấn công
    private string specialAbility;

    private const int BEKILLED = -1;
    private const float BEFOREDESTROY = 5f;

    private EnemyRemainText enemyRemainText;
    private AnimationCreep animationCreep;

    public void Start()
    {
        enemyRemainText = FindAnyObjectByType<EnemyRemainText>();
        animationCreep = GetComponent<AnimationCreep>();

        if (animationCreep == null)
        {
            Debug.LogWarning("ko co enemy text");
        }

        if (animationCreep == null)
        {
            Debug.LogWarning("ko co animation");
        }
    }

    //add status from database
    public void AddDataStatus(IDataReader readEnemyStatus)
    {
        enemyID = Convert.ToInt32(readEnemyStatus["enemyID"]);
        enemyName = Convert.ToString(readEnemyStatus["enemyName"]);
        health = Convert.ToInt32(readEnemyStatus["health"]);
        damage = Convert.ToInt32(readEnemyStatus["damage"]);
        speed = Convert.ToInt32(readEnemyStatus["speed"]);
        attackRange = (float)Convert.ToDouble(readEnemyStatus["attackRange"]);
        attackCooldown = (float)Convert.ToDouble(readEnemyStatus["attackCooldown"]);
        specialAbility = Convert.ToString(readEnemyStatus["specialAbility"]);
    }

    public void SetEnemyStatus(Enemy enemy)
    {
        enemyID = enemy.enemyID;
        enemyName = enemy.enemyName;
        health = enemy.health;
        damage = enemy.damage;
        speed = enemy.speed;
        attackRange = enemy.attackRange;
        attackCooldown= enemy.attackCooldown;
        specialAbility = enemy.specialAbility;
    }

    public string GetEnemyName()
    {
        return enemyName.ToLower(); 
    }

    public int GetEnemyID() => enemyID;
    public int GetDamage() => damage;
    public int GetSpeed() => speed;
    public float GetAttackRange() => attackRange;
    public float GetAttackCooldown() => attackCooldown;
    public string GetSpecialAbility() => specialAbility;

    public void TakeDamage(int healthBalance)
    {
        if (health < 0)
        {
            animationCreep.ChangeAnimation("Death");
            EnemyAI enemyAI = gameObject.GetComponent<EnemyAI>();

            if (enemyAI != null)
                enemyAI.gameObject.SetActive(false);
            Destroy(gameObject, BEFOREDESTROY);
            enemyRemainText.AddNumEnemyRemain(BEKILLED);
        }
        else { health -= healthBalance; }
    }

    public override string ToString()
    {
        return "EnemyID: " + enemyID +
                ", EnemyName: " + enemyName +
                ", Health: " + health +
                ", Damage: " + damage +
                ", Speed: " + speed +
                ", attackRange: " + attackRange +
                ", attackCooldown: " + attackCooldown +
                ", SpecialAbility: " + specialAbility;
    }
}
