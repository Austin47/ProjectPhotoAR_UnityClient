using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.EditorHelpers
{
    public class Builder
    {
        [MenuItem("Tools/AutoBuilder/Android")]
        public static void BuildDevForAndroid()
        {
            // Temporarily comment out code so test fails
            // List<string> scenes = new List<string>();
            // foreach (var s in EditorBuildSettings.scenes)
            // {
            //     scenes.Add(s.path);
            // }
            // string buildPath = "Builds/Android/Development";
            // if (!Directory.Exists(buildPath))
            //     Directory.CreateDirectory(buildPath);

            // string buildName = $"v{PlayerSettings.bundleVersion}_b{PlayerSettings.Android.bundleVersionCode}.apk";    
            // BuildPipeline.BuildPlayer(scenes.ToArray(), $"{buildPath}/{buildName}", BuildTarget.Android, BuildOptions.Development);
        }
    }
}

