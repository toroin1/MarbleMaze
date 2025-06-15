using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;
using System.Collections;


public class BoardControl : Agent
{
    [SerializeField] GameObject board;
    private RotateBoard rotateBoard;
    [SerializeField] GameObject rewardPoints;
    private RewardScript[] rewardScripts;
    [SerializeField] GameObject goal;
    private GoalReached goalReached;
    [SerializeField] Transform goalTransform;
    Rigidbody m_Marble;


    

    public override void Initialize()
    {
        m_Marble = this.GetComponent<Rigidbody>();
        rewardScripts = rewardPoints.GetComponentsInChildren<RewardScript>();
        foreach(var rewardScript in rewardScripts)
        {
            rewardScript.BoardControl = this;
        }
        rotateBoard = board.GetComponent<RotateBoard>();
        goalReached = goal.GetComponent<GoalReached>();
        goalReached.BoardControl = this;
    }

    public override void OnEpisodeBegin()
    {
        board.transform.eulerAngles = Vector3.zero;
        transform.localPosition = new Vector3(8f, 3f, 8f);
        m_Marble.linearVelocity = Vector3.zero;
        foreach (var rewardScript in rewardScripts) 
        {
            rewardScript.EnableColls();
        }
        goalReached.EnableColls();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(board.transform.localRotation.eulerAngles.x);
        sensor.AddObservation(board.transform.localRotation.eulerAngles.z);
        sensor.AddObservation(m_Marble.linearVelocity);
        sensor.AddObservation(m_Marble.linearVelocity.magnitude);
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(goalTransform.localPosition);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {

        rotateBoard.RotateBoardOnActionRecieved(actions.ContinuousActions[0], actions.ContinuousActions[1]);
        AddReward(-1f/ MaxStep);
       
    }

    public void AddRewardFromRewardPoint()
    {
        AddReward(1f);

    }

    public void GoalReached()
    {
        AddReward(20f);

        EndEpisode();

        StartCoroutine(BlinkGoalRenderer(0.5f));
    }

    IEnumerator BlinkGoalRenderer(float time)
    {
        yield return new WaitForSeconds(time);
        goalReached.EnableColls();
    }

    
}
