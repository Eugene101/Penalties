using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class OppAttack : State
{
    PenaltyOppBasic _penaltyOppBasic;
    Vector3 ballDir;
    float yshift;
    float xshift;
    float xdir;
    int yrand;
    int xrand;
    bool ballFly;

    public OppAttack(PenaltyOppBasic penaltyOppBasic)
    {
        _penaltyOppBasic = penaltyOppBasic;
    }


    public override void Enter()
    {
        yrand = Random.Range(0, 2);
        xrand = Random.Range(0, 2);

        if (xrand == 1)
        {
            //xshift = _penaltyOppBasic.gatesPoints[6].transform.position.x + _penaltyOppBasic.Accuracy;
            //xdir = Random.Range(_penaltyOppBasic.gatesPoints[0].transform.position.x, xshift);
            xshift = Random.Range( 0, _penaltyOppBasic.accuraccyCoefficient/_penaltyOppBasic.Accuracy);
        }

        else if (xrand == 0)
        {
          xshift = -Random.Range(0, _penaltyOppBasic.accuraccyCoefficient / _penaltyOppBasic.Accuracy);
        }

        xdir = _penaltyOppBasic.gatesPoints[0].transform.position.x + xshift;
        //if (yrand == 0)
        //{

        //}

        //ballDir = new Vector3(xdir, 1f, _penaltyOppBasic.gatesPoints[0].transform.position.z);
        ballDir = new Vector3 (xdir, _penaltyOppBasic.gatesPoints[0].transform.position.y, _penaltyOppBasic.gatesPoints[0].transform.position.z);
        Vector3 direction = ballDir - _penaltyOppBasic.ball.transform.position;
        _penaltyOppBasic.TestSphere.transform.position = ballDir;


        // Calculate the force required to move the ball to the target point

        _penaltyOppBasic.oppStatus = PenaltyOppBasic.OppStatus.attacking;
        _penaltyOppBasic.ball.transform.parent = null;
        _penaltyOppBasic.rb.velocity = Vector3.zero;
        _penaltyOppBasic.rb.angularVelocity = Vector3.zero;
        //_penaltyOppBasic.ball.transform.LookAt(ballDir);
        _penaltyOppBasic.rb.AddForce(direction * Time.deltaTime * 9200f);
        Debug.Log(direction + " directionV");


    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        
    }
}
