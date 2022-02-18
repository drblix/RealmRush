using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsoleHandler : MonoBehaviour
{
    private BankScript bank;
    private CastleScript castleScript;

    [SerializeField]
    private GameObject consoleUI;

    [SerializeField]
    private Button submitButton;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private TextMeshProUGUI placeholderText;

    [SerializeField]
    private AudioSource buttonSFX;

    private string commandText;

    private bool cheatsEnabled = false;

    private void Awake()
    {
        bank = FindObjectOfType<BankScript>();
        castleScript = FindObjectOfType<CastleScript>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote) && cheatsEnabled)
        {
            consoleUI.SetActive(!consoleUI.activeInHierarchy);
            buttonSFX.Play();
        }
    }

    public void CommandSubmitted()
    {
        if (inputField.text != null)
        {
            commandText = inputField.text.ToLower();

            inputField.text = null;

            CheckCommand();
        }
    }

    private void CheckCommand()
    {
        Debug.Log(commandText);
        buttonSFX.Play();

        if (commandText.Contains("help"))
        {

        }

        if (commandText.Contains("money"))
        {
            int amount;
            string[] words = commandText.Split(' ');

            // Checks if supplied value is an integer
            try
            {
                amount = Mathf.Abs(System.Convert.ToInt32(words[1]));
            }
            catch (System.Exception)
            {
                StartCoroutine(DisplayError("Value not an integer"));
                return;
            }

            bank.DepositCoins(amount);

            string amountStr = amount.ToString();

            if (amountStr.Length > 4)
            {
                amountStr = amountStr.Substring(0, 4) + "...";
            }

            StartCoroutine(DisplayMessage(amountStr + " coins added to bank"));

            return;
        }

        if (commandText.Contains("toohard"))
        {
            if (!castleScript.GodMode)
            {
                StartCoroutine(DisplayMessage("Enabling god-mode"));
            }
            else
            {
                StartCoroutine(DisplayMessage("Disabling god-mode"));
            }

            castleScript.ToggleGodMode();

            return;
        }

        if (commandText.Contains("timescale"))
        {
            float amount;
            string[] words = commandText.Split(' ');

            // Checks if supplied value is a float
            try
            {
                amount = Mathf.Abs(System.Convert.ToSingle(words[1]));
            }
            catch (System.Exception)
            {
                StartCoroutine(DisplayError("Value not a number"));
                return;
            }

            if (amount < 0.1f)
            {
                StartCoroutine(DisplayError("Must be greater than 0.1"));
                return;
            }

            StartCoroutine(DisplayMessage("Set timescale to: " + amount));

            Time.timeScale = amount;
            return;
        }

        if (commandText.Contains("firerate"))
        {
            float amount;
            string[] words = commandText.Split(' ');

            // Checks if supplied value is a float
            try
            {
                amount = Mathf.Abs(System.Convert.ToSingle(words[1]));
            }
            catch (System.Exception)
            {
                StartCoroutine(DisplayError("Value not a number"));
                return;
            }

            if (amount < 0.4f || amount > 2.5f)
            {
                StartCoroutine(DisplayError("Amount out of bounds"));
                return;
            }

            Transform towersPool = GameObject.FindGameObjectWithTag("TowersPool").transform;

            if (amount > 0.4f && amount < 2.5f)
            {
                foreach (Transform child in towersPool)
                {
                    child.GetComponent<TargetLocator>().ChangeFireRate(amount);
                }
            }

            StartCoroutine(DisplayMessage("Changed firerate to: " + amount));
            return;
        }

        if (commandText.Contains("clear"))
        {
            Transform objectPool = GameObject.Find("ObjectPool").transform;

            foreach (Transform child in objectPool)
            {
                child.gameObject.SetActive(false);
            }

            StartCoroutine(DisplayMessage("Cleared all enemies"));
            return;
        }

        StartCoroutine(DisplayError("Inputted command is invalid"));
    }


    private IEnumerator DisplayError(string errorMsg)
    {
        string originalText = placeholderText.text;
        Color originalColor = placeholderText.color;
        Color newColor = new Color(255f, 0f, 0f, 255f);

        inputField.interactable = false;
        submitButton.interactable = false;
        placeholderText.color = newColor;
        placeholderText.text = errorMsg;

        yield return new WaitForSeconds(1.5f);

        placeholderText.text = originalText;
        placeholderText.color = originalColor;
        inputField.interactable = true;
        submitButton.interactable = true;
    }

    private IEnumerator DisplayMessage(string message)
    {
        string originalText = placeholderText.text;

        submitButton.interactable = false;
        inputField.interactable = false;
        placeholderText.text = message;

        yield return new WaitForSeconds(1.5f);

        placeholderText.text = originalText;
        inputField.interactable = true;
        submitButton.interactable = true;
    }

    public void ToggleCheats(bool state)
    {
        cheatsEnabled = state;
    }
}
