using System.Text;
using System.Windows;

namespace LabWindow;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void RegistrationButton_Click(object sender, RoutedEventArgs e)
    {
        StringBuilder errors = new();
        if (string.IsNullOrWhiteSpace(LoginTextBox.Text))
            errors.AppendLine("Поле 'Логин' не должно быть пустым.");
        if (string.IsNullOrWhiteSpace(PasswordTextBox.Text))
            errors.AppendLine("Поле 'Пароль' не должно быть пустым.");
        if (string.IsNullOrWhiteSpace(ConfirmPasswordTextBox.Text))
            errors.AppendLine("Поле 'Подтверждение пароля' не должно быть пустым.");
        if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            errors.AppendLine("Поле 'Email' не должно быть пустым.");

        if (errors.Length > 0)
        {
            MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (PasswordTextBox.Text != ConfirmPasswordTextBox.Text)
            errors.AppendLine("Подтверждение пароля и парроль не совпадают.");

        if (errors.Length > 0)
        {
            MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}