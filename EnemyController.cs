using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{

    GameObject target;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;

    private void Start()
    {
        target = GameObject.FindWithTag("Player");
        agent.updateRotation = false;
    }

    void Update()
    {
        agent.SetDestination(target.transform.position);

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }
}
