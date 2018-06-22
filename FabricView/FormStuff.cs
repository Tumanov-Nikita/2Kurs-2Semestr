using FabricService.BindingModels;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FabricView
{
	public partial class FormStuff : Form
	{
		public int Id { set { id = value; } }

		private int? id;

		private List<StuffPartViewModel> StuffParts;

		public FormStuff()
		{
			InitializeComponent();
		}

		private void FormStuff_Load(object sender, EventArgs e)
		{
			if (id.HasValue)
			{
				try
				{
					var response = APIClient.GetRequest("api/Stuff/Get/" + id.Value);
					if (response.Result.IsSuccessStatusCode)
					{
						var Stuff = APIClient.GetElement<StuffViewModel>(response);
						textBoxName.Text = Stuff.StuffName;
						textBoxPrice.Text = Stuff.Cost.ToString();
						StuffParts = Stuff.StuffParts;
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
			else
			{
				StuffParts = new List<StuffPartViewModel>();
			}
		}

		private void LoadData()
		{
			try
			{
				if (StuffParts != null)
				{
					dataGridView.DataSource = null;
					dataGridView.DataSource = StuffParts;
					dataGridView.Columns[0].Visible = false;
					dataGridView.Columns[1].Visible = false;
					dataGridView.Columns[2].Visible = false;
					dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			var form = new FormStuffPart();
			if (form.ShowDialog() == DialogResult.OK)
			{
				if (form.Model != null)
				{
					if (id.HasValue)
					{
						form.Model.StuffId = id.Value;
					}
					StuffParts.Add(form.Model);
				}
				LoadData();
			}
		}

		private void buttonUpd_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				var form = new FormStuffPart();
				form.Model = StuffParts[dataGridView.SelectedRows[0].Cells[0].RowIndex];
				if (form.ShowDialog() == DialogResult.OK)
				{
					StuffParts[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
					LoadData();
				}
			}
		}

		private void buttonDel_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					try
					{
						StuffParts.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					LoadData();
				}
			}
		}

		private void buttonRef_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxName.Text))
			{
				MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (string.IsNullOrEmpty(textBoxPrice.Text))
			{
				MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (StuffParts == null || StuffParts.Count == 0)
			{
				MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			try
			{
				List<StuffPartBindingModel> StuffPartBM = new List<StuffPartBindingModel>();
				for (int i = 0; i < StuffParts.Count; ++i)
				{
					StuffPartBM.Add(new StuffPartBindingModel
					{
						Id = StuffParts[i].Id,
						StuffId = StuffParts[i].StuffId,
						PartId = StuffParts[i].PartId,
						Count = StuffParts[i].Count
					});
				}
				Task<HttpResponseMessage> response;
				if (id.HasValue)
				{
					response = APIClient.PostRequest("api/Stuff/UpdElement", new StuffBindingModel
					{
						Id = id.Value,
						StuffName = textBoxName.Text,
						Cost = Convert.ToInt32(textBoxPrice.Text),
						StuffParts = StuffPartBM
					});
				}
				else
				{
					response = APIClient.PostRequest("api/Stuff/AddElement", new StuffBindingModel
					{
						StuffName = textBoxName.Text,
						Cost = Convert.ToInt32(textBoxPrice.Text),
						StuffParts = StuffPartBM
					});
				}
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
