using System.Windows;
using UsersApp.Models;

namespace UsersApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static User? CurrentUser { get; set; } = null;
    public static List<User>? Users { get; set; } = null;
}
