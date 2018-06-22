using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
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
				var responseC = APIClient.GetRequest("api/Customer/GetList");
				if (responseC.Result.IsSuccessStatusCode)
				{
					List<CustomerViewModel> list = APIClient.GetElement<List<CustomerViewModel>>(responseC);
					if (list != null)
					{
						comboBoxCustomer.DisplayMember = "CustomerFIO";
						comboBoxCustomer.ValueMember = "Id";
						comboBoxCustomer.DataSource = list;
						comboBoxCustomer.SelectedItem = null;
					}
				}
				else
				{
					throw new Exception(APIClient.GetError(responseC));
				}
				var responseP = APIClient.GetRequest("api/Stuff/GetList");
				if (responseP.Result.IsSuccessStatusCode)
				{
					List<StuffViewModel> list = APIClient.GetElement<List<StuffViewModel>>(responseP);
					if (list != null)
					{
						comboBoxStuff.DisplayMember = "StuffName";
						comboBoxStuff.ValueMember = "Id";
						comboBoxStuff.DataSource = list;
						comboBoxStuff.SelectedItem = null;
					}
				}
				else
				{
					throw new Exception(APIClient.GetError(responseP));
				}
			}
			catch (Exception ex)
			{
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
					var responseP = APIClient.GetRequest("api/Stuff/Get/" + id);
					if (responseP.Result.IsSuccessStatusCode)
					{
						StuffViewModel Stuff = APIClient.GetElement<StuffViewModel>(responseP);
						int count = Convert.ToInt32(textBoxCount.Text);
						textBoxSum.Text = (count * (int)Stuff.Cost).ToString();
					}
					else
					{
						throw new Exception(APIClient.GetError(responseP));
					}
				}
				catch (Exception ex)
				{
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
			try
			{
				var response = APIClient.PostRequest("api/General/CreateBooking", new BookingBindingModel
				{
					CustomerId = Convert.ToInt32(comboBoxCustomer.SelectedValue),
					StuffId = Convert.ToInt32(comboBoxStuff.SelectedValue),
					Count = Convert.ToInt32(textBoxCount.Text),
					Cost = Convert.ToInt32(textBoxSum.Text)
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
