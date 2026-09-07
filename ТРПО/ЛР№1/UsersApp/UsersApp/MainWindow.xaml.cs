using System.Windows;
using UsersApp.Services;

namespace UsersApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AuthorizationMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (App.Users is null)
        {
            MessageBox.Show("Для авторизации необходимо, чтобы пользователи были загружены", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        Window window = new AuthorizationWindow();
        window.ShowDialog();
        if (App.CurrentUser is not null)
            CollapsMenuItems();
        ShowUsers();
    }

    private void RegistrationMenuItem_Click(object sender, RoutedEventArgs e)
    {
        Window window = new RegistrationWindow();
        window.ShowDialog();

        if (App.CurrentUser is not null)
        {
            App.Users ??= new();
            App.Users.Add(App.CurrentUser);
            CollapsMenuItems();
            ShowUsers();
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
    }

    private void ExportMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (App.Users is null)
        {
            MessageBox.Show("Для выгрузки необходимо,чтобы в списке был хотя бы один пользователь.", "Экспорт данных", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (DataManageService.ExportUsers(App.Users))
            MessageBox.Show("Данные успешно выгружены.", "Экспорт данных", MessageBoxButton.OK, MessageBoxImage.Information);
        else
            MessageBox.Show("При выгрузке данных возникла ошибка", "Экспорт данных", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void ImportMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var users = DataManageService.ImportUsers();

        if (users is null)
        {
            MessageBox.Show("При загрузке возникла ошибка", "Импорт данных", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        App.Users ??= new();
        App.Users.AddRange(users);
        MessageBox.Show("Данные успешно загружены.", "Импорт данных", MessageBoxButton.OK, MessageBoxImage.Information);
        ShowUsers();
    }

    private void CollapsMenuItems()
    {
        AuthorizationMenuItem.Visibility = Visibility.Collapsed;
        RegistrationMenuItem.Visibility = Visibility.Collapsed;
    }

    private void ShowUsers()
    {
        if (App.Users is null)
            return;

        UsersDataGrid.ItemsSource = null;

        UsersDataGrid.ItemsSource = App.Users;
    }
}