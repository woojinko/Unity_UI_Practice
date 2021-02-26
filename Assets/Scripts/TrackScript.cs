using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackScript : MonoBehaviour
{
    public Text trackText;
    public GameObject myIconPrefab;
    public Transform trackTransform;
    public void SetUpTrack(Color trackColor, string trackName)
    {
        // Set up track color and text
        gameObject.GetComponent<Image>().color = trackColor;
        trackText.text = trackName;
    }
    public void SetUpIcon()
    {
        // Set up icon on track
        GameObject myNewTrack = Instantiate(myIconPrefab, trackTransform);
    }
}
