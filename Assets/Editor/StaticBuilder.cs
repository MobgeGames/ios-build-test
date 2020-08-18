using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Mobge.HyperCasualSetup {
    public static class StaticBuilder {
        public static void Build() {
            PlayerSettings.SplashScreen.showUnityLogo = false;
            // LevelSetAddressablePreBuildProcessor.PreExport();
    
            var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
            var args = Environment.GetCommandLineArgs();
            var buildTarget = BuildTarget.NoTarget;
            var outputPath = "";
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] == "-buildTarget")
                {
                    switch (args[i + 1])
                    {
                        case "Android":
                            buildTarget = BuildTarget.Android;
                            break;
                        case "iOS":
                            buildTarget = BuildTarget.iOS;
                            break;
                        case "Mac":
                            buildTarget = BuildTarget.StandaloneOSX;
                            break;
                        case "Windows":
                            buildTarget = BuildTarget.StandaloneWindows;
                            break;
                        case "WebGL":
                            buildTarget = BuildTarget.WebGL;
                            break;
                        default:
                            Debug.LogError($"{args[i + 1]} is not supported.");
                            EditorApplication.Exit(1);
                            break;
                    }
                }
                if (args[i] == "-outputPath")
                {
                    outputPath = args[i + 1];
                }
            }
            BuildPipeline.BuildPlayer(scenes, outputPath, buildTarget, BuildOptions.None);
            Debug.Log("Build Completed!");
            Debug.Log($"Output Path: {outputPath}");
            DirectoryTree.PrintDirectoryTree(outputPath);
        }
    }
}

