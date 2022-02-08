using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public bool isVideoEnabled;
    [Space]
    public VideoPlayer videoPlayer;
    public RawImage rawImage;

    private void Start()
    {
        StartCoroutine(PrepareVideo());
    }

    private IEnumerator PrepareVideo()
    {
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(.5f);
        }

        rawImage.texture = videoPlayer.texture;

        isVideoEnabled = true;
    }

}