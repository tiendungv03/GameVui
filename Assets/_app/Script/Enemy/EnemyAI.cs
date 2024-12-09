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
    protected Rigidbody rb; // Rigidbody để xử lý va chạm
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

    // Hàm xử lý va chạm để không cho quái đẩy Player
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Nếu có va chạm với Player
        {
            transform.Translate(Vector3.zero); // Ngừng di chuyển quái khi va chạm với Player
        }
    }

    public virtual void TakeInformation()
    {
        enemyStatus = gameObject.GetComponent<Enemy>();
        navAgent = GetComponent<NavMeshAgent>(); // Lấy NavMeshAgent từ quái
        rb = GetComponent<Rigidbody>(); // Lấy Rigidbody từ quái
        attackRange = enemyStatus.GetAttackRange();

        navAgent.speed = enemyStatus.GetSpeed();
        target = GameObject.FindWithTag("Player").transform;
        navAgent.stoppingDistance = attackRange; // Đặt khoảng cách dừng khi quái đến gần Player
    }

    public virtual void EnemyAction()
    {

    }
}

