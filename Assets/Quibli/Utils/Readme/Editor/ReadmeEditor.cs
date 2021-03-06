using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Rendering;

namespace Quibli {
[CustomEditor(typeof(Readme))]
public class ReadmeEditor : Editor {
    private static readonly GUID UrpPipelineAssetGuid = new GUID("524f0a41e9b5f451b98453c3fad05721");

    private Readme _readme;
    private bool _showingVersionMessage;
    private string _versionLatest;

    private bool _showingClearCacheMessage;
    private bool _cacheClearedSuccessfully;

    private void OnEnable() {
        _readme = serializedObject.targetObject as Readme;
        if (_readme == null) {
            Debug.LogError("[Quibli] Readme error.");
            return;
        }

        _readme.Refresh();
        _showingVersionMessage = false;
        _showingClearCacheMessage = false;
        _versionLatest = null;
    }

    public override void OnInspectorGUI() {
        {
            EditorGUILayout.LabelField("Quibli", EditorStyles.boldLabel);
            DrawUILine(Color.gray, 1, 0);
            EditorGUILayout.LabelField($"Version {_readme.AssetVersion}", EditorStyles.miniLabel);
            EditorGUILayout.Separator();
        }

        if (GUILayout.Button("Documentation")) {
            OpenDocumentation();
        }

        {
            if (_showingVersionMessage) {
                EditorGUILayout.Space(20);

                if (_versionLatest == null) {
                    EditorGUILayout.HelpBox($"Checking the latest version...", MessageType.None);
                } else {
                    var local = Version.Parse(_readme.AssetVersion);
                    var remote = Version.Parse(_versionLatest);
                    if (local >= remote) {
                        EditorGUILayout.HelpBox($"You have the latest version! {_readme.AssetVersion}.",
                                                MessageType.Info);
                    } else {
                        EditorGUILayout
                            .HelpBox($"Update needed. " + $"The latest version is {_versionLatest}, but you have {_readme.AssetVersion}.",
                                     MessageType.Warning);
                    }
                }
            }

            if (GUILayout.Button("Check for updates")) {
                _showingVersionMessage = true;
                _versionLatest = null;
                CheckVersion();
            }

            if (_showingVersionMessage) {
                EditorGUILayout.Space(20);
            }
        }

        {
            if (!string.IsNullOrEmpty(_readme.PackageManagerError)) {
                EditorGUILayout.Separator();
                DrawUILine(Color.yellow, 1, 0);
                EditorGUILayout.HelpBox($"Package Manager error: {_readme.PackageManagerError}", MessageType.Warning);
                DrawUILine(Color.yellow, 1, 0);
            }
        }

        {
            DrawUILine(Color.gray, 1, 20);
            EditorGUILayout.LabelField("Graphics and Quality settings", EditorStyles.label);

            if (GUILayout.Button("Use Quibli's example URP settings", EditorStyles.miniButtonLeft)) {
                ConfigureUrp();
            }
        }

        {
            DrawUILine(Color.gray, 1, 20);
            EditorGUILayout.LabelField("Open support ticket", EditorStyles.label);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("On Trello")) {
                OpenSupportTicketTrello();
            }

            if (GUILayout.Button("On GitHub")) {
                OpenSupportTicketGitHub();
            }

            GUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Please copy the debug info below and paste it in the ticket.",
                                       EditorStyles.miniLabel);
        }

        {
            DrawUILine(Color.gray, 1, 20);
            EditorGUILayout.LabelField("Package Manager", EditorStyles.label);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Clear cache")) {
                ClearPackageCache();
            }

            if (GUILayout.Button("Select Quibli")) {
                OpenPackageManager();
            }

            GUILayout.EndHorizontal();

            if (GUILayout.Button("Reimport Quibli files")) {
                ReimportAsset();
            }

