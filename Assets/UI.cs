using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;
    public Toggle toggle4;
    public Toggle toggle5;
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        toggle1 = root.Q<Toggle>("toggle1");
        toggle2 = root.Q<Toggle>("toggle2");
        toggle3 = root.Q<Toggle>("toggle3");
        toggle4 = root.Q<Toggle>("toggle4");
        toggle5 = root.Q<Toggle>("toggle5");
        Button buttonScene = root.Q<Button>("button3");
        Button Rescue = root.Q<Button>("RescueButton");
        Debug.Log(toggle1.value);
        buttonScene.clicked += () => TaskOnClick();
        Rescue.clicked += () => Application.OpenURL("tel:1234567890");

    }
    void TaskOnClick()
    {
        if (toggle1.value == true && toggle2.value == true && toggle3.value == true && toggle4.value == true && toggle5.value == true )
        {
            MoveToScene(1);
            AndroidToastMessage("Changing to AR...");
            Debug.Log("Changing to AR");
        }
        else
        {
            Debug.Log("First complete all the steps !");
            AndroidToastMessage("Please complete and check all the steps!");
        }
    }
    private void AndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}