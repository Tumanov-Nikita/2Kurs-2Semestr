using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
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
				var response = APIClient.GetRequest("api/Executer/GetList");
				if (response.Result.IsSuccessStatusCode)
				{
					List<ExecuterViewModel> list = APIClient.GetElement<List<ExecuterViewModel>>(response);
					if (list != null)
					{
						comboBoxExecuter.DisplayMember = "ExecuterFIO";
						comboBoxExecuter.ValueMember = "Id";
						comboBoxExecuter.DataSource = list;
						comboBoxExecuter.SelectedItem = null;
					}
				}
				else
				{
					throw new Exception(APIClient.GetError(response));
				}
			}
			catch (Exception ex)
			{
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
				var response = APIClient.PostRequest("api/General/TakeBookingInWork", new BookingBindingModel
				{
					Id = id.Value,
					ExecuterId = Convert.ToInt32(comboBoxExecuter.SelectedValue)
				});
				if (response.Result.IsSuccessStatusCode)
				{
					MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
					DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
					throw new Exception(APIClient.GetError(response));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
