using UnityEngine;

public class RoundBasic : MonoBehaviour
{
    public enum RoundState { roundStart, roundEnd, playerTurn, enemyTurn }
    public static bool isPlayerTurn;
    public RoundState roundState;
    private StateMachine stateMachine;
    public PenaltyOppBasic penaltyOppBasic;
    public GameObject ball;
    public GameObject tableauHolder;
    public GameObject player;
    public GameObject enemy;
    public GameObject goalkeeper;
    public Transform[] dots;
    public static bool isEndofRound;
    public static bool playerCanEndTurn;
    public static bool playerCanScore = true;
    public Tableau tableau;
    P_Ball p_Ball;
    [SerializeField] int maxTurns;
    Vector3 lastPos;
    public int turnNumber;


    void Start()
    {
        goalkeeper = GameObject.FindGameObjectWithTag("GK");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        stateMachine = new StateMachine();
        stateMachine.Intialize(new RoundIdle(this));
        penaltyOppBasic = enemy.GetComponent<PenaltyOppBasic>();
        tableau = tableauHolder.GetComponent<Tableau>();
        p_Ball = ball.GetComponent<P_Ball>();
        turnNumber = 0;
        Invoke("StartRound", 1f);
        tableau.turnsLeftNumber.text = maxTurns.ToString();
    }

    void StartRound()
    {
        isEndofRound = false;
        int turn = Random.Range(1, 2);
        roundState = RoundState.roundStart;
        P_Ball.onOppShooted += OppShooted;
        P_Ball.onPlayerScored += PlayerScored;
        PenaltyOppBasic.returnedBack += SwitchTurn;
        penaltyOppBasic.transform.position = dots[0].transform.position;
        penaltyOppBasic.transform.LookAt(goalkeeper.transform.position);
        switch (turn)
        {
            case 0:
                isPlayerTurn = false;
                roundState = RoundState.enemyTurn;
                stateMachine.Intialize(new RoundComputerTurn(this));
                break;
            case 1:
                roundState = RoundState.playerTurn;
                stateMachine.Intialize(new RoundPlayerTurn(this));
                isPlayerTurn = true;
                break;
        }
    }

    void EndRound()
    {
        isEndofRound = true;
        roundState = RoundState.roundEnd;
        if (tableau.playerScore > tableau.opponentScore)
        {
            tableau.instructions.text = "You win!";
        }

        else if (tableau.playerScore < tableau.opponentScore)
        {
            tableau.instructions.text = "You lose!";
        }

        else if (tableau.playerScore == tableau.opponentScore)
        {
            tableau.instructions.text = "Draw!";
        }

        stateMachine.Intialize(new RoundEnd(this));
    }

    public void SwitchTurn()
    {
        if (turnNumber < maxTurns)
        {
            turnNumber++;
            tableau.TurnMinus();
            if (roundState == RoundState.playerTurn)
            {
                isPlayerTurn = false;
                roundState = RoundState.enemyTurn;
                stateMachine.Intialize(new RoundComputerTurn(this));
            }

            else if (roundState == RoundState.enemyTurn)
            {
                roundState = RoundState.playerTurn;
                isPlayerTurn = true;
                stateMachine.Intialize(new RoundPlayerTurn(this));
            }

        }



        else
        {
            EndRound();
        }

    }

    void OppShooted(int shootResult)
    {
        penaltyOppBasic.AfterShoot(shootResult);
        if (shootResult == 0)
        {
            tableau.OppScore();
        }
        
    }

    void PlayerScored()
    {
        if (playerCanScore)
        {
            tableau.PlayerScore();
            playerCanScore = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.Update();
        if (Input.GetKeyDown(KeyCode.JoystickButton3) && playerCanEndTurn && roundState == RoundState.playerTurn)
        {
            playerCanEndTurn = false;
            playerCanScore = true;
            SwitchTurn();
        }

        lastPos = player.transform.position;

        //if (player.transform.position.x >= dots[1].transform.position.x)
        //{
        //    player.transform.position = new Vector3(dots[1].transform.position.x, player.transform.position.y, player.transform.position.z);
        //}

        //else if (player.transform.position.x <= dots[2].transform.position.x)
        //{
        //    player.transform.position = new Vector3(dots[2].transform.position.x, player.transform.position.y, player.transform.position.z);
        //}

        //else if (player.transform.position.z >= dots[1].transform.position.z)
        //{
        //    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, dots[1].transform.position.z);
        //}

        //else if (player.transform.position.z <= dots[2].transform.position.x)
        //{
        //    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, dots[2].transform.position.z);
        //}



    }

    //void FixedUpdate()
    //{
    //    if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Button3) && playerCanEndTurn && roundState == RoundState.playerTurn)
    //    {
    //        playerCanEndTurn = false;
    //        SwitchTurn();
    //    }

    //}


}
