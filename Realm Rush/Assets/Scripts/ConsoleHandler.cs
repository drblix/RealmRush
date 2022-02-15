using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsoleHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;

    private string commandText;

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

        if (commandText.Contains("money"))
        {
            int amount;
            string[] words = commandText.Split(' ');

            // Checks if supplied value is an integer
            try
            {
                amount = System.Convert.ToInt32(words[1]);
            }
            catch (System.Exception)
            {
                Debug.LogError("Value not integer");
                return;
            }

            Debug.Log(amount);
        }
    }
}
