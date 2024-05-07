using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class FSM_AIController : MonoBehaviour
{
    public enum States
    {
        Wait,
        Patrol,
        Pursue,
        Search
    }

    private States currentState;

    [Header("State: Wait")]
    public float waitTime = 2f;
    private float waitingTime;

    [Header("State: Patrol")]
    public Transform waypoint1;
    public Transform waypoint2;
    private Transform currentWaypoint;
    public float waypointMinimumDistance = 2f;
    private float currentWaypointDistance;

    [Header("State: Pursue")]
    public float fieldOfVision = 5f;
    private float playerDistance;
    private GameObject player;

    [Header("State: Search")]
    public float searchLimitTime = 6f;
    private float lostPlayerSight;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        currentWaypoint = waypoint1;

        Wait();
    }

    private void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        if(currentState != States.Pursue && CanSeePlayer())
        {
            Pursue();

            return;
        }

        switch (currentState)
        {
            case States.Wait:
                if(WaitedTimeEnough())
                {
                    Patrol();
                }
                else
                {
                    target = transform;
                }
                break;

            case States.Patrol:
                if(NearCurrentWaypoint())
                {
                    Wait();

                    ChangeWaypoint();
                }
                else
                {
                    target = currentWaypoint;
                }
                break;

            case States.Pursue:
                if(!CanSeePlayer())
                {
                    Search();
                }
                else
                {
                    target = player.transform;
                }

                break;

            case States.Search:
                if(LostPlayerTimeLimit())
                {
                    Wait();
                }
                break;

            default:
                break;
        }

        aiCharacterControl.target = target;

    }

    

    #region STATES

    private void Wait()
    {
        currentState = States.Wait;
        waitingTime = Time.time;
    }

    private void Patrol()
    {
        currentState = States.Patrol;
    }

    private void Pursue()
    {
        currentState = States.Pursue;
    }

    private void Search()
    {
        currentState = States.Search;

        lostPlayerSight = Time.time;

        target = null;
    }
    #endregion

    #region WAIT STATE METHODS
    
    private bool WaitedTimeEnough()
    {
        return waitingTime + waitTime <= Time.time;
    }
    #endregion

    #region PATROL STATE METHODS

    private bool NearCurrentWaypoint()
    {
        currentWaypointDistance = Vector3.Distance(transform.position, currentWaypoint.position);
        return currentWaypointDistance <= waypointMinimumDistance;
    }

    private void ChangeWaypoint()
    {
        currentWaypoint = (currentWaypoint == waypoint1) ? waypoint2 : waypoint1;
    }

    #endregion

    #region PURSUE STATE METHODS

    private bool CanSeePlayer()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        return playerDistance <= fieldOfVision;
    }

    #endregion

    #region SEARCH STATE METHODS

    private bool LostPlayerTimeLimit()
    {
        return lostPlayerSight + searchLimitTime <= Time.time;
    }

    #endregion

    #region AI MOVEMENT

    private AICharacterControl aiCharacterControl;
    private Transform target;

    private void Awake()
    {
        aiCharacterControl = GetComponent<AICharacterControl>();
    }


    #endregion

    
}
