using UnityEngine;

public class OppWalking : State
{
    // Start is called before the first frame update
    public enum direction { forward, backward };
    private direction _dir;
    Transform destination;
    
    PenaltyOppBasic _penaltyOppBasic;
    
    public OppWalking(PenaltyOppBasic penaltyOppBasic, direction dir)
    {
        _penaltyOppBasic = penaltyOppBasic;
        _dir = dir;
    }

    public override void Enter()
    {
        _penaltyOppBasic.oppStatus = PenaltyOppBasic.OppStatus.walking;
        _penaltyOppBasic.animator.SetInteger("EnemyBeh", 2);

        if (_dir == direction.forward)
        {
            destination = _penaltyOppBasic.PointOfShooting;
        }

        else if (_dir == direction.backward)
        {
            destination = _penaltyOppBasic.oppPointWaiting;
        }

        _penaltyOppBasic.transform.LookAt(destination);


        PenaltyOppBasic.iCanGo = true;
    }

    public override void Exit()
    {
        if (_dir == direction.forward)
        {
            _penaltyOppBasic.transform.LookAt(_penaltyOppBasic.goals.transform.position);
        }

        else if (_dir == direction.backward)
        {
            _penaltyOppBasic.transform.LookAt(_penaltyOppBasic.ball.transform.position);
        }

        _penaltyOppBasic.animator.StopPlayback();
        _penaltyOppBasic.animator.SetInteger("EnemyBeh", 0);
    }

    public override void Update()
    {
        float dist = Vector3.Distance(_penaltyOppBasic.transform.position, destination.position);

        if (PenaltyOppBasic.iCanGo)
        {
            _penaltyOppBasic.transform.position = Vector3.MoveTowards(_penaltyOppBasic.transform.position, destination.position, _penaltyOppBasic.Speed * Time.deltaTime);
            _penaltyOppBasic.animator.Play("Walking");
        }

        if (dist <= 0.1f)
        {
            PenaltyOppBasic.iCanGo = false;
            if (_dir == direction.forward)
            {
                _penaltyOppBasic.animator.StopPlayback();
                _penaltyOppBasic.Swinging();
            }

            else if (_dir == direction.backward)
            {
                _penaltyOppBasic.Idle();
                _penaltyOppBasic.LetPlayerTurn();
            }

        }





    }
}
