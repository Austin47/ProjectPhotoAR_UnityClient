using UnityEngine;

namespace Infrastructure.Common
{


    public class FilePath
    {
        private const string FilesHeader = "file://";

        public static string StreamingAssets
        {
            get
            {
#if UNITY_EDITOR
                return $"{FilesHeader}{Application.streamingAssetsPath}";
#elif UNITY_ANDROID
            return Application.streamingAssetsPath;
#else
            return Application.streamingAssetsPath;
#endif
            }
        }

        public static string Local
        {
            get
            {
#if UNITY_EDITOR
                return $"{FilesHeader}{LocalNoHeader}";
#elif UNITY_ANDROID
            return $"{FilesHeader}{LocalNoHeader}";
#else
            return LocalNoHeader;
#endif
            }
        }

        public static string LocalNoHeader
        {
            get
            {
#if UNITY_EDITOR
                return Application.persistentDataPath;
#elif UNITY_ANDROID
            return Application.persistentDataPath;
#else
            return Application.persistentDataPath;
#endif
            }
        }

        public static string Gallery = "TODO";
    }
}