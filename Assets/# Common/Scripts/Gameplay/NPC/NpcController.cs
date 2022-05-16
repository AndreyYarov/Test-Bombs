using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NpcController : MonoBehaviour
{
    private Coroutine walkCoroutine;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Walk(float areaSize, float speed)
    {
        Stop();
        walkCoroutine = StartCoroutine(WalkProcess(areaSize, speed));
    }

    public void Stop()
    {
        if (walkCoroutine != null)
            StopCoroutine(walkCoroutine);
    }

    private IEnumerator WalkProcess(float areaSize, float speed)
    {
        while (!agent)
            yield return null;
        agent.speed = speed;

        if (!agent.isOnNavMesh)
        {
            Vector3 randomPoint = new Vector3(Random.Range(-areaSize, areaSize), 0f, Random.Range(-areaSize, areaSize));
            if (NavMesh.SamplePosition(randomPoint, out var hit, areaSize, agent.areaMask))
                transform.position = hit.position;
            else
                yield break;
        }

        while (true)
        {
            Vector3 randomPoint = new Vector3(Random.Range(-areaSize, areaSize), 0f, Random.Range(-areaSize, areaSize));
            if (NavMesh.SamplePosition(randomPoint, out var hit, areaSize, agent.areaMask))
            {
                agent.SetDestination(hit.position);

                do
                {
                    yield return null;
                }
                while (agent.remainingDistance > agent.stoppingDistance);
            }
        }
    }
}
