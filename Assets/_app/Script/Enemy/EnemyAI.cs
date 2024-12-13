using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Thêm thư viện NavMesh để sử dụng NavMeshAgent

public class EnemyAI : MonoBehaviour
{
    protected float lastAttackTime = 0f; // Thời gian tấn công lần cuối
    protected float attackRange;

    protected Transform target; // Đối tượng Player (quái sẽ di chuyển về Player)
    protected NavMeshAgent navAgent; // Để quái di chuyển theo NavMesh
    protected Enemy enemyStatus;

    // Hàm gọi khi game bắt đầu
    public virtual void Start()
    {
        TakeInformation();
    }

    // Hàm cập nhật mỗi khung hình
    void Update()
    {
        EnemyAction();
    }

    // Hàm tấn công
    public virtual void Attack()
    {
        
    }

    public virtual void TakeInformation()
    {
        enemyStatus = gameObject.GetComponent<Enemy>();
        navAgent = GetComponent<NavMeshAgent>(); // Lấy NavMeshAgent từ quái    
        if (enemyStatus == null)
        {
            Debug.LogWarning("Ko co script enemy");
            return;
        }

        if (navAgent == null)
        {
            Debug.LogWarning("ko co navmesh agent");
            return;
        }

        attackRange = enemyStatus.GetAttackRange() - 1;
        navAgent.stoppingDistance = attackRange; // Đặt khoảng cách dừng khi quái đến gần Player
        navAgent.speed = enemyStatus.GetSpeed();
        target = GameObject.FindWithTag("Player").transform;
    }

    public virtual void EnemyAction()
    {

    }
}

