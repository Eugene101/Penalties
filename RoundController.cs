using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] GameObject roundManagerHolder;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject player;
    [SerializeField] GameObject tableau;
    GameObject goalkeeper;
    GameObject enemy;
    RoundBasic roundBasic;
  

    // Start is called before the first frame update

    void Start()
    {
        goalkeeper = GameObject.FindGameObjectWithTag("GK");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        roundBasic = roundManagerHolder.GetComponent<RoundBasic>();
    }
  }
