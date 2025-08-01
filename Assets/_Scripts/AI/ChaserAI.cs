using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChaserAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] private Transform player;

	void Awake()
	{
		this.navMeshAgent = this.GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		
	}

	void Update()
    {
        this.navMeshAgent.SetDestination(this.player.position);
    }
}
