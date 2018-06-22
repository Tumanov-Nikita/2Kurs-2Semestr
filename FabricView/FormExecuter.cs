using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FabricView
{
    public partial class FormExecuter : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormExecuter()
        {
            InitializeComponent();
        }

        private void FormExecuter_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var Executer = Task.Run(() => APIClient.GetRequestData<ExecuterViewModel>("api/Executer/Get/" + id.Value)).Result;
                    textBoxFIO.Text = Executer.ExecuterFIO;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string fio = textBoxFIO.Text;
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Executer/UpdElement", new ExecuterBindingModel
                {
                    Id = id.Value,
                    ExecuterFIO = fio
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Executer/AddElement", new ExecuterBindingModel
                {
                    ExecuterFIO = fio
                }));
            }

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
