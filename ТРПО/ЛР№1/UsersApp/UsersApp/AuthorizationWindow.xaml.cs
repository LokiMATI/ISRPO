using System.Windows;

namespace UsersApp;

/// <summary>
/// Логика взаимодействия для AuthorizationWindow.xaml
/// </summary>
public partial class AuthorizationWindow : Window
{
    public AuthorizationWindow()
    {
        InitializeComponent();
    }

    private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
    {
        var user = App.Users.FirstOrDefault(u => u.UserName == UsernameTextBox.Text);
        if (user is not null)
        {
            if (user.Password == PasswordTextBox.Text)
            {
                App.CurrentUser = user;
                Close();
            }
            else
                MessageBox.Show("Неверный пароль", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        else
            MessageBox.Show("Неверное имя пользователя", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);

    }
}
