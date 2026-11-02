using System;
using Unity.Mathematics;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    [SerializeField]
    private GameObject _basicAttackPrefab;
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private string _basicAttack;

    public void BasicAttack()
    {
        GameObject basicAttack = Instantiate(_basicAttackPrefab, _attackPoint.position, _attackPoint.transform.parent.rotation);

        // Mirror the attack to match the player's facing (player flips via localScale.x)
        float facing = Mathf.Sign(transform.localScale.x);
        Vector3 s = basicAttack.transform.localScale;
        s.x = Mathf.Abs(s.x) * facing;
        basicAttack.transform.localScale = s;

        Destroy(basicAttack, 2.0f); // sloppy but whatevs
    }
}
