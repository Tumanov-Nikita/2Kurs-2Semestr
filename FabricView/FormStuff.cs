using FabricService.BindingModels;
using FabricService.Interfaces;
using FabricService.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace FabricView
{
    public partial class FormStuff : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IStuffService service;

        private int? id;

        private List<StuffPartViewModel> productParts;

        public FormStuff(IStuffService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormStuff_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    StuffViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.StuffName;
                        textBoxPrice.Text = view.Cost.ToString();
                        productParts = view.StuffParts;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                productParts = new List<StuffPartViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (productParts != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = productParts;
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
            var form = Container.Resolve<FormStuffPart>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.StuffId = id.Value;
                    }
                    productParts.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormStuffPart>();
                form.Model = productParts[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    productParts[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
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
                        productParts.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
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
            if (productParts == null || productParts.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<StuffPartBindingModel> productPartBM = new List<StuffPartBindingModel>();
                for (int i = 0; i < productParts.Count; ++i)
                {
                    productPartBM.Add(new StuffPartBindingModel
                    {
                        Id = productParts[i].Id,
                        StuffId = productParts[i].StuffId,
                        PartId = productParts[i].PartId,
                        Count = productParts[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new StuffBindingModel
                    {
                        Id = id.Value,
                        StuffName = textBoxName.Text,
                        Cost = Convert.ToInt32(textBoxPrice.Text),
                        StuffParts = productPartBM
                    });
                }
                else
                {
                    service.AddElement(new StuffBindingModel
                    {
                        StuffName = textBoxName.Text,
                        Cost = Convert.ToInt32(textBoxPrice.Text),
                        StuffParts = productPartBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
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
