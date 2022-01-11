using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    private int cost = 50;

    public bool CreateTower(TowerScript tower, Vector3 position)
    {
        BankScript bank = FindObjectOfType<BankScript>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.WithdrawCoins(cost);
            return true;
        }

        return false;
    }
}
