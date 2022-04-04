using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

public class PlayerAgent : Agent
{
    
    private GravitationalBody gb;
    private Rigidbody2D r;
    private Player p;

    [SerializeField] private ArenaBounds b;
    [SerializeField] private RoundController round;

    public bool bot = true;

    private Vector2 destinationPt;



    public float roundDuration = 5f;


    public override void Initialize()
    {
        gb = GetComponent<GravitationalBody>();
        r = GetComponent<Rigidbody2D>();
        p = GetComponent<Player>();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(destinationPt, 0.5f);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        base.OnActionReceived(actions);
        float movex = actions.ContinuousActions[0];
        float movez = actions.ContinuousActions[1];
        destinationPt = destinationPt + new Vector2(movex, movez);
        destinationPt = b.BoundPt(destinationPt);
        p.LerpToPos(destinationPt);
        
        
        if (bot)
        {
            if (round.elapsedTime < roundDuration)
            {
                AddReward(0.1f);
            }
            else
            {
                RoundSuccessfulEnd();
            }
            
        }

        
    }


    // allows human player to control agent
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousAction = actionsOut.ContinuousActions;
        continuousAction[0] = Input.GetAxisRaw("Horizontal");
        continuousAction[1] = Input.GetAxisRaw("Vertical");
    }

    public override void CollectObservations(VectorSensor sensor)
    {
     
        Vector2 p1 = gb.attractableRb[0].transform.position;
        Vector2 p2 = gb.attractableRb[1].transform.position;
    
        
        sensor.AddObservation((Vector2) transform.position);
        sensor.AddObservation(p1);
        sensor.AddObservation(p2);
        sensor.AddObservation((p1+p2) * 0.5f);

        // Total 8 observations
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log( p.gameObject.name + " failed round");
                RoundFailedEnd();
            }

            if (collision.gameObject.CompareTag("Wall"))
            {
                AddReward(-0.5f);
            }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
            if (collision.gameObject.CompareTag("Wall"))
            {
                AddReward(-0.1f);
            }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        

            if (other.CompareTag("Goal"))
            {
                Debug.Log("Scoreee");
                AddReward(1f);
            }
        
    }

    public override void OnEpisodeBegin()
    {
        
            round.StartRound();
            destinationPt = p.spawnPt;    
        
        
    }

    
    
    public void RoundSuccessfulEnd()
    {
        AddReward(5f);
        round.EndRound();
        
        StartCoroutine(round.LerpImgCol(Color.green, 0.1f));
        EndEpisode();
    }

    public void RoundFailedEnd()
    {
        AddReward(-10f);
        round.EndRound();
        
        StartCoroutine(round.LerpImgCol(Color.red, 0.1f));
        EndEpisode();
    }
}