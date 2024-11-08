using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace KanbanSimulator
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<ToDoItem> ToDoItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ToDoItems = new ObservableCollection<ToDoItem>();
            ToDoListView.ItemsSource = ToDoItems;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TaskTextBox.Text) || PriorityComboBox.SelectedItem == null || StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Omple tots els camps per crear la taska");
                return;
            }

            var newTask = new ToDoItem
            {
                Task = TaskTextBox.Text,
                Priority = ((ComboBoxItem)PriorityComboBox.SelectedItem).Content.ToString(),
                Status = ((ComboBoxItem)StatusComboBox.SelectedItem).Content.ToString()
            };

            ToDoItems.Add(newTask);
            TaskTextBox.Clear();
            PriorityComboBox.SelectedIndex = -1;
            StatusComboBox.SelectedIndex = -1;
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (ToDoListView.SelectedItem != null)
            {
                var selectedTask = (ToDoItem)ToDoListView.SelectedItem;
                ToDoItems.Remove(selectedTask);
            }
            else
            {
                MessageBox.Show("Selecciona una tasca per esborrar.");
            }
        }

        private void ModifyTask_Click(object sender, RoutedEventArgs e)
        {
            if (ToDoListView.SelectedItem != null)
            {
                if (PriorityComboBox.SelectedItem == null && StatusComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona una prioritat i un Status per modificar la tasca.");
                    return;
                }

                var editingTask = (ToDoItem)ToDoListView.SelectedItem;
                editingTask.Task = TaskTextBox.Text;
                editingTask.Priority = ((ComboBoxItem)PriorityComboBox.SelectedItem)?.Content.ToString();
                editingTask.Status = ((ComboBoxItem)StatusComboBox.SelectedItem)?.Content.ToString();

                ToDoListView.Items.Refresh();
                TaskTextBox.Clear();
                PriorityComboBox.SelectedIndex = -1;
                StatusComboBox.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Selecciona una tasca per modificar.");
            }
        }

        // New event handlers to resolve errors

        private void ToDoItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToDoListView.SelectedItem is ToDoItem selectedTask)
            {
                MessageBox.Show($"Tasca seleccionada: {selectedTask.Task}");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Clear();
        }

        private void PriorityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PriorityComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedPriority = selectedItem.Content.ToString();
                MessageBox.Show($"Prioritat seleccionada: {selectedPriority}");
            }
        }
        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedStatus = selectedItem.Content.ToString();
                MessageBox.Show($"Estat seleccionat: {selectedStatus}");
            }
        }

        public class ToDoItem
        {
            public string Task { get; set; }
            public string Priority { get; set; }
            public string Status { get; set; }
            public ObservableCollection<string> StatusOptions { get; } = new ObservableCollection<string> { "To Do", "Doing", "Done" };
        }
    }
}
