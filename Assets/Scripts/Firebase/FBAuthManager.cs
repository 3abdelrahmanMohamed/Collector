using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;


public class FBAuthManager : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    [Header("Login")]
    public TMP_InputField UsernameLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;
    public static string UserEmail;
    public static string Username;


    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    public TMP_Text confirmRegisterText;

    DatabaseReference reference;


    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("could not resolve all firebase dependencies: " + dependencyStatus);
            }
        });

        reference = FirebaseDatabase.DefaultInstance.RootReference;



    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
    }
    public void LoginButton()
    {

        Username = UsernameLoginField.text;
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UsernameLoginField.text).Child("email").ValueChanged += EmailTracker;


    }

    public void EmailTracker(object sender, ValueChangedEventArgs args)
    {
        DataSnapshot snapshot = args.Snapshot;
        //Debug.Log(snapshot.Value);
        UserEmail = snapshot.Value.ToString();
        UsernameLoginField.text = UserEmail;
        //Debug.Log(UserEmail);
        //Debug.Log(UsernameLoginField.text);
        StartCoroutine(login(UsernameLoginField.text, passwordLoginField.text));

    }



    private IEnumerator login(string _email, string _password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {

            confirmLoginText.text = "";
            Debug.LogWarning(message: $"failed to register task with {loginTask.Exception}");
            FirebaseException firebaseEx = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong password";
                    break;
                case AuthError.InvalidEmail:
                    message = "invalid email";
                    break;
                case AuthError.UserNotFound:
                    message = "User Not Found";
                    break;
            }
            warningLoginText.text = message;
        }

        else
        {
            User = loginTask.Result;
            Debug.LogFormat("User signed in succesfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "logged in";
            SceneManager.LoadScene(1);

        }
    }

    public void registerButton()
    {
        StartCoroutine(register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
        UserEmail = emailRegisterField.text;
    }

    private IEnumerator register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            warningRegisterText.text = "Password does not match!";
        }
        else
        {

            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);
            if (registerTask.Exception != null)
            {

                confirmRegisterText.text = "";
                Debug.LogWarning(message: $"failed to register task with {registerTask.Exception}");
                FirebaseException firebaseEx = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing password";
                        break;
                    case AuthError.WrongPassword:
                        message = "Wrong password";
                        break;
                    case AuthError.InvalidEmail:
                        message = "invalid email";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email already in use";
                        break;
                }
                confirmRegisterText.text = message;


            }

            else
            {
                User = registerTask.Result;
                if (User != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = _username };
                    var profileTask = User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => profileTask.IsCompleted);
                    SceneManager.LoadScene(1);
                    if (profileTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"failed to register task with { profileTask.Exception}");
                        FirebaseException firebaseEx = profileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username set failed!";


                    }
                }


            }

        }
    }





}
