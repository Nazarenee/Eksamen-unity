using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "NavigateAndSetTarget", story: "Agent navigate to [Target]", category: "Action", id: "4b7235ccc31dc0dd321e35b0892427c8")]
public partial class NavigateAndSetTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    private GameObject _self;
    private NavMeshAgent _agent;
    private float moveSpeed = 3f;  // Speed at which the enemy moves
   

    protected override Status OnStart()
    {
        _self = GameObject; // Get the enemy GameObject
        if (_self == null)
        {
            Debug.LogError("Enemy GameObject not found!");
            return Status.Failure;
        }

        _agent = _self.GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            //     Debug.LogError("NavMeshAgent component missing!");
            return Status.Failure;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Hunter");
        if (player == null)
        {
            Debug.LogError("No GameObject with tag 'Hunter' found!");
            return Status.Failure;
        }

        Debug.Log("Player is set with tag" + player.name);
        Target.Value = player;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Target == null || Target.Value == null)
        {
            Debug.LogWarning("Target is null");
            return Status.Failure;
        }
        float distance = Vector3.Distance(_self.transform.position, Target.Value.transform.position);
        float stopDistance = _agent.stoppingDistance > 0 ? _agent.stoppingDistance : 5f; // Default stopping distance
//        Debug.Log($"Distance to target: {distance}, Stopping Distance: {stopDistance}");

        if (distance <= stopDistance)
        {
            //  Debug.Log("Enemy reached stopping distance. Transitioning to attack.");
            _agent.ResetPath();
            return Status.Success;
        }

        if (_agent.isStopped)
            _agent.isStopped = false;

        _agent.SetDestination(Target.Value.transform.position);
        return Status.Running;
    }

    protected override void OnEnd()
    {
        // Optional: Handle cleanup if needed
    }
}
