using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundPlayerTurn : State
{
    RoundBasic _roundBasic;
    public RoundPlayerTurn(RoundBasic roundBasic)
    {
        _roundBasic = roundBasic;
    }

    public override void Enter()
    {
        _roundBasic.tableau.instructions.text = "Press A to install the ball";

    }

    public override void Exit()
    {

    }

    public override void Update()
    {

    }
}
