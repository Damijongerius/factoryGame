using System;

using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class signingManager : MonoBehaviour
{
    private static signingManager instance;

    private Login login;
    private SignUp signUp;
    private Offline offline;

    private Button switchSigning;

    private Button signing;
    private Button playOffline;

    private WebServer ws;

    private TextMeshProUGUI switchSigningText;
    private TextMeshProUGUI signingText;

    private bool LoginSignUp;
    private void Start()
    {
        ws = new WebServer();
        Transform options = transform.Find("options");
        switchSigning = options.Find("SwitchSigning").GetComponent<Button>();
        signing = options.Find("Signing").GetComponent<Button>();
        playOffline = options.Find("playOffline").GetComponent<Button>();

        switchSigningText = switchSigning.GetComponentInChildren<TextMeshProUGUI>();
        signingText = signing.GetComponentInChildren<TextMeshProUGUI>();

        playOffline.onClick.AddListener(GoOffline);
        switchSigning.onClick.AddListener(Switch);
        signing.onClick.AddListener(Sign);

        TMP_InputField password = transform.Find("PasswordField").GetComponent<TMP_InputField>();
        TMP_InputField username = transform.Find("UserNameField").GetComponent<TMP_InputField>();

        login = new Login(username, password, this);
        signUp = new SignUp(username, password, this);
        offline = new Offline(this);

        
    }
    
    private void Sign()
    {
        if(LoginSignUp)
        {
            login.LoggingIn();
        }
        else
        {
            signUp.SigningUp();
        }
    }

    private void GoOffline()
    {
        offline.Set();
    }

    private void Switch()
    {
        if (LoginSignUp)
        {
            switchSigningText.text = "SignUp";
            signingText.text = "Login";
            LoginSignUp = false;
        }
        else
        {
            switchSigningText.text = "Login";
            signingText.text = "Signup";
            LoginSignUp = true;
        }
    }

    private void Awake()
    {
        if(User.GetInstance().UserName != null)
        {
            transform.parent.parent.gameObject.GetComponent<openCloseManager>().HandleClick();
        }
    }
}
