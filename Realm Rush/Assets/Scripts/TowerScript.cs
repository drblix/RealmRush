using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    private int cost = 50;


    public bool CreateTower(TowerScript tower, Vector3 position)
    {
        BankScript bank = FindObjectOfType<BankScript>();
        Transform towersPool = GameObject.Find("TowersPool").transform;

        if (bank != null && bank.CurrentBalance >= cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity, towersPool);
            bank.WithdrawCoins(cost);
            return true;
        }

        return false;
    }
}
