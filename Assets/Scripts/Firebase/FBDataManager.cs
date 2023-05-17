using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using TMPro;

public class FBDataManager : MonoBehaviour
{

    DatabaseReference reference;

    public TextMeshProUGUI PointCounter;
    public TMP_InputField PointCounterByAmount;
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseDatabase.DefaultInstance.GetReference("counter").ValueChanged += PointTracker;
        
    }


    public void PointTracker(object sender, ValueChangedEventArgs args)
    {
        DataSnapshot snapshot = args.Snapshot;
        Debug.Log(snapshot.Value);
        PointCounter.text = snapshot.Value.ToString();
    }

    public void ScoreUpdater()
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
                int value = int.Parse(Convert.ToString(snapshot.Value));
                value += int.Parse(PointCounterByAmount.text);
                reference.Child("counter").SetValueAsync(value);
                reference.Child("Heba").Child("email").SetValueAsync("Heba@gmail.com");

            }
        });
    }
}
