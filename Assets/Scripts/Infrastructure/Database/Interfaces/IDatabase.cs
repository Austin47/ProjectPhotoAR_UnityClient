using System;
using UnityEngine;

namespace Infrastructure.DatabaseService
{
    public interface IDatabase
    {
        void Load<T>(string url, Action<T> callback);
        void LoadDefaultTexture(string url, Action<Texture2D> callback);
        void LoadCustomTexture(string url, Action<Texture2D> callback);
        void Save<T>(string url);
    }
}


