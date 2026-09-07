using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using UsersApp.Models;

namespace UsersApp.Services;

public static class DataManageService
{
    public static List<User>? ImportUsers()
    {
        OpenFileDialog dialog = new();
        dialog.Filter = "JSON|*.json";

        if (dialog.ShowDialog() == true)
        {
            var file = new FileInfo(dialog.FileName);

            if (file.Exists && file.Extension == ".json")
            {
                string body = File.ReadAllText(file.FullName);
                return JsonSerializer.Deserialize<List<User>>(body);
            }
        }

        return null;
    }

    public static bool ExportUsers(List<User> users)
    {
        SaveFileDialog dialog = new();
        dialog.Filter = "JSON|*.json";

        if (dialog.ShowDialog() == true)
        {
            var file = new FileInfo(dialog.FileName);
            var json = JsonSerializer.Serialize(users);

            if (json is not null)
            {
                File.WriteAllText(file.FullName, json);
                return true;
            }
        }

        return false;
    }
}
