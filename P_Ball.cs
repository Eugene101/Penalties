using UnityEngine;
using UnityEngine.Events;
using UltimateXR.Core.Components.Composite;
using UltimateXR.Manipulation;
using UnityEngine.XR;

public class P_Ball : MonoBehaviour
{
    public static bool isNearOpponent;
    Rigidbody rb;
    Tableau tableau;
    bool playerCanScore = true;
    bool canInvokeJumping = true;
    public Vector3 LeftControllerVelocity;
    public Vector3 RightControllerVelocity;
    RoundBasic roundBasic;
    [SerializeField] float playerPowerCoefficient;
    public static UnityAction<int> onOppShooted;
    public static UnityAction onPlayerScored;
    public static UnityAction onPlayerMissed;
    public static UnityAction onballReleased;
    Vector3 prev;
    Quaternion RightControllerRot;


    private void Start()
    {
        tableau = GameObject.Find("Tableau").GetComponent<Tableau>();
        roundBasic = GameObject.Find("RoundController").GetComponent<RoundBasic>();
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Timer1", 0f, 0.5f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Vasya" + collision.gameObject.name);
        if (collision.gameObject.name.Contains("Border"))
        {
            //if (!RoundBasic.isPlayerTurn)
            //{
            //    onOppShooted?.Invoke(1);
            //}

            rb.useGravity = true;
        }

        if (collision.gameObject.name.Contains("Border"))
        {
            var vel = rb.velocity;
            Vector3 refl = Vector3.Reflect(vel, Vector3.up);
            rb.AddForce(refl);
        }

    }

    void Timer1()
    {
        prev = transform.position;
    }

    public void OnBallReleaseRightHand()
    {
        if (RoundBasic.isPlayerTurn)
        {
            rb.useGravity = true;
            RoundBasic.playerCanEndTurn = true;
            tableau.instructions.text = "Press Y to end turn";
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.deviceVelocity, out RightControllerVelocity);
            Vector3 dir = transform.position - prev; dir.Normalize();
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.deviceRotation, out RightControllerRot);
            Vector3 throwDirection = RightControllerRot * transform.right.normalized;
            float Rpower = RightControllerVelocity.magnitude;
            rb.AddForce(dir * playerPowerCoefficient * Time.deltaTime * Rpower);
            Invoke("GkNeedToJump", 0.5f);
        }


    }

    public void OnBallReleaseLeftHand()
    {
        if (RoundBasic.isPlayerTurn)
        {
            rb.useGravity = true;
            RoundBasic.playerCanEndTurn = true;
            tableau.instructions.text = "Press Y to end turn";
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.deviceVelocity, out LeftControllerVelocity);
            Vector3 dir = transform.position - prev; dir.Normalize();
            float Lpower = LeftControllerVelocity.magnitude;
            rb.AddForce(dir * playerPowerCoefficient * Time.deltaTime * Lpower);
            Invoke("GkNeedToJump", 0.5f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("TriggerCube"))
        {
            rb.useGravity = true;

            if (RoundBasic.isPlayerTurn)
            {
                onPlayerScored?.Invoke();
            }

            else
            {
                onOppShooted?.Invoke(0);
                rb.useGravity = true;
                PenaltyOppBasic.enemyScored = true;
            }



            //if (!RoundBasic.isPlayerTurn)
            //{
            //    roundBasic.OppScored();
            //}

        }

        else if (other.name.Contains("MissCube"))
        {
            if (!RoundBasic.isPlayerTurn)
            {
                onOppShooted?.Invoke(1);
            };
        }
    }
    void GkNeedToJump()
    {
        P_Ball.onballReleased?.Invoke();
    }

    private void Update()
    {
    }


}
