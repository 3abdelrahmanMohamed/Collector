using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using TMPro;

public class MainTaskManager : MonoBehaviour
{

    DatabaseReference reference;
    public String UserReference;

    public TextMeshProUGUI MainTaskReward;
    public TextMeshProUGUI MainTaskStatus;
    public TextMeshProUGUI MainTaskReward2;
    public TextMeshProUGUI MainTaskStatus2;
    public TextMeshProUGUI MainTaskReward3;
    public TextMeshProUGUI MainTaskStatus3;
    public TextMeshProUGUI PointCounter;

    public TextMeshProUGUI WarningText;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        UserReference = FBAuthManager.Username;
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("points").ValueChanged += RewardTracker;

        // track task status for 1,2,3
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("MainTasks").Child("TaskStatus").ValueChanged += StatusTracker;
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("MainTasks").Child("TaskStatus2").ValueChanged += StatusTracker2;
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("MainTasks").Child("TaskStatus3").ValueChanged += StatusTracker3;

    }


    public void RewardTracker(object sender, ValueChangedEventArgs args)
    {
        DataSnapshot snapshot = args.Snapshot;
        Debug.Log(snapshot.Value);
        PointCounter.text = snapshot.Value.ToString();
    }

    public void StatusTracker(object sender, ValueChangedEventArgs args)
    {
        DataSnapshot snapshot = args.Snapshot;
        Debug.Log(snapshot.Value);
        MainTaskStatus.text = snapshot.Value.ToString();
    }

    public void StatusTracker2(object sender, ValueChangedEventArgs args)
    {
        DataSnapshot snapshot = args.Snapshot;
        Debug.Log(snapshot.Value);
        MainTaskStatus2.text = snapshot.Value.ToString();
    }

    public void StatusTracker3(object sender, ValueChangedEventArgs args)
    {
        DataSnapshot snapshot = args.Snapshot;
        Debug.Log(snapshot.Value);
        MainTaskStatus3.text = snapshot.Value.ToString();
    }

    public void MTaskReward()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("points").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task);
            }
            else if (task.IsCompleted)
            {
                if(MainTaskStatus.text == "INACTIVE")
                {
                    reference.Child("Users").Child(UserReference).Child("MainTasks").Child("TaskStatus").SetValueAsync("PENDING");

                }
                else if (MainTaskStatus.text == "PENDING")
                {
                    WarningText.text = "Already Pending. Please wait.";
                }
                else if (MainTaskStatus.text == "REDEEMABLE")
                {
                    DataSnapshot snapshot = task.Result;
                    int value = int.Parse(Convert.ToString(snapshot.Value));
                    value += int.Parse(MainTaskReward.text);
                    reference.Child("Users").Child(UserReference).Child("points").SetValueAsync(value);
                    reference.Child("Users").Child(UserReference).Child("MainTasks").Child("TaskStatus").SetValueAsync("REDEEMED");
                }
                else if(MainTaskStatus.text == "REDEEMED")
                {
                    WarningText.text = "Already Redeemed";
                }
                

            }
        });
    }

    public void MTaskReward2()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("points").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task);
            }
            else if (task.IsCompleted)
            {
                if (MainTaskStatus2.text == "INACTIVE")
                {
                    reference.Child("Users").Child(UserReference).Child("MainTasks").Child("TaskStatus2").SetValueAsync("PENDING");

                }
                else if (MainTaskStatus2.text == "PENDING")
                {
                    WarningText.text = "Already Pending. Please wait.";
                }
                else if (MainTaskStatus2.text == "REDEEMABLE")
                {
                    DataSnapshot snapshot = task.Result;
                    int value = int.Parse(Convert.ToString(snapshot.Value));
                    value += int.Parse(MainTaskReward2.text);
                    reference.Child("Users").Child(UserReference).Child("points").SetValueAsync(value);
                    reference.Child("Users").Child(UserReference).Child("MainTasks").Child("TaskStatus2").SetValueAsync("REDEEMED");
                }
                else if (MainTaskStatus2.text == "REDEEMED")
                {
                    WarningText.text = "Already Redeemed";
                }


            }
        });
    }

    public void MTaskReward3()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("points").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task);
            }
            else if (task.IsCompleted)
            {
                if (MainTaskStatus3.text == "INACTIVE")
                {
                    reference.Child("Users").Child(UserReference).Child("MainTasks").Child("TaskStatus3").SetValueAsync("PENDING");

                }
                else if (MainTaskStatus3.text == "PENDING")
                {
                    WarningText.text = "Already Pending. Please wait.";
                }
                else if (MainTaskStatus3.text == "REDEEMABLE")
                {
                    DataSnapshot snapshot = task.Result;
                    int value = int.Parse(Convert.ToString(snapshot.Value));
                    value += int.Parse(MainTaskReward3.text);
                    reference.Child("Users").Child(UserReference).Child("points").SetValueAsync(value);
                    reference.Child("Users").Child(UserReference).Child("MainTasks").Child("TaskStatus3").SetValueAsync("REDEEMED");
                }
                else if (MainTaskStatus3.text == "REDEEMED")
                {
                    WarningText.text = "Already Redeemed";
                }


            }
        });
    }
}
