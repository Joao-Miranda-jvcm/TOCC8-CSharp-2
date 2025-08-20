using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            TOCC8Entities1 contexto;

            try
            {
                contexto = new TOCC8Entities1();
                var lista = from c in contexto.produto select c;
                this.dgvDados.DataSource = lista.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            TOCC8Entities1 contexto;
            produto obj;
            try
            {
                obj = new produto();
                obj.descricao = txtDescricao.Text;
                obj.preco = Convert.ToDouble(txtPrecoFinal.Text);
                obj.taxalucro = Convert.ToDouble(txtTaxa.Text);
                obj.datavalidade = Convert.ToDateTime(dtPrazoValidade.Text);

                contexto = new TOCC8Entities1();
                contexto.produto.Add(obj);
                contexto.SaveChanges();
                MessageBox.Show("Salvo com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar o produto: " + ex.Message);
            }
        }

        private void btnGrafico_Click(object sender, EventArgs e)
        {
            FGrafico f;
            f = new FGrafico();
            f.WindowState = FormWindowState.Maximized;
            f.ShowDialog(); //não perde o foco
        }
        private void txtCodigo_Leave(object sender, EventArgs e)
        {

        }
        private void dgvDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int linha;
            linha = this.dgvDados.SelectedCells[0].RowIndex;
            this.txtCodigo.Text = dgvDados.Rows[linha].Cells[0].Value.ToString();
            this.txtDescricao.Text = dgvDados.Rows[linha].Cells[1].Value.ToString();
            this.dtPrazoValidade.Text = dgvDados.Rows[linha].Cells[2].Value.ToString();
            this.txtPrecoFinal.Text = dgvDados.Rows[linha].Cells[3].Value.ToString();
            this.txtTaxa.Text = dgvDados.Rows[linha].Cells[4].Value.ToString();
            this.txtDias.Text = (Convert.ToDateTime(dtPrazoValidade.Text) - DateTime.Now).Days.ToString() + " dias";

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            TOCC8Entities1 contexto;
            produto obj;
            int codigo;
            try
            {
                codigo = Convert.ToInt32(txtCodigo.Text);
                contexto = new TOCC8Entities1();
                obj = contexto.produto.First(c => c.codigo == codigo);
                if (obj != null)
                {
                    obj.descricao = txtDescricao.Text;
                    obj.preco = Convert.ToDouble(txtPrecoFinal.Text);
                    obj.taxalucro = Convert.ToDouble(txtTaxa.Text);
                    obj.datavalidade = Convert.ToDateTime(dtPrazoValidade.Text);
                    contexto.SaveChanges();
                }
                MessageBox.Show("Alterado com sucesso.");
                this.btnListar.PerformClick();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnRemover_Click(object sender, EventArgs e)
        {
            TOCC8Entities1 contexto;
            produto obj;
            int codigo;
            try
            {
                codigo = Convert.ToInt32(txtCodigo.Text);
                contexto = new TOCC8Entities1();
                obj = contexto.produto.First(c => c.codigo == codigo);
                if (obj != null)
                {
                    contexto.produto.Remove(obj);
                    contexto.SaveChanges();
                }
                MessageBox.Show("Removido com sucesso.");
                this.btnListar.PerformClick();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtDescricao_KeyUp(object sender, KeyEventArgs e)
        {
            TOCC8Entities1 contexto;

            try
            {
                contexto = new TOCC8Entities1();
                var obj = contexto.produto.Where(c => c.descricao.StartsWith(txtDescricao.Text.ToLower()));
                this.dgvDados.DataSource = obj.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
