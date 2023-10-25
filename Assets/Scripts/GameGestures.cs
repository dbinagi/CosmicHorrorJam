using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

public class GameGestures : MonoBehaviour
{

    [SerializeField]
    public FingersImageGestureHelperComponentScript ImageScript;

    [SerializeField]
    public TrailRenderer trail;

    int linesAmount;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            trail.Clear();
        }

        if (linesAmount < 2)
        {
            trail.Clear();
        }
        else
        {
            trail.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        ImageScript.LinesUpdated += LinesUpdated;
        ImageScript.LinesCleared += LinesCleared;
        ImageScript.Gesture.StateUpdated += Gesture_StateUpdated;
    }

    private void LinesUpdated(object sender, System.EventArgs args)
    {
        linesAmount++;
    }

    private void LinesCleared(object sender, System.EventArgs args)
    {
        Debug.LogFormat("Lines cleared!");
    }

    private void Gesture_StateUpdated(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            ImageGestureImage match = ImageScript.CheckForImageMatch(false);
            if (match != null)
            {
                // Debug.Log("Found image match: " + match.Name);
                MiniGameManager.Instance.Match(match.Name);
            }
            else
            {
                MiniGameManager.Instance.NotMatch();
                // Debug.Log("No match found!");
            }

            linesAmount = 0;
            trail.Clear();
            trail.gameObject.SetActive(false);

            ImageScript.BeginAnimateOutLines();
            gesture.Reset();
        }
    }
}
