using UnityEngine;
using UnityEngine.AI;

public class TraceWaypoint : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private Animator animator;
    private int currentWaypointIndex = 0;
    private bool isPlayerInRange = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            agent.isStopped = true;
            animator.SetFloat("MoveY", 0);
            animator.SetBool("isWalking", false);
        }
        else
        {
            if (agent.remainingDistance < agent.stoppingDistance && !agent.pathPending)
            {
                MoveToNextWaypoint();
            }

            Vector3 movement = agent.velocity;
            bool isMoving = movement.magnitude > 0;

            // 애니메이터 파라미터 설정
            animator.SetFloat("MoveY", movement.magnitude); // 움직임의 속도로 설정
            animator.SetBool("isWalking", isMoving);

            // 이동 방향에 따라 회전
            if (isMoving)
            {
                Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * agent.angularSpeed / 100); // 회전 속도 조정
            }

            agent.isStopped = false; // 이동 재개
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            agent.isStopped = false; // 이동 재개
        }
    }
}
