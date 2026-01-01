using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlaybackUI : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = System.IO.Path.Combine(
            Application.streamingAssetsPath,
            "NumerosARolar.mp4"
        );

        videoPlayer.isLooping = true;
        videoPlayer.playOnAwake = false;
    }

    public void Play()
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    public void Stop()
    {
        videoPlayer.Stop();
        videoPlayer.gameObject.SetActive(false);
    }
}
