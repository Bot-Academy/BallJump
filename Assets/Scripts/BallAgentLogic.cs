using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class BallAgentLogic : Agent
{

    Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public Transform target;
    public override void OnEpisodeBegin()
    {
        // Reset agent
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(-9, 0.5f, 0);

        // Move target to a new spot
        target.localPosition = new Vector3(12 + Random.value * 8, Random.value * 3, Random.value * 10 - 5);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions & Agent velocity
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(rBody.velocity);
    }


    public float speed = 20;
    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];

        if (vectorAction[1] == 2)
        {
            controlSignal.z = 1;
        }
        else
        {
            controlSignal.z = -vectorAction[1];
        }

        // Prevent adding forces after jumping
        if (this.transform.localPosition.x < 8.5)
        {
            rBody.AddForce(controlSignal * speed);
        }

        float distanceToTarget = Vector3.Distance(this.transform.localPosition, target.localPosition);
        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Fell of platform
        if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Vertical");
        actionsOut[1] = Input.GetAxis("Horizontal");
    }

}