            if (_showingClearCacheMessage) {
                if (_cacheClearedSuccessfully) {
                    EditorGUILayout
                        .HelpBox($"Successfully removed cached packages. \nPlease re-download Quibli in the Package Manager.",
                                 MessageType.Info);
                } else {
                    EditorGUILayout.HelpBox($"Could not find or clear package cache. It might be already cleared.",
                                            MessageType.Warning);
                }
            }
        }

        {
            DrawUILine(Color.gray, 1, 20);
            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Debug info", EditorStyles.miniBoldLabel);

            if (GUILayout.Button("Copy", EditorStyles.miniButtonLeft)) {
                CopyDebugInfoToClipboard();
                EditorUtility.DisplayDialog("Quibli",
                                            "Debug info copied to clipboard, please paste it in the support ticket.",
                                            "OK");
            }

            GUILayout.EndHorizontal();
            var debugInfo = GetDebugInfo();
            foreach (var s in debugInfo) {
                EditorGUILayout.LabelField($"    " + s, EditorStyles.miniLabel);
            }

            EditorGUILayout.Separator();
        }
    }

    private string[] GetDebugInfo() {
        var info = new List<string>
        {
            $"Unity {_readme.UnityVersion}",
            $"Dev platform: {Application.platform}",
            $"Target platform: {EditorUserBuildSettings.activeBuildTarget}",
            $"URP installed: {_readme.UrpInstalled}, version {_readme.UrpVersionInstalled}",
            $"Render pipeline: {Shader.globalRenderPipeline}"
        };

        var qualityConfig = QualitySettings.renderPipeline == null ? "N/A" : QualitySettings.renderPipeline.name;
        info.Add($"Quality config: {qualityConfig}");

        var graphicsConfig = GraphicsSettings.currentRenderPipeline == null
            ? "N/A"
            : GraphicsSettings.currentRenderPipeline.name;
        info.Add($"Graphics config: {graphicsConfig}");

        return info.ToArray();
    }

    private void OpenPackageManager() {
        Client.Resolve();
        const string packageName = "Quibli: Anime Shaders and Tools";
        UnityEditor.PackageManager.UI.Window.Open(packageName);

        /*
        var request = Client.Add(packageName);
        while (!request.IsCompleted) System.Threading.Tasks.Task.Delay(100);
        if (request.Status != StatusCode.Success) Debug.LogError("Cannot import Quibli: " + request.Error.message);
        */
    }

    private void ReimportAsset() {
        const string rootGuid = "373fc0c4abc8d413a98a36590c6e8860";
        var assetRoot = AssetDatabase.GUIDToAssetPath(rootGuid);
        if (string.IsNullOrEmpty(assetRoot)) {
            EditorUtility.DisplayDialog("Quibli",
                                        "Could not find the root asset folder. Please re-import from the Package Manager.",
                                        "OK");
        } else {
            AssetDatabase.ImportAsset(assetRoot, ImportAssetOptions.ImportRecursive);
            EditorUtility.DisplayDialog("Quibli", "Successfully re-imported the root asset folder.", "OK");
        }
    }

    private void ClearPackageCache() {
        string path = string.Empty;
        // TODO: Use UPM_CACHE_ROOT.
        if (Application.platform == RuntimePlatform.OSXEditor) {
            path = "~/Library/Unity/Asset Store-5.x/Dustyroom/";
        }

        if (Application.platform == RuntimePlatform.LinuxEditor) {
            path = "~/.local/share/unity3d/Asset Store-5.x/Dustyroom/";
        }

        if (Application.platform == RuntimePlatform.WindowsEditor) {
            // This wouldn't understand %APPDATA%.
            path =
                Application.persistentDataPath
                    .Substring(0, Application.persistentDataPath.IndexOf("AppData", StringComparison.Ordinal)) +
                "/AppData/Roaming/Unity/Asset Store-5.x/Dustyroom";
        }

        if (path == string.Empty) return;

        _cacheClearedSuccessfully |= FileUtil.DeleteFileOrDirectory(path);
        _showingClearCacheMessage = true;

        OpenPackageManager();
    }

    private void ConfigureUrp() {
        string path = AssetDatabase.GUIDToAssetPath(UrpPipelineAssetGuid.ToString());
        if (path == null) {
            Debug.LogError("<b>[Quibli]</b> Couldn't find the URP pipeline asset. " +
                           "Have you unpacked the URP package?");
            return;
        }

        var pipelineAsset = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>(path);
        if (pipelineAsset == null) {
            Debug.LogError("<b>[Quibli]</b> Couldn't load the URP pipeline asset.");
            return;
        }

        Debug.Log("<b>[Quibli]</b> Set the render pipeline asset in the Graphics settings " +
                  "to the bundled example.");
        GraphicsSettings.renderPipelineAsset = pipelineAsset;
        GraphicsSettings.defaultRenderPipeline = pipelineAsset;

        ChangePipelineAssetAllQualityLevels(pipelineAsset);
    }

    private void ChangePipelineAssetAllQualityLevels(RenderPipelineAsset pipelineAsset) {
        var originalQualityLevel = QualitySettings.GetQualityLevel();

        var logString = "<b>[Quibli]</b> Set the render pipeline asset for the quality levels:";

        for (int i = 0; i < QualitySettings.names.Length; i++) {
            logString += $"\n\t{QualitySettings.names[i]}";
            QualitySettings.SetQualityLevel(i, false);
            QualitySettings.renderPipeline = pipelineAsset;
        }

        Debug.Log(logString);

        QualitySettings.SetQualityLevel(originalQualityLevel, false);
    }

    private void CheckVersion() {
        NetworkManager.GetVersion(version => { _versionLatest = version; });
    }

    private void CopyDebugInfoToClipboard() {
        var debugInfo = GetDebugInfo();
        var s = String.Join("\n", debugInfo);
        EditorGUIUtility.systemCopyBuffer = s;
    }

    private void OpenSupportTicketGitHub() {
        Application.OpenURL("https://github.com/dustyroom-studio/quibli-doc/issues/new/choose");
    }

    private void OpenSupportTicketTrello() {
        Application.OpenURL("https://trello.com/b/tOhjxOib/quibli-support");
    }

    private void OpenDocumentation() {
        Application.OpenURL("https://quibli.dustyroom.com/");
    }

    private static void DrawUILine(Color color, int thickness = 2, int padding = 10) {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += padding / 2f;
        r.x -= 2;
        EditorGUI.DrawRect(r, color);
    }
}
}