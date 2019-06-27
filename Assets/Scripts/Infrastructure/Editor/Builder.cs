using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Infrastructure.EditorHelpers
{
    public class Builder
    {
        [MenuItem("Tools/AutoBuilder/Android")]
        public static void BuildDevForAndroid()
        {
            List<string> scenes = new List<string>();
            foreach (var s in EditorBuildSettings.scenes)
            {
                scenes.Add(s.path);
            }
            string buildPath = "Builds/Android/Development";
            if (!Directory.Exists(buildPath))
                Directory.CreateDirectory(buildPath);

            string buildName = GetBuildName();
            var result = BuildPipeline.BuildPlayer(scenes.ToArray(), $"{buildPath}/{buildName}", BuildTarget.Android, BuildOptions.Development);

            // HACK: Small Hack so we don't exit the editor on local machines 
            if (string.IsNullOrWhiteSpace(GetArg("buildName")))
                return;
            if (result.summary.result == BuildResult.Succeeded)
                EditorApplication.Exit(0);
            else
                EditorApplication.Exit(1);
        }

        private static string GetBuildName()
        {
            string name = GetArg("buildName");
            return !string.IsNullOrWhiteSpace(name) ? name : $"v{PlayerSettings.bundleVersion}_b{PlayerSettings.Android.bundleVersionCode}.apk";
        }

        // source: https://stackoverflow.com/questions/39843039/game-development-how-could-i-pass-command-line-arguments-to-a-unity-standalo#answer-45578115
        private static string GetArg(string name)
        {
            var args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == name && args.Length > i + 1)
                {
                    return args[i + 1];
                }
            }
            return null;
        }
    }
}

