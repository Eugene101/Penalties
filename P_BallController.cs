using System.Collections;
using System.Collections.Generic;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;
using UnityEngine;

public class P_BallController : MonoBehaviour
{
    [SerializeField] Transform ballPoint;
    [SerializeField] GameObject ball;
    [SerializeField, Range(0, 10)] int ballCooldown;
    public GameObject cloth;
    public static bool iCanSpawnBall = true;
    public static bool ballIsInGame;
    Rigidbody rb;


    private void Start()
    {
        rb = ball.GetComponent<Rigidbody>();
    }

    void Idle()
    {
        ball.transform.position = ballPoint.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        Invoke("CoolDown", 1f);
    }

    void Update()
    {
        //if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Button1) && iCanSpawnBall && RoundBasic.isPlayerTurn)
        //{
        //    Idle();
        //    iCanSpawnBall = false;
        //}

        if (Input.GetKeyDown(KeyCode.JoystickButton0) && iCanSpawnBall)
        {
            Idle();
            iCanSpawnBall = false;
            Invoke("CoolDown", 0.2f);
        }
    }

    void CoolDown()
    {
        iCanSpawnBall = true;
    }
}
