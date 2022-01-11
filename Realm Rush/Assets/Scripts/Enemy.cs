using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int goldReward = 10;
    [SerializeField]
    private int goldPenalty = 25;

    private BankScript bank;

    private void Awake()
    {
        bank = FindObjectOfType<BankScript>();
    }

    public void RewardGold()
    {
        if(bank == null) { return; }

        bank.DepositCoins(goldReward);
    }

    public void StealGold()
    {
        if (bank == null) { return; }

        bank.WithdrawCoins(goldPenalty);
    }
}
