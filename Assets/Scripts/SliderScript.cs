using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    public GameObject myImagePrefab;
    public Transform backgroundTransform;
    public RectTransform handleArea;
    public int totalFrames;
    public int numColors;
    private int numTracks = 0;
    private int colorIndex = 0;
    private List<Color> colorList = new List<Color>();
    private (int, int) currentFrames = (0, 0);
    private GameObject currentTrack;


    public void Start()
    {
        // Generate the specified number of random colors within each hue interval
        for (float i = 0; i < numColors; i++)
        {
            colorList.Add(Random.ColorHSV(i / 10f, (i + 1) / 10f, 0.5f, 0.5f, 0.5f, 0.5f));
        }

        // Shuffle colors for use in tracks
        colorList.Shuffle();
    }

    public void AddTrack(int start, int end, string name)
    {
        // Unfortunately, you can't call a multi parameter method in a unity event
        // But we can make a 0 or 1 parameter method (AddRandomTrack) for a unity event that calls this AddTrack method

        numTracks += 1;
        var rectTransform = myImagePrefab.GetComponent<RectTransform>();

        // Set width by dividing frame by total number of frames (set in inspector to 800 currently)
        float minX = (float) start / totalFrames;
        float maxX = (float) end / totalFrames;

        // Set height of 0.25f with offset based on number of tracks
        // TODO: need to implement scroll bar in the future
        float minY = (float) 1 - numTracks * 0.5f;
        float maxY = (float) 1 - (numTracks - 1) * 0.5f;

        // Set width and height of track
        rectTransform.anchorMin = new Vector2(minX, minY);
        rectTransform.anchorMax = new Vector2(maxX, maxY);

        GameObject newTrack = Instantiate(myImagePrefab, backgroundTransform);
        currentTrack = newTrack;
        colorIndex = (numTracks - 1) % 10;

        // Call SetUp method in TrackScript for track to set itself up with these parameters
        newTrack.GetComponent<TrackScript>().SetUpTrack(colorList[colorIndex], name);

        // Resize slider handle according to number of tracks
        float newXSize = handleArea.offsetMin.x;
        float newYSize = handleArea.offsetMin.y - 10;
        handleArea.offsetMin = new Vector2(newXSize, newYSize);

    }

    public void AddRandomTrack()
    {
        // Generate two random values
        int frame1 = (int)Random.Range(0, totalFrames);
        int frame2 = (int)Random.Range(0, totalFrames);

        // Calculate difference, make sure it's bigger than 1/4 of the size of total track
        int difference = Mathf.Abs(frame1 - frame2);
        while (difference < 1f / 3 * totalFrames)
        {
            frame1 = (int) Random.Range(0, totalFrames);
            frame2 = (int) Random.Range(0, totalFrames);
            difference = Mathf.Abs(frame1 - frame2);
        }
        int startFrame = frame1;
        int endFrame = frame2;

        // Figure out which one is bigger smaller, then call AddTrack(smaller, bigger)
        if (frame1 > frame2)
        {
            startFrame = frame2;
            endFrame = frame1;
        }
        string description = startFrame + " - " + endFrame;
        currentFrames = (startFrame, endFrame);
        AddTrack(startFrame, endFrame, description);
    }

    public void AddIcon()
    {
        if (numTracks > 0)
        {
            // Call SetUp method in TrackScript for track to set itself up with these parameters
            currentTrack.GetComponent<TrackScript>().SetUpIcon();
        }
    }

}
