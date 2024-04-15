using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundEnd : State
{
    RoundBasic _roundBasic;
    public RoundEnd(RoundBasic roundBasic)
    {
        _roundBasic = roundBasic;
    }

    public override void Enter()
    {
        if (_roundBasic.tableau.playerScore > _roundBasic.tableau.opponentScore)
        {
            _roundBasic.penaltyOppBasic.AfterMatch(1);
        }

        else
        {
            _roundBasic.penaltyOppBasic.AfterMatch(0);
        }
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        Debug.Log("Code for Idle while Updating");
    }
}
