using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Net;
using UnityEngine.SceneManagement;

public class TaskSampleTest : MonoBehaviour
{
    // Start is called before the first frame update
    private async void Start()
    {
        Debug.Log("Wait.");
        await Task.WhenAll(new Task[] {
            DownloadFileAcsync("https://sample-videos.com/img/Sample-jpg-image-1mb.jpg", "1.jpg"),
            DownloadFileAcsync("https://sample-videos.com/img/Sample-jpg-image-2mb.jpg", "2.jpg"),
            DownloadFileAcsync("https://sample-videos.com/img/Sample-jpg-image-10mb.jpg", "10.jpg"),
        });
        Debug.Log("Test");
    }
    private async Task DownloadFileAcsync(string url, string filename)
    {
        //await SceneManager.LoadSceneAsync(0);

        Debug.Log("1");
        //https://sample-videos.com/download-sample-jpg-image.php

        var webClient = new WebClient();

        Debug.LogFormat("DownloadBegin url:{0}, filename:{1}", url, filename);

        webClient.DownloadProgressChanged += (sender, e) =>
        {
            Debug.LogFormat("DownloadProgressChanged url:{0}, filename:{1}, bytesReceived:{2}", url, filename, e.BytesReceived);
        };

        await webClient.DownloadFileTaskAsync(new Uri(url), filename);

        Debug.LogFormat("DownloadFinish url:{0}, filename:{1}", url, filename);
    }
}
