using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private MedicalHistory currentMedicalHistory = new MedicalHistory();
        public AddEditPage(MedicalHistory SelectedMedicalHistory)
        {
            InitializeComponent();

            if (SelectedMedicalHistory != null)
                currentMedicalHistory = SelectedMedicalHistory;

            DataContext = currentMedicalHistory;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            
            if (currentMedicalHistory.ID == 0) errors.AppendLine("Укажите учетный номер");
            if (currentMedicalHistory.IdPatient == 0) errors.AppendLine("Укажите номер пациента");
            if (string.IsNullOrWhiteSpace(currentMedicalHistory.Diagnosis)) errors.AppendLine("Укажите диагноз");
            if (currentMedicalHistory.IdDoctor == 0) errors.AppendLine("Укажите номер врача");
            if (currentMedicalHistory.TypeTreatment == null) errors.AppendLine("Выберите тип лечения");
            if (string.IsNullOrEmpty(currentMedicalHistory.DateDisease) ||
                !DateTime.TryParseExact(currentMedicalHistory.DateDisease, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                errors.AppendLine("Введите корректную дату поступления в формате дд.мм.гггг");
            }
            if (string.IsNullOrEmpty(currentMedicalHistory.DateCure) ||
                !DateTime.TryParseExact(currentMedicalHistory.DateCure, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                errors.AppendLine("Введите корректную дату выписки в формате дд.мм.гггг");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var allMedicalHistory = HospitalEntities.GetContext().MedicalHistory.ToList();
            allMedicalHistory = allMedicalHistory.Where(p => p.ID == currentMedicalHistory.ID).ToList();

            using (var context = HospitalEntities.GetContext())
            {
                // check теккущ
                var existingMedicalHistory = context.MedicalHistory.FirstOrDefault(p => p.ID == currentMedicalHistory.ID);

                if (existingMedicalHistory == null) // Добавить
                {
                    context.MedicalHistory.Add(currentMedicalHistory);
                }
                
                try
                {
                    HospitalEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        
    }
}