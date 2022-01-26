using System.Collections;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    private int cost = 50;
    [SerializeField]
    private float buildDelay = 1f;

    private void Start()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(BuildTower());
        }
    }

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

    public IEnumerator BuildTower()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);

            foreach (Transform grandchild in child)
            {
                yield return new WaitForSeconds(buildDelay);
                grandchild.gameObject.SetActive(true);
            }
        }
    }
}
