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
    protected AnimationCreep animationCreep;

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
        animationCreep = GetComponent<AnimationCreep>();
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

        if (animationCreep == null)
        {
            Debug.LogWarning("ko co animation");
            return;
        }

        attackRange = enemyStatus.GetAttackRange();
        navAgent.stoppingDistance = attackRange; // Đặt khoảng cách dừng khi quái đến gần Player
        navAgent.speed = enemyStatus.GetSpeed();
        target = GameObject.FindWithTag("Player").transform;
    }

    public virtual void EnemyAction()
    {

    }

    // Gizmos để kiểm tra các khoảng cách trong Unity Editor (tùy chọn)
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Vùng an toàn
    }
}

