using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PenaltyOppBasic : MonoBehaviour
{
    protected float power;
    protected float accuracy;
    [SerializeField] private Animator anim;
    [SerializeField] GameObject enemyBall;
    public static bool enemyBallIsInFly;
    [SerializeField] GameObject hand;
    public Transform oppPointWaiting;
    public Transform PointOfShooting;
    private StateMachine stateMachine;
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
    public IEnumerator GoTime()
    {
        yield return new WaitForSeconds(5);
        stateMachine.ChangeState(new OppWalking(this, OppWalking.direction.forward));
    }

    private void Start()
    {
        stateMachine = new StateMachine();
        animator = GetComponent<Animator>();
        stateMachine.Intialize(new OppIdle(this));
        
    }
    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
