using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(FlaskLauncher))]
[RequireComponent(typeof(Networking))]
public class ServerCore : MonoBehaviour
{
    Networking networking;
    FlaskLauncher flaskLauncher;

    private void Awake()
    {
        networking = GetComponent<Networking>();
        flaskLauncher = GetComponent<FlaskLauncher>();
    }

    void Start()
    {
        StartCoroutine(StartServer());
    }

    IEnumerator StartServer()
    {
        FlaskLauncher.LaunchFlask();
        yield return new WaitForSeconds(3f);
    }
}
