using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int goldReward = 10;
    [SerializeField] [Range(5f, 100f)]
    private float castleDamageAmount = 10f;

    private BankScript bank;
    private CastleScript castle;

    private void Awake()
    {
        bank = FindObjectOfType<BankScript>();
        castle = FindObjectOfType<CastleScript>();
    }
    
    public void RewardGold()
    {
        if(bank == null) { Debug.LogWarning("Bank not found"); return; }

        bank.DepositCoins(goldReward);
    }

    public void AttackCastle()
    {
        if (castle == null) { Debug.LogWarning("Castle not found"); return; }

        castle.DamageCastle(castleDamageAmount);
    }
}
