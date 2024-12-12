using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public CoinSpawner coinSpawner;
    public GameController gameController;

    private void Start()
    {
        agent.updateRotation = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           Ray ray = cam.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;

           if (Physics.Raycast(ray, out hit))
           {
                agent.SetDestination(hit.point);
           }
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        } else
        {
            character.Move(Vector3.zero, false, false);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Coin")
        {
            Destroy(col.gameObject);
            gameController.score++;
            coinSpawner.SpawnCoin();
        }
        if(col.tag == "Enemy")
        {
            gameController.Dead();
        }
    }
}
