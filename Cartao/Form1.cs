using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System.Text;
using System.Windows.Forms;

namespace Cartao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnItau_Click(object sender, EventArgs e)
        {
            string nome_arquivo = EscolherArquivo();
            if (nome_arquivo == string.Empty) throw new Exception("Arquivo Não Selecionado: " + nome_arquivo);
            ExtrairTexto_Itau(nome_arquivo);
        }

        private void ExtrairTexto_Itau(string filePath)
        {
            if (File.Exists(filePath))
            {
                StringBuilder stringBuilder = new StringBuilder();
                PdfReader pdfReader = new PdfReader(filePath); //Initialize PDF reader
                PdfDocument pdfDoc = new PdfDocument(pdfReader);

                for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string data = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
                    stringBuilder.Append(data);
                }

                // richTextBox1.Text =stringBuilder.ToString();
                var palavra = "Lançamentos: compras e saques";
                var palavra2 = "PAGAMENTO EFETUADO";
                var palavra3 = "Encargos cobrados nesta fatura";
                var palavra4 = "Lançamentos no cartão";
                var palavra5 = "DATA ESTABELECIMENTO VALOR ";
                var dados_pdf = stringBuilder.ToString().Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();

                bool area_interesse = false;
                var texto_final = string.Empty;
                foreach (var linha in dados_pdf)
                {

                    if (linha.Contains(palavra)) { area_interesse = true; continue; }
                    if (linha.Contains(palavra3)) break;

                    if (area_interesse && !linha.Contains(palavra2))
                    {
                        var dados_linha = linha.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                        if (linha.Contains(palavra4)) dados_linha = linha.Split(palavra4)[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                        if (linha.Contains(palavra5)) dados_linha = linha.Split(palavra5)[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                        if (dados_linha.Count == 0) continue;

                        string frase = dados_linha[0] + "\t";

                        bool alvo = false;
                        /* if (linha.Contains(palavra4))
                         {
                             frase = linha.Split(palavra4)[0];
                             alvo = true;
                         }*/


                        if (frase.Length < 3 || frase[2] != '/') continue;
                        /* if (!alvo)
                         {*/
                        for (int i = 1; i < dados_linha.Count - 1; i++)
                        {

                            if (dados_linha[i].Contains(","))
                            {
                                frase += "\t" + dados_linha[i];
                                alvo = true;
                                break;
                            }
                            else frase += dados_linha[i] + " ";
                        }

                        if(!alvo)
                        frase += "\t" + dados_linha[dados_linha.Count - 1];
                                          
                        texto_final += frase + "\n";
                    }

                }
                richTextBox1.Text = texto_final;
            }
        }

        private void btnBradesco_Click(object sender, EventArgs e)
        {
            string nome_arquivo = EscolherArquivo();
            if (nome_arquivo == string.Empty) throw new Exception("Arquivo Não Selecionado: " + nome_arquivo);
            ExtrairTexto(nome_arquivo);
        }

        private void ExtrairTexto(string filePath)
        {


            if (File.Exists(filePath))
            {
                StringBuilder stringBuilder = new StringBuilder();
                PdfReader pdfReader = new PdfReader(filePath); //Initialize PDF reader
                PdfDocument pdfDoc = new PdfDocument(pdfReader);

                for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string data = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
                    stringBuilder.Append(data);
                }

                var dados_pdf = stringBuilder.ToString().Split("-", StringSplitOptions.RemoveEmptyEntries).ToList();
                // richTextBox1.Text = dados_pdf[4];
                var dados_interesse = dados_pdf[4].Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                var texto_final = string.Empty;
                foreach (var linha in dados_interesse)
                {

                    if (linha.Length > 3 && linha[2] == '/')
                    {
                        var dados_linha = linha.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                        string frase = dados_linha[0] + "\t";

                        for (int i = 1; i < dados_linha.Count - 1; i++)
                        {
                            frase += dados_linha[i] + " ";
                        }

                        frase += "\t" + dados_linha[dados_linha.Count - 1];
                        texto_final += frase + "\n";
                    }
                    else texto_final += linha + "\n";
                }
                richTextBox1.Text = texto_final;
            }
        }

        private string EscolherArquivo()
        {
            var fileDialog = new OpenFileDialog();
            /* fileDialog.Title = "Escolha o PDF";
             fileDialog.InitialDirectory = @"C:\";
             fileDialog.Filter = "All files (.)|.|PDF (.pdf)|.pdf";
             fileDialog.FilterIndex = 2;*/
            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK && fileDialog.CheckFileExists)
            {
                return fileDialog.FileName;
            }
            else return string.Empty;
        }



    }
}
