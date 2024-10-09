using UnityEditor;
using UnityEngine;
using System.Diagnostics;

public class PythonInstallerWindow : EditorWindow
{
    private string pythonExecutable = "python";
    private string requirementsFilePath = "Assets/Resources/requirements.txt";

    [MenuItem("Window/Python Installer")]
    public static void ShowWindow()
    {
        GetWindow<PythonInstallerWindow>("Python Installer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Python Installer", EditorStyles.boldLabel);

        pythonExecutable = EditorGUILayout.TextField("Python Executable:", pythonExecutable);
        requirementsFilePath = EditorGUILayout.TextField("Requirements File Path:", requirementsFilePath);

        if (GUILayout.Button("Install Python Packages"))
        {
            InstallPackages();
        }
    }

    private void InstallPackages()
    {
        Process process = new Process();
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            Verb = "runas" // Run as administrator (optional)
        };

        process.StartInfo = psi;
        process.Start();

        // Run 'pip install -r requirements.txt' in cmd
        string installCommand = $"{pythonExecutable} -m pip install -r \"{requirementsFilePath}\"";
        process.StandardInput.WriteLine(installCommand);

        // Close the cmd window
        process.StandardInput.WriteLine("exit");

        string output = process.StandardOutput.ReadToEnd();
        string errors = process.StandardError.ReadToEnd();

        process.WaitForExit();

        // Output results to Unity console
        if (!string.IsNullOrEmpty(output))
            UnityEngine.Debug.Log("Installation Output: " + output);

        if (!string.IsNullOrEmpty(errors))
            UnityEngine.Debug.LogError("Installation Errors: " + errors);

        process.Close();
    }
}
