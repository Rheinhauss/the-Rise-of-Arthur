using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Enemy")]

public class EnemyPatrol : Action
{
    public SharedBool randomPatrol = false;
    public SharedTransformList waypoints;
    public SharedMoveType moveType;

    // A cache of the NavMeshAgent
    private EnemyMoveEntity moveEntity;
    // The current index that we are heading towards within the waypoints array
    private int waypointIndex;

    public override void OnAwake()
    {
        // cache for quick lookup
    }

    public override void OnStart()
    {
        moveEntity = gameObject.GetComponent<Enemy>().EnemyMoveEntity;
        if (waypoints.Value.Count == 0)
        {
            Transform[] transforms = moveEntity.WayPoints.GetComponentsInChildren<Transform>();
            foreach (Transform transform in transforms)
            {
                waypoints.Value.Add(transform);
            }
            waypoints.Value.RemoveAt(0);
        }
        // initially move towards the closest waypoint
        float distance = Mathf.Infinity;
        float localDistance;
        for (int i = 0; i < waypoints.Value.Count; ++i)
        {
            if ((localDistance = Vector3.Magnitude(transform.position - waypoints.Value[i].position)) < distance)
            {
                distance = localDistance;
                waypointIndex = i;
            }
        }

        // set the speed, angular speed, and destination then enable the agent
        moveEntity.ChangeMoveType(moveType.Value);
        moveEntity.NavMove(Target());
    }

    // Patrol around the different waypoints specified in the waypoint array. Always return a task status of running. 
    public override TaskStatus OnUpdate()
    {
        if (!moveEntity.agent.pathPending)
        {
            var thisPosition = transform.position;
            thisPosition.y = moveEntity.agent.destination.y; // ignore y
            if (Vector3.SqrMagnitude(thisPosition - moveEntity.agent.destination) < moveEntity.agent.stoppingDistance)
            {
                if (randomPatrol.Value)
                {
                    waypointIndex = Random.Range(0, waypoints.Value.Count);
                }
                else
                {
                    waypointIndex = (waypointIndex + 1) % waypoints.Value.Count;
                }
                moveEntity.NavMove(Target());
            }
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        // Disable the nav mesh
        moveEntity.agent.enabled = false;
    }

    // Return the current waypoint index position
    private Vector3 Target()
    {
        return waypoints.Value[waypointIndex].position;
    }

    // Reset the public variables
    public override void OnReset()
    {
        waypoints = null;
        randomPatrol = false;
    }

    // Draw a gizmo indicating a patrol 
    public override void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (waypoints == null)
        {
            return;
        }
        var oldColor = UnityEditor.Handles.color;
        UnityEditor.Handles.color = Color.yellow;
        for (int i = 0; i < waypoints.Value.Count; ++i)
        {
            UnityEditor.Handles.SphereHandleCap(0, waypoints.Value[i].position, waypoints.Value[i].rotation, 1, EventType.MouseDown);
        }
        UnityEditor.Handles.color = oldColor;
#endif
    }
}