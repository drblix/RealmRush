using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BankScript : MonoBehaviour
{
    // Ballista tower = 50

    [SerializeField]
    private TextMeshProUGUI coinsAmountUI;

    [SerializeField]
    private Color _lowCoinsColor;
    [SerializeField]
    private Color _coinsColor;

    [Header("Currency modifications")]

    [SerializeField]
    private int startingBalance = 150;

    [SerializeField]
    private int currentBalance;
    public int CurrentBalance { get { return currentBalance; } } // Getter for "currentBalance" variable

    // Variables


    private void Awake()
    {
        currentBalance = startingBalance; // Sets current balance at start of game to starting balance
        UpdateDisplay(); // Calls UpdateDisplay function
    }

    public void DepositCoins(int amount)
    {
        currentBalance += Mathf.Abs(amount); // Adds absolute value of passed in amount to current balance

        UpdateDisplay(); // Calls UpdateDisplay function
    }

    public void WithdrawCoins(int amount)
    {
        currentBalance -= Mathf.Abs(amount); // Subtracts absolute value of passed in ammount from current balance

        if (currentBalance < 0)
        {
            currentBalance = 0;
        }

        UpdateDisplay(); // Calls UpdateDisplay function
    }

    private void UpdateDisplay()
    {
        coinsAmountUI.text = "Coins: " + currentBalance.ToString(); // Sets text to "Coins: " then current balance

        if (currentBalance <= 20) // If current balance is less than or equal to 20, set text to red
        {
            coinsAmountUI.color = _lowCoinsColor;
        }
        else
        {
            coinsAmountUI.color = _coinsColor; // If current balance is greater than 20, set text to yellow
        }
    }
}
