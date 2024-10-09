using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[DisallowMultipleComponent]
public class TextToSpeech : MonoBehaviour
{
    [TextArea(3, 10)]
    public string textToSpeak = "Hello, world!";
    public string apiKey = "YOUR_GOOGLE_CLOUD_API_KEY";
    public string languageCode = "en-US";

    private string apiUrl = "https://texttospeech.googleapis.com/v1/text:synthesize";

    public void StartTextToSpeech()
    {
        StartCoroutine(MakeTextToSpeechRequest(textToSpeak));
    }

    IEnumerator MakeTextToSpeechRequest(string text)
    {
        string postData = $"{{\"input\":{{\"text\":\"{text}\"}},\"voice\":{{\"languageCode\":\"{languageCode}\"}},\"audioConfig\":{{\"audioEncoding\":\"LINEAR16\"}}}}";

        UnityWebRequest www = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(postData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();

        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", $"Bearer {apiKey}");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
        }
        else
        {
            Debug.LogError($"Text-to-Speech request failed: {www.error}");
        }
    }
}

