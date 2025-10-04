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

	void Update()
    {
		if (this.player != null)
			this.navMeshAgent.SetDestination(this.player.position);
		else
			this.navMeshAgent.SetDestination(this.transform.position);
	}

	public void SetTarget(Transform playerTransform = null)
	{
		this.player = playerTransform;
	}
}
