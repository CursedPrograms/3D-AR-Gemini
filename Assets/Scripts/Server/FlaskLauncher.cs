using System.IO;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class FlaskLauncher : MonoBehaviour
{
    private static Process flaskProcess;

    public static bool useGemini = true;

    void Awake()
    {
        KillPythonProcesses();
    }

    void Start()
    {        
        Application.quitting += OnApplicationQuit;
    }

    static void OnApplicationQuit()
    {
        if (flaskProcess != null && !flaskProcess.HasExited)
        {
            flaskProcess.Kill();
            flaskProcess.Dispose();
        }
        KillPythonProcesses();
    }

    public static void LaunchFlask()
    {
        string dataPath = Application.dataPath;

        Thread thread = new Thread(() => RunFlaskProcess(dataPath));
        thread.IsBackground = true;
        thread.Start();        
    }

    static void KillPythonProcesses()
    {
        Process[] processes = Process.GetProcessesByName("python");
        foreach (var process in processes)
        {
            try
            {
                process.Kill();
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"Failed to kill Python process: {ex.Message}");
            }
        }
    }

    static void RunFlaskProcess(string dataPath)
    {
        string currentServer = "";


        if (useGemini)
        {
            currentServer = "unity_gemini_app.py";
        }
        else
        {
            currentServer = "unity_app.py";
        }

        string scriptPath = Path.Combine(dataPath, "Resources", currentServer);

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "CMD.exe",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = Path.GetDirectoryName(scriptPath),
            Verb = "runas"
        };

        flaskProcess = new Process { StartInfo = psi };
        flaskProcess.Start();

        flaskProcess.StandardInput.WriteLine($"cd /D {Path.GetDirectoryName(scriptPath)}");
        flaskProcess.StandardInput.WriteLine($"python {Path.GetFileName(scriptPath)}");
    }
}


