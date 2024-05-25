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

            // �ִϸ����� �Ķ���� ����
            animator.SetFloat("MoveY", movement.magnitude); // �������� �ӵ��� ����
            animator.SetBool("isWalking", isMoving);

            // �̵� ���⿡ ���� ȸ��
            if (isMoving)
            {
                Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * agent.angularSpeed / 100); // ȸ�� �ӵ� ����
            }

            agent.isStopped = false; // �̵� �簳
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
            agent.isStopped = false; // �̵� �簳
        }
    }
}
