using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppSwinging : State
{
    PenaltyOppBasic _penaltyOppBasic;
    public OppSwinging(PenaltyOppBasic penaltyOppBasic)
    {
        _penaltyOppBasic = penaltyOppBasic;
    }

    public override void Enter()
    {
        _penaltyOppBasic.oppStatus = PenaltyOppBasic.OppStatus.swinging;
        _penaltyOppBasic.animator.SetInteger("EnemyBeh", 1);
        _penaltyOppBasic.animator.Play("Attack");
        _penaltyOppBasic.rb.velocity = Vector3.zero;
        _penaltyOppBasic.rb.angularVelocity = Vector3.zero;
        _penaltyOppBasic.ball.transform.position = _penaltyOppBasic.hand.transform.position;
        //_penaltyOppBasic.ball.transform.parent = _penaltyOppBasic.hand.transform;
    }

    public override void Exit()
    {
        _penaltyOppBasic.animator.SetInteger("EnemyBeh", 0);
    }

    public override void Update()
    {
        _penaltyOppBasic.ball.transform.position = _penaltyOppBasic.hand.transform.position;
    }
}
