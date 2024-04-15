using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PenaltyOppBasic : MonoBehaviour
{
    [SerializeField] float power;
    [SerializeField] float accuracy;
    public enum OppStatus { idle, attacking, walking, swinging }
    public OppStatus oppStatus;
    [SerializeField] private Animator anim;
    public GameObject ball;
    public static bool enemyScored;
    public GameObject hand;
    public Transform oppPointWaiting;
    public Transform PointOfShooting;
    public GameObject goals;
    public static bool iCanGo;
    bool iShooted;
    private StateMachine stateMachine;
    [SerializeField] private float speed;
    public float Speed { get { return speed; } }
    public Transform postDot;
    bool shoot;
    public Rigidbody rb;
    ////////////
    public Transform[] gatesPoints;
    public float accuraccyCoefficient;
    public GameObject TestSphere;
    public static UnityAction returnedBack;
    float yposition;

    public Animator animator
    {
        get
        {
            return anim;
        }
        private set { anim = value; }
    }

    public float Power
    {
        get { return power; }
    }

    public float Accuracy
    {
        get { return accuracy; }
    }
    public PenaltyOppBasic(float _power, float _accuracy)
    {
        power = _power;
        accuracy = _accuracy;
    }

    private void Start()
    {
        stateMachine = new StateMachine();
        animator = GetComponent<Animator>();
        stateMachine.Intialize(new OppIdle(this));
        rb = ball.GetComponent<Rigidbody>();
        yposition = transform.position.y;

    }

    public void Idle()
    {
        //  yield return new WaitForSeconds(5);
        stateMachine.ChangeState(new OppIdle(this));
        //Invoke("Attack", 3f);
    }

    public void Moving(int direction)
    {
        OppWalking.direction direction1 = new OppWalking.direction();
        switch (direction)
        {
            case 0:
                direction1 = OppWalking.direction.forward;
                break;
            case 1:
                direction1 = OppWalking.direction.backward;
                break;
        }
        //  yield return new WaitForSeconds(5);
        stateMachine.ChangeState(new OppWalking(this, direction1));
        //Invoke("Attack", 3f);
    }

    public void Swinging()
    {
        stateMachine.ChangeState(new OppSwinging(this));
        Invoke("ShootBall", 0.8f);
    }

    public void ShootBall()
    {
        stateMachine.ChangeState(new OppAttack(this));
        Invoke("GkNeedToJumpOpp", 0.5f);
        //Invoke("EndTurnLose", 2f);

    }

    void EndTurnLose()
    {
        if (!enemyScored)
        {
            AfterShoot(1);
        }
    }


    void GkNeedToJumpOpp()
    {
        P_Ball.onballReleased?.Invoke();
    }


    public void AfterShoot(int mood)
    {
        OppReaction.reaction reaction1 = new OppReaction.reaction();
        switch (mood)
        {
            case 0:
                reaction1 = OppReaction.reaction.happy;
                break;
            case 1:
                reaction1 = OppReaction.reaction.angry;
                break;
        }
        //  yield return new WaitForSeconds(5);
        stateMachine.ChangeState(new OppReaction(this, reaction1));
        Invoke("Moveback", 3f);

    }

    public void AfterMatch(int matchMood)
    {
        OppReaction.reaction reaction1 = new OppReaction.reaction();
        switch (matchMood)
        {
            case 0:
                reaction1 = OppReaction.reaction.happy;
                break;
            case 1:
                reaction1 = OppReaction.reaction.angry;
                break;
        }
        //  yield return new WaitForSeconds(5);
        stateMachine.ChangeState(new OppReaction(this, reaction1));
    }

    void Moveback()
    {
        Moving(1);
    }

    public void LetPlayerTurn()
    {
        returnedBack?.Invoke();
        Debug.Log("Invoke Entering");
    }


    private void Update()
    {
        stateMachine.currentState.Update();
        //if (oppStatus == OppStatus.swinging && !iShooted)
        //{
        //    Invoke("ShootBall", 0.8f);
        //    iShooted = true;
        //}

        //float dist = Vector3.Distance(ball.transform.position, postDot.transform.position);
        //if (dist<0.2&& shoot)
        //{
        //    rb.velocity = ((postDot.transform.position - ball.transform.position)*10000f);
        //}

        //transform.position = new Vector3(transform.position.x, yposition, transform.position.z);

    }
}
