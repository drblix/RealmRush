using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BankScript : MonoBehaviour
{
    // Ballista tower = 50 coins

    [SerializeField]
    private int startingBalance = 100;

    [SerializeField]
    private int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    private void Awake()
    {
        currentBalance = startingBalance;
    }

    public void DepositCoins(int amount)
    {
        currentBalance += Mathf.Abs(amount);
    }

    public void WithdrawCoins(int amount)
    {
        currentBalance -= Mathf.Abs(amount);

        if (currentBalance < 0)
        {
            // Game over
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
