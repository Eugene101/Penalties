using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundIdle : State
{
    RoundBasic _roundBasic;
    public RoundIdle(RoundBasic roundBasic)
    {
        _roundBasic = roundBasic;
    }

    public override void Enter()
    {
        Debug.Log("Code for Idle while Entering");
     }

    public override void Exit()
    {
        Debug.Log("Code for Idle while Exiting");
    }

    public override void Update()
    {
        Debug.Log("Code for Idle while Updating");
    }
}
