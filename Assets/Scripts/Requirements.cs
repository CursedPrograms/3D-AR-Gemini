using UnityEngine;

[DisallowMultipleComponent]
public class Requirements : MonoBehaviour
{
    string pythonDownloadLink = "https://www.python.org/downloads/";

    public void OpenPythonDownloadLink()
    {
        Application.OpenURL(pythonDownloadLink);
    }
}
