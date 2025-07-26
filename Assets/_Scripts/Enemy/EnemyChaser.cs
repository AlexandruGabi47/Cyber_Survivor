using UnityEngine;
using UnityEngine.AI;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;

    void Update()
    {
        agent.SetDestination(player.position);
    }
}
