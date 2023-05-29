using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using TMPro;

public class TaskManager : MonoBehaviour
{

    DatabaseReference reference;
    public String UserReference;

    public TextMeshProUGUI PointReward;
    public TextMeshProUGUI PointReward2;
    public TextMeshProUGUI PointReward3;
    public TextMeshProUGUI PointReward4;
    public TextMeshProUGUI PointReward5;
    public TextMeshProUGUI PointCounter;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        UserReference = FBAuthManager.Username;
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("points").ValueChanged += RewardTracker;
        
    }


    public void RewardTracker(object sender, ValueChangedEventArgs args)
    {
        DataSnapshot snapshot = args.Snapshot;
        Debug.Log(snapshot.Value);
        PointCounter.text = snapshot.Value.ToString();
    }

    public void TaskReward()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("points").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int value = int.Parse(Convert.ToString(snapshot.Value));
                value += int.Parse(PointReward.text);
                reference.Child("Users").Child(UserReference).Child("points").SetValueAsync(value);

            }
        });
    }

    public void TaskReward2()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("points").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int value = int.Parse(Convert.ToString(snapshot.Value));
                value += int.Parse(PointReward2.text);
                reference.Child("Users").Child(UserReference).Child("points").SetValueAsync(value);

            }
        });
    }

    public void TaskReward3()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("points").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int value = int.Parse(Convert.ToString(snapshot.Value));
                value += int.Parse(PointReward3.text);
                reference.Child("Users").Child(UserReference).Child("points").SetValueAsync(value);

            }
        });
    }

    public void TaskReward4()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("points").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int value = int.Parse(Convert.ToString(snapshot.Value));
                value += int.Parse(PointReward4.text);
                reference.Child("Users").Child(UserReference).Child("points").SetValueAsync(value);

            }
        });
    }

    public void TaskReward5()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserReference).Child("points").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int value = int.Parse(Convert.ToString(snapshot.Value));
                value += int.Parse(PointReward5.text);
                reference.Child("Users").Child(UserReference).Child("points").SetValueAsync(value);

            }
        });
    }
}
