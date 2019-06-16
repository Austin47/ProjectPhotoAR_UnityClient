using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.EditorHelpers
{
    //TODO: AT - can definitely be broken up
    // Will refactor if code is needed in other editor scripts
    public class CreateZenjectStructure : EditorWindow
    {
        private string scriptName = "PlaceHolder";
        private string nameSpace = "Infrastructure.Domain.Presentation";

        [MenuItem("Assets/Create/Zenject/CreatePlaceHolderStructre")]
        private static void CreatePlaceHolderStructre()
        {
            ShowUI();
        }

        private static void ShowUI()
        {
            CreateZenjectStructure window = ScriptableObject.CreateInstance<CreateZenjectStructure>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 150);
            window.ShowUtility();
        }

        private static void CreateStructure(string name, string nameSpace)
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrWhiteSpace(path))
            {
                Debug.LogWarning("Need a root folder selected to create");
                return;
            }

            Debug.LogFormat("Making scripts => {0} With NameSpace => {1}", name, nameSpace);

            MakeFolder(path, "Tests");

            MakeInstallerScript(name, nameSpace, MakeFolder(path, "Installers"));
            MakeInstallsScript(name, nameSpace, MakeFolder(path, "Installs"));
            MakeInterfaceScript(name, nameSpace, MakeFolder(path, "Interfaces"));
            MakeTestScript(name, nameSpace, MakeFolder($"{path}/Tests", "Editor"));
            AssetDatabase.Refresh();
        }

        private static string MakeFolder(string path, string folderName)
        {
            if (Directory.Exists($"{path}/{folderName}")) return $"{path}/{folderName}";
            var guid = AssetDatabase.CreateFolder(path, folderName);
            return AssetDatabase.GUIDToAssetPath(guid);
        }

        private void OnGUI()
        {
            scriptName = EditorGUILayout.TextField("Enter Script Name", scriptName);
            GUILayout.Space(1);
            nameSpace = EditorGUILayout.TextField("Enter namespace", nameSpace);
            GUILayout.Space(10);
            if (GUILayout.Button("Create Structure"))
            {
                Close();
                CreateStructure(scriptName, nameSpace);
            }

            GUILayout.Space(10);
            if (GUILayout.Button("Cancel")) Close();
        }

        private static void MakeInstallerScript(string name, string nameSpace, string path)
        {
            name = $"{name}Installer";
            string combinedPath = CombinePath(name, path);

            if (File.Exists(combinedPath)) return;

            using (StreamWriter outfile =
                new StreamWriter(combinedPath))
            {
                outfile.WriteLine("using Zenject;");
                outfile.WriteLine("");
                outfile.WriteLine($"namespace {nameSpace}");
                outfile.WriteLine("{");
                outfile.WriteLine($"    public class {name} : MonoInstaller ");
                outfile.WriteLine("    {");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
                outfile.WriteLine("");
                outfile.WriteLine("");
            }
        }

        public static void MakeInstallsScript(string name, string nameSpace, string path)
        {
            string combinedPath = CombinePath(name, path);

            if (File.Exists(combinedPath)) return;

            using (StreamWriter outfile =
                new StreamWriter(combinedPath))
            {
                outfile.WriteLine("");
                outfile.WriteLine($"namespace {nameSpace}");
                outfile.WriteLine("{");
                outfile.WriteLine($"    public class {name} : I{name} ");
                outfile.WriteLine("    {");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
                outfile.WriteLine("");
                outfile.WriteLine("");
            }
        }

        private static void MakeInterfaceScript(string name, string nameSpace, string path)
        {
            name = $"I{name}";
            string combinedPath = CombinePath(name, path);

            if (File.Exists(combinedPath)) return;

            using (StreamWriter outfile =
                new StreamWriter(combinedPath))
            {
                outfile.WriteLine("");
                outfile.WriteLine($"namespace {nameSpace}");
                outfile.WriteLine("{");
                outfile.WriteLine($"    public interface {name}");
                outfile.WriteLine("    {");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
                outfile.WriteLine("");
                outfile.WriteLine("");
            }
        }

        private static void MakeTestScript(string name, string nameSpace, string path)
        {
            name = $"{name}Test";
            string combinedPath = CombinePath(name, path);

            if (File.Exists(combinedPath)) return;

            using (StreamWriter outfile =
                new StreamWriter(combinedPath))
            {
                outfile.WriteLine("using Zenject;");
                outfile.WriteLine("");
                outfile.WriteLine($"namespace {nameSpace}.Tests");
                outfile.WriteLine("{");
                outfile.WriteLine($"    public class {name}");
                outfile.WriteLine("    {");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
                outfile.WriteLine("");
                outfile.WriteLine("");
            }
        }

        private static string CombinePath(string name, string path)
        {
            name = name.Replace(" ", "_");
            name = name.Replace("-", "_");
            return $"{path}/{name}.cs";
        }
    }
}


