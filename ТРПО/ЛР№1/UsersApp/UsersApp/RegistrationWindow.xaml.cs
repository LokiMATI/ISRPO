using System.Text;
using System.Windows;
using UsersApp.Models;

namespace UsersApp;

/// <summary>
/// Логика взаимодействия для RegistrationWindow.xaml
/// </summary>
public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        InitializeComponent();
    }

    private void RegistrationButton_Click(object sender, RoutedEventArgs e)
    {
        StringBuilder sb = new();
        if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            sb.AppendLine("Имя пользователя не должно быть пустым.");

        if (string.IsNullOrWhiteSpace(PasswordTextBox.Text))
            sb.AppendLine("Пароль не должен быть пустым.");

        if (sb.Length > 0)
        {
            MessageBox.Show(sb.ToString(), "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        App.CurrentUser = new()
        {
            UserName = UsernameTextBox.Text,
            Password = PasswordTextBox.Text
        };

        Close();
    }
}
