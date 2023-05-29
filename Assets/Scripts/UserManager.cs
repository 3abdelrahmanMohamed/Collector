using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using TMPro;

public class UserManager : MonoBehaviour
{

    DatabaseReference reference;
    public String UserReference;

    public TextMeshProUGUI Username;

    //public TMP_InputField EmailInput;
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        UserReference = FBAuthManager.Username;
        Username.text = UserReference;
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("username").ValueChanged += UserTracker;

    }

    public void UserTracker(object sender, ValueChangedEventArgs args)
    {
        DataSnapshot snapshot = args.Snapshot;
        Debug.Log(snapshot.Value);
        Username.text = snapshot.Value.ToString();
    }


    public void UserUpdater()
    {
        FirebaseDatabase.DefaultInstance.GetReference("counter").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                String value = Convert.ToString(snapshot.Value);

            }
        });
    }
}
