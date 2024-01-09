using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class BandaidChangeScript : MonoBehaviour
{
    [SerializeField] private GameObject[] arObjects;
    [SerializeField] private Button buttonBigger;
    [SerializeField] private Button buttonSmaller;
    private int currentBandaidIndex = 0;

    void Start() {
        buttonBigger.onClick.AddListener(() => {
            currentBandaidIndex = Math.Clamp(currentBandaidIndex + 1, 0, arObjects.Length - 1);
            SetBandaidActive(currentBandaidIndex);
        });
        buttonSmaller.onClick.AddListener(() =>
        {
            currentBandaidIndex = Math.Clamp(currentBandaidIndex - 1, 0, arObjects.Length - 1);
            SetBandaidActive(currentBandaidIndex);
        });
    }

    private void SetBandaidActive(int index)
    {
        for(int i=0; i < arObjects.Length; i++)
        {
            arObjects[i].SetActive(i == index);
        }
    }

    public void Update()
    {
        Transform transform = GetBandAidTransform(currentBandaidIndex);
        for (int i=0; i < arObjects.Length; i++)
        {
            if(i == currentBandaidIndex)
            {
                continue;
            }
            GetBandAidTransform(i).SetLocalPositionAndRotation(transform.localPosition, transform.localRotation);
        }
    }

    private Transform GetBandAidTransform(int index)
    {
        return arObjects[index].GetComponent<ARTrackedImageManager>().trackedImagePrefab.transform.GetChild(0).transform;
    }
}
