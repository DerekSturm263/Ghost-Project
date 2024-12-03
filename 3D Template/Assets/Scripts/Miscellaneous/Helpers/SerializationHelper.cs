using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;

public static class SerializationHelper
{
    private static readonly byte[] Key =
    {
        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
        0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
    };

    public static bool Save<T>(T item, string directory, string fileName) where T : struct
    {
        string json = ToJSON(item);

        CreateDirectory(directory);

        FileMode writeMode = File.Exists(directory + fileName) ? FileMode.Truncate : FileMode.Create;
        using FileStream stream = new(directory + fileName, writeMode);

        using Aes aes = Aes.Create();
        aes.Key = Key;

        byte[] iv = aes.IV;
        stream.Write(iv, 0, iv.Length);

        using CryptoStream crypto = new(stream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using StreamWriter writer = new(crypto);

        writer.Write(json);
        writer.Flush();

        Debug.Log($"{nameof(T)} has been successfully saved as: {json}");
        return true;
    }

    public static T Load<T>(T defaultT, string directory, string fileName) where T : struct
    {
        CreateDirectory(directory);

        if (File.Exists(directory + fileName))
        {
            using FileStream stream = new(directory + fileName, FileMode.Open);

            using Aes aes = Aes.Create();

            byte[] iv = new byte[aes.IV.Length];
            int numBytesToRead = aes.IV.Length;
            int numBytesRead = 0;

            while (numBytesToRead > 0)
            {
                int n = stream.Read(iv, numBytesRead, numBytesToRead);
                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }

            using CryptoStream crypto = new(stream, aes.CreateDecryptor(Key, iv), CryptoStreamMode.Read);
            using StreamReader reader = new(crypto);

            string json = reader.ReadToEnd();
            T item = FromJSON<T>(json);

            Debug.Log($"{nameof(T)} has been successfully loaded as: {json}");
            return item;
        }
        else
        {
            Debug.Log($"{nameof(T)} could not be loaded. Returning the default: {ToJSON(defaultT)}");
            return defaultT;
        }
    }

    public static void Delete(string filePath, string directory)
    {
        CreateDirectory(directory);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Item was successfully deleted");
        }
        else
        {
            Debug.Log("Item could not be deleted, as it was not found");
        }
    }

    private static string ToJSON<T>(T item) where T : struct => JsonUtility.ToJson(item, true);
    private static T FromJSON<T>(string json) where T : struct => JsonUtility.FromJson<T>(json);

    public static IEnumerable<T> LoadAllFromDirectory<T>(string directory) where T : struct
    {
        CreateDirectory(directory);
        return Directory.EnumerateFiles(directory, "*.json").Select(filePath => Load<T>(default, filePath, directory));
    }

    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}
