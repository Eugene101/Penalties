using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppReaction : State
{
    PenaltyOppBasic _penaltyOppBasic;

    public enum reaction { happy, angry };
    private reaction _rea;
    public OppReaction(PenaltyOppBasic penaltyOppBasic, reaction rea)
    {
        _penaltyOppBasic = penaltyOppBasic;
        _rea = rea;
    }

    public override void Enter()
    {
        if (_rea == reaction.happy)
        {
            _penaltyOppBasic.animator.SetInteger("EnemyBeh", 3);
            _penaltyOppBasic.animator.Play("Scored");
        }

        if (_rea == reaction.angry)
        {
            _penaltyOppBasic.animator.SetInteger("EnemyBeh", 4);
            _penaltyOppBasic.animator.Play("Missed");
        }
    }

    public override void Exit()
    {
        _penaltyOppBasic.animator.StopPlayback();
        _penaltyOppBasic.animator.SetInteger("EnemyBeh", 0);
    }

    public override void Update()
    {

    }
}
