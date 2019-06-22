using System;

namespace Infrastructure.DatabaseService
{
    public interface IDatabase
    {
        void Load<T>(string url, Action<T> callback);
        void Save<T>(string url);
    }
}


