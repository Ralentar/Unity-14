using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _route;

    private Transform[] _waypoints;
    private float _speed = 2;
    private int _waypointIndex;

    private void Awake()
    {
        _waypoints = new Transform[_route.childCount];

        for (int i = 0; i < _waypoints.Length; i++)
            _waypoints[i] = _route.GetChild(i);
    }

    private void Start()
    {
        TurnToTarget();
    }

    private void Update()
    {
        Vector3 targetWaypoint = _waypoints[_waypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, _speed * Time.deltaTime);

        if (transform.position == targetWaypoint)
            ChangeTarget();
    }

    private void ChangeTarget()
    {
        _waypointIndex = ++_waypointIndex % _waypoints.Length;
        TurnToTarget();
    }

    private void TurnToTarget()
    {
        transform.forward = _waypoints[_waypointIndex].position - transform.position;
    }
}