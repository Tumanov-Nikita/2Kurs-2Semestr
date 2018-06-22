﻿using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FabricView
{
	public partial class FormGeneral : Form
	{
		public FormGeneral()
		{
			InitializeComponent();
		}

		private void LoadData()
		{
			try
			{
				var response = APIClient.GetRequest("api/General/GetList");
				if (response.Result.IsSuccessStatusCode)
				{
					List<BookingViewModel> list = APIClient.GetElement<List<BookingViewModel>>(response);
					if (list != null)
					{
						dataGridView.DataSource = list;
						dataGridView.Columns[0].Visible = false;
						dataGridView.Columns[1].Visible = false;
						dataGridView.Columns[3].Visible = false;
						dataGridView.Columns[5].Visible = false;
						dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

		private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new FormCustomers();
			form.ShowDialog();
		}

		private void компонентыToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new FormParts();
			form.ShowDialog();
		}

		private void изделияToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new FormStuffs();
			form.ShowDialog();
		}

		private void складыToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new FormStorages();
			form.ShowDialog();
		}

		private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new FormExecuters();
			form.ShowDialog();
		}

		private void пополнитьСкладToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new FormPutOnStorage();
			form.ShowDialog();
		}

		private void buttonCreateBooking_Click(object sender, EventArgs e)
		{
			var form = new FormCreateBooking();
			form.ShowDialog();
			LoadData();
		}

		private void buttonTakeBookingInWork_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				var form = new FormTakeBookingInWork
				{
					Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value)
				};
				form.ShowDialog();
				LoadData();
			}
		}

		private void buttonBookingReady_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
				try
				{
					var response = APIClient.PostRequest("api/General/FinishBooking", new BookingBindingModel
					{
						Id = id
					});
					if (response.Result.IsSuccessStatusCode)
					{
						LoadData();
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
		}

		private void buttonPayBooking_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
				try
				{
					var response = APIClient.PostRequest("api/General/PayBooking", new BookingBindingModel
					{
						Id = id
					});
					if (response.Result.IsSuccessStatusCode)
					{
						LoadData();
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
		}

		private void buttonRef_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void прайсСудовToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog
			{
				Filter = "doc|*.doc|docx|*.docx"
			};
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				try
				{
					var response = APIClient.PostRequest("api/Report/SaveStuffPrice", new ReportBindingModel
					{
						FileName = sfd.FileName
					});
					if (response.Result.IsSuccessStatusCode)
					{
						MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
		}

		private void загруженностьСкладовToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			var form = new FormStoragesLoad();
			form.ShowDialog();
		}

		private void контрактыToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new FormCustomerBookings();
			form.ShowDialog();
		}
	}
}
