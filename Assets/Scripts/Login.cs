using UnityEngine;

public class Login : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField usernameInputField;
    [SerializeField] private UnityEngine.UI.Button loginButton;

    void Start()
    {
        if (usernameInputField != null && loginButton != null)
        {
            loginButton.onClick.AddListener(OnLoginButtonClicked);
        }
        else
        {
            Debug.LogWarning("Username Input Field or Login Button is not assigned!");
        }
    }

    public void OnLoginButtonClicked()
    {
        string username = usernameInputField.text;
        if (!string.IsNullOrEmpty(username))
        {
            Debug.Log("Username entered: " + username);

            // Set player name and start the game
            if (PlayerMovement.Instance != null)
            {
                PlayerMovement.Instance.OnPlayerLoggedIn(username);
            }
            else
            {
                Debug.LogError("PlayerMovement.Instance is null!");
            }

            // Optionally, hide the login panel here if not handled elsewhere
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Username field is empty!");
        }
    }
}