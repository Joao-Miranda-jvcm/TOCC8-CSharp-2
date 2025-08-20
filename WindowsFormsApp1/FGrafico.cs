using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FGrafico : Form
    {
        public FGrafico()
        {
            InitializeComponent();
            mostrarGrafico();
        }

        private void btnForm_Click(object sender, EventArgs e)
        {
            Form1 f;
            f = new Form1();
            f.WindowState = FormWindowState.Maximized;
            f.ShowDialog(); 
        }
        public void mostrarGrafico()
        {
            TOCC8Entities1 contexto;

            try
            {
                contexto = new TOCC8Entities1();
                var produtos = from p in contexto.produto select p;

                chart1.Series.Clear();

                chart1.Series.Add(new Series("Lucro"));
                chart1.Series["Lucro"].ChartType = SeriesChartType.Column;
                chart1.Series["Lucro"].Color = Color.Green;

                chart1.Series.Add(new Series("PrazoValidade"));
                chart1.Series["PrazoValidade"].ChartType = SeriesChartType.Column;
                chart1.Series["PrazoValidade"].Color = Color.Yellow;

                chart1.Titles.Clear();
                chart1.Titles.Add("Lucro x Prazo de Validade");

                foreach (var p in produtos)
                {
                    var prazoValidade = (Convert.ToDateTime(p.datavalidade) - DateTime.Now).Days;
                    double lucroUnitario = (double)(p.preco - p.taxalucro);

                    chart1.Series["Lucro"].Points.AddXY(p.descricao, lucroUnitario);
                    chart1.Series["PrazoValidade"].Points.AddXY(p.descricao, prazoValidade);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
