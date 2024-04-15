using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemySetup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform enemyPlace;
    [SerializeField] GameObject enemyParent;

    void Start()
    {
        InstallEnemy();
    }

    void InstallEnemy()
    {
        int rand = Random.Range(0, enemies.Length);
        GameObject enemy = Instantiate(enemies[rand], enemyPlace.position, transform.rotation);
        enemy.gameObject.tag = "Enemy";
        if (enemy.GetComponent<Rigidbody>() == null)
        { enemy.AddComponent<Rigidbody>(); }
        //var script = enemy.AddComponent<PenaltyOppBasic>();

        foreach (var item in enemy.transform)
        {
            print("enemy " + transform.name);
        }
    }
}
