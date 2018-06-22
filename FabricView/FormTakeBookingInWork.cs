
using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FabricView
{
    public partial class FormTakeBookingInWork : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormTakeBookingInWork()
        {
            InitializeComponent();
        }

        private void FormTakeBookingInWork_Load(object sender, EventArgs e)
        {
            try
            {
                if (!id.HasValue)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                List<ExecuterViewModel> list = Task.Run(() => APIClient.GetRequestData<List<ExecuterViewModel>>("api/Executer/GetList")).Result;
                if (list != null)
                {
                    comboBoxExecuter.DisplayMember = "ExecuterFIO";
                    comboBoxExecuter.ValueMember = "Id";
                    comboBoxExecuter.DataSource = list;
                    comboBoxExecuter.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxExecuter.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int ExecuterId = Convert.ToInt32(comboBoxExecuter.SelectedValue);
                Task task = Task.Run(() => APIClient.PostRequestData("api/General/TakeBookingInWork", new BookingBindingModel
                {
                    Id = id.Value,
                    ExecuterId = ExecuterId
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Заказ передан в работу. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
                    TaskContinuationOptions.OnlyOnRanToCompletion);
                task.ContinueWith((prevTask) =>
                {
                    var ex = (Exception)prevTask.Exception;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }, TaskContinuationOptions.OnlyOnFaulted);

                Close();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
