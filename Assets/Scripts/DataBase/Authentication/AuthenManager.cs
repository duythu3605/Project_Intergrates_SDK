using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;

public class AuthenManager : MonoBehaviour
{
    private GameManager _gameManager ;
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => 
        {
            dependencyStatus = task.Result;
            if(dependencyStatus == DependencyStatus.Available){
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void LoginClick(string email,string password)
    {
        StartCoroutine(Login(email, password));
    }

    public void RegisterClick(string _email, string _password, string _username)
    {
        StartCoroutine(Register(_email, _password,_username));
    }

    private IEnumerator Login(string _email, string _password)
    {
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);

        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if(LoginTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");

            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";

            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            _gameManager._uIManager._uiLogin.ShowConfirmText(message, true);
            /// _login text = message;
        }
        else
        {
            User = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            _gameManager._uIManager._uiLogin.ShowConfirmText("Logged In", false);
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if(_username == "")
        {
            _gameManager._uIManager._uiRegister.ShowConfirmText("Missing Username", true);
        }
        else if (_gameManager._uIManager._uiRegister._inputPass.text != _gameManager._uIManager._uiRegister._inputPassAgain.text)
        {
            _gameManager._uIManager._uiRegister.ShowConfirmText("Password Does Not Match!", true);
        }
        else
        {
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;

                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email!";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password!";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password!";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use!";
                        break;
                }
                _gameManager._uIManager._uiRegister.ShowConfirmText(message, true);
            }
            else
            {
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    var ProfileTask = User.UpdateUserProfileAsync(profile);

                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if(ProfileTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError erroCode = (AuthError)firebaseEx.ErrorCode;
                        _gameManager._uIManager._uiRegister.ShowConfirmText("Username Set Failed!", true);
                    }
                    else
                    {
                        _gameManager._uIManager._uiRegister.HideUIRegister();
                        _gameManager._uIManager._uiLogin.ShowUILogin();
                    }
                }
            }
        }
    }
}
