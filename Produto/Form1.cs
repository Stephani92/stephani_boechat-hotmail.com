using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Produto
{
    public partial class tabelaProd : Form
    {
        public tabelaProd()
        {
            InitializeComponent();
            
            
        }
        
        private void bntCommit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tabelaProd.Rows.Count; i++)
            {
                var linha = tabelaProd.Rows[i];
                for (int j = 0; j < tabelaProd.Rows[i].Cells.Count; j++)
                {   
                    var celula = linha.Cells[j];
                    textBox1.Text += $"{celula.Value} ";
                }
                textBox1.Text += "\r\n" ;
            }
        }

        //private void tabelaProd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{   var linhaindex = e.RowIndex;
        //    var linha = tabelaProd.Rows[linhaindex];
        //    for (int j = 0; j < linha.Cells.Count; j++)
        //    {
        //        var celula = linha.Cells[j];
        //        textBox1.Text += $"{celula.Value} ";
        //    }
        //    textBox1.Text += "\r\n";
        //}
        public void OpenFileDialogForm()
        {
            openFileDialog1 = new OpenFileDialog();
            selectButton = new Button
            {
                Size = new Size(100, 20),
                Location = new Point(15, 15),
                Text = "Select file"
            };
            selectButton.Click += new EventHandler(SelectButton_Click);
            textBox1 = new TextBox
            {
                Size = new Size(300, 300),
                Location = new Point(15, 40),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            ClientSize = new Size(330, 360);
            Controls.Add(selectButton);
            Controls.Add(textBox1);
        }
        public void DateGrid(string path)
        {
            //Criando um DataTable
            DataTable dt = new DataTable();

            //Lendo Todas as linhas do arquivo CSV
            string[] Linha = System.IO.File.ReadAllLines($"{path}");
            //Neste For, vamos percorrer todas as linhas que foram lidas do arquivo CSV
            for (Int32 i = 0; i < Linha.Length; i++)
            {
                //Aqui Estamos pegando a linha atual, e separando os campos
                //Por exemplo, ele vai lendo um texto, e quando achar um ponto e virgula
                //ele pega o texto e joga na outra posição do array temp, e assim por diante
                //até chegar no final da linha
                string[] campos = Linha[i].Split(Convert.ToChar(";"));

                //Um datable precisa de colunas
                //Como a função acima jogou cada campo em uma posição do array de acordo com
                //o ponto e virgula, eu estou pegando quantos campos ele achou e criando a mesma
                //quantidade de colunas no datatable para corresponder a cada campo
                if (i == 0)
                {
                    for (Int32 i2 = 0; i2 < campos.Length; i2++)
                    {
                        //Criando uma coluna
                        DataColumn col = new DataColumn();
                        col.ColumnName = campos[i2];
                        col.Namespace = campos[i2];
                        //Adicionando a coluna criada ao datatable
                        dt.Columns.Add(col);
                    }
                }
                if (i > 0)
                {
                    dt.Rows.Add(campos);
                }
                //Inserindo uma linha(row) no datable (Vai fazer isso para cada linha encontrada
                //no arquivo CSV

            }

            //Depois de montado o datatable, vamos falar para o grid
            //que a fonte de dados para ele exibir, será o datatable que 
            //a gente acabou de criar

            tabelaProd.DataSource = dt;
        }
        //private void SetText(string text)
        //{
        //    textBox1.Text = text;
        //}
        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var path = openFileDialog1.FileName;
                    DateGrid(path);
                    //var sr = new StreamReader(openFileDialog1.FileName);
                    //SetText(sr.ReadToEnd());
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

    }
}
