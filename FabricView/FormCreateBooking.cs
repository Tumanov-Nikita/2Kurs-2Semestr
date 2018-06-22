using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FabricView
{
    public partial class FormCreateBooking : Form
    {
        public FormCreateBooking()
        {
            InitializeComponent();
        }

        private void FormCreateBooking_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerViewModel> listC = Task.Run(() => APIClient.GetRequestData<List<CustomerViewModel>>("api/Customer/GetList")).Result;
                if (listC != null)
                {
                    comboBoxCustomer.DisplayMember = "CustomerFIO";
                    comboBoxCustomer.ValueMember = "Id";
                    comboBoxCustomer.DataSource = listC;
                    comboBoxCustomer.SelectedItem = null;
                }

                List<StuffViewModel> listP = Task.Run(() => APIClient.GetRequestData<List<StuffViewModel>>("api/Stuff/GetList")).Result;
                if (listP != null)
                {
                    comboBoxStuff.DisplayMember = "StuffName";
                    comboBoxStuff.ValueMember = "Id";
                    comboBoxStuff.DataSource = listP;
                    comboBoxStuff.SelectedItem = null;
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

        private void CalcSum()
        {
            if (comboBoxStuff.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxStuff.SelectedValue);
                    StuffViewModel Stuff = Task.Run(() => APIClient.GetRequestData<StuffViewModel>("api/Stuff/Get/" + id)).Result;
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * (int)Stuff.Cost).ToString();
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
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxStuff_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCustomer.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStuff.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int CustomerId = Convert.ToInt32(comboBoxCustomer.SelectedValue);
            int StuffId = Convert.ToInt32(comboBoxStuff.SelectedValue);
            int count = Convert.ToInt32(textBoxCount.Text);
            int sum = Convert.ToInt32(textBoxSum.Text);
            Task task = Task.Run(() => APIClient.PostRequestData("api/General/CreateBooking", new BookingBindingModel
            {
                CustomerId = CustomerId,
                StuffId = StuffId,
                Count = count,
                Cost = sum
            }));

            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
