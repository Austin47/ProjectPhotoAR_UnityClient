using System;
using UnityEngine;

namespace Infrastructure.DatabaseService
{
    public interface IDatabase
    {
        void LoadFromStreamAssets<T>(string url, Action<T> callback);
        void LoadTextureFromStringmAssets(string url, Action<Texture2D> callback);
        void LoadTextureFromLocalApp(string url, Action<Texture2D> callback);
        void LoadTextureFromGallery(string url, Action<Texture2D> callback);
        void DeleteTextureFromLocalApp(string url);
        bool TextureFromLocalAppExist(string url);
        int GetLocalTexterCount();
        void SavePathToLocal(string url);
    }
}


