using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для MedicalHistoryPage.xaml
    /// </summary>
    public partial class MedicalHistoryPage : Page
    {
        int CountRecords; // Количество записей в таблице
        int CountPage; // Общее количество страниц
        int CurrentPage = 0; // Текущая страница
        List<MedicalHistory> CurrentPageList = new List<MedicalHistory>();
        List<MedicalHistory> TableList;

        public MedicalHistoryPage()
        {
            InitializeComponent();
            var currentMedicalHistory = HospitalEntities.GetContext().MedicalHistory.ToList();
            MedicalHistoryListView.ItemsSource = currentMedicalHistory;
        }

        private void UpdateMedicalHistory()
        {
            var currentMedicalHistory = HospitalEntities.GetContext().MedicalHistory.ToList();
                       
            if (ComboType.SelectedIndex == 2)
            {
                currentMedicalHistory = currentMedicalHistory.Where(p => (p.TypeTreatment == "Амбулаторное")).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentMedicalHistory = currentMedicalHistory.Where(p => (p.TypeTreatment == "Стационарное")).ToList();
            }
                        

            currentMedicalHistory = currentMedicalHistory.Where(p => p.ID.ToString().Contains(TBoxSearch.Text.ToLower()) || 
            p.Patient.Surname.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
            p.Patient.Name.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
            p.Patient.Patronymic.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();


            if (RButtonDown.IsChecked.Value)
            {
                currentMedicalHistory = currentMedicalHistory.OrderBy(p => p.DateCure).ToList();
            }

            if (RButtonUP.IsChecked.Value)
            {
                currentMedicalHistory = currentMedicalHistory.OrderBy(p => p.DateDisease).ToList();
            }

            MedicalHistoryListView.ItemsSource = currentMedicalHistory;

            TableList = currentMedicalHistory;
            ChangePage(0, 0);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as MedicalHistory));
            UpdateMedicalHistory();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var currentMedicalHistory = (sender as Button).DataContext as MedicalHistory;
                        
            if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HospitalEntities.GetContext().MedicalHistory.Remove(currentMedicalHistory);
                    HospitalEntities.GetContext().SaveChanges();
                    MedicalHistoryListView.ItemsSource = HospitalEntities.GetContext().MedicalHistory.ToList();
                    UpdateMedicalHistory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            UpdateMedicalHistory();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
            UpdateMedicalHistory();
        }

        private void ChangePage(int direction, int? selectedPage)
        {
            try
            {
                CurrentPageList.Clear();
                CountRecords = TableList.Count;

                // Вычисление количества страниц
                CountPage = (CountRecords + 9) / 10; // Упрощенное вычисление количества страниц

                bool isUpdateNeeded = true;
                int min;

                if (selectedPage.HasValue)
                {
                    if (selectedPage >= 0 && selectedPage < CountPage) // Исправлено на < CountPage
                    {
                        CurrentPage = (int)selectedPage;
                        min = Math.Min(CurrentPage * 10 + 10, CountRecords); // Используем Math.Min
                        for (int i = CurrentPage * 10; i < min; i++)
                        {
                            CurrentPageList.Add(TableList[i]);
                        }
                    }
                    else
                    {
                        isUpdateNeeded = false; // Если выбранная страница не валидна
                    }
                }
                else
                {
                    switch (direction)
                    {
                        case 1: // лево руля
                            if (CurrentPage > 0)
                            {
                                CurrentPage--;
                            }
                            else
                            {
                                isUpdateNeeded = false;
                            }
                            break;

                        case 2: // право
                            if (CurrentPage < CountPage - 1)
                            {
                                CurrentPage++;
                            }
                            else
                            {
                                isUpdateNeeded = false;
                            }
                            break;
                    }

                    // Обновление списка текущей страницы
                    if (isUpdateNeeded)
                    {
                        min = Math.Min(CurrentPage * 10 + 10, CountRecords);
                        for (int i = CurrentPage * 10; i < min; i++)
                        {
                            CurrentPageList.Add(TableList[i]);
                        }
                    }
                }

                if (isUpdateNeeded)
                {
                    PageListBox.Items.Clear();
                    for (int i = 1; i <= CountPage; i++)
                    {
                        PageListBox.Items.Add(i);
                    }

                    PageListBox.SelectedIndex = CurrentPage;

                    min = Math.Min(CurrentPage * 10 + 10, CountRecords);
                    TBCount.Text = min.ToString();
                    TBAllRecords.Text = " из " + CountRecords.ToString();

                    MedicalHistoryListView.ItemsSource = CurrentPageList;
                    MedicalHistoryListView.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // или логировать ошибку йцуйцкцу
            }
        }



        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
                {
                    ChangePage(1, null);
                }

                private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
                {
                    ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
                }

                private void RightDirButton_Click(object sender, RoutedEventArgs e)
                {
                    ChangePage(2, null);
                }

                private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
                {
                    UpdateMedicalHistory();
                }

                private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                    UpdateMedicalHistory();
                }

                private void RButtonUP_Checked(object sender, RoutedEventArgs e)
                {
                    UpdateMedicalHistory();
                }

                private void RButtonDown_Checked(object sender, RoutedEventArgs e)
                {
                    UpdateMedicalHistory();
                }
            }
}
