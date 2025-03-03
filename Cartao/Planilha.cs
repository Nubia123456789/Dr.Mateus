using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static iText.Kernel.Pdf.Colorspace.PdfSpecialCs;
using iText.StyledXmlParser.Jsoup.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cartao
{
    public partial class Planilha : Form
    {
        public Planilha()
        {
            InitializeComponent();
        }

        private void btnItau_Click(object sender, EventArgs e)
        {
            var textoPdf = PegarTexto();
            if (!string.IsNullOrWhiteSpace(textoPdf)) ExtrairDadosDoTexto_Itau2(textoPdf);
        }

        private void btnBradesco_Click(object sender, EventArgs e)
        {
            var textoPdf = PegarTexto();
            if (!string.IsNullOrWhiteSpace(textoPdf)) ExtrairDadosDoTexto_Bradesco2(textoPdf);
        }

        private void ExtrairDadosDoTexto_Itau2(string textoDoPdf)
        {
            string palavraChave1 = "Pagamentos efetuados Lançamentos: compras e saques";
            string palavraChave2 = "Encargos cobrados nesta fatura";

            var try1 = textoDoPdf.Split(palavraChave1);
            var try2 = "";
            if (try1.Length > 1) try2 = try1[1].Split(palavraChave2)[0];
            else try2 = textoDoPdf.Split(palavraChave2)[0];
            var dados_pdf = try2.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            var dados_tratados = dados_pdf.Select(Transformar).Where(o => o.E_Valido).ToList();

            StringBuilder sb = new StringBuilder();
            dados_tratados.ForEach(p => sb.AppendLine(p.ToString()));
            txtBox.Text = sb.ToString();
        }

        private void ExtrairDadosDoTexto_Bradesco2(string textoDoPdf)
        {
            string palavraChave1 = "Número do Cartão";
            string palavraChave2 = "Pagamento mínimo desta fatura";

            var dados_pdf = textoDoPdf.Split(palavraChave1)[1].Split(palavraChave2)[0].Split("\n", StringSplitOptions.RemoveEmptyEntries);

            var dados_tratados = dados_pdf.Select(Transformar).Where(o => o.E_Valido).ToList();

            StringBuilder sb = new StringBuilder();
            dados_tratados.ForEach(p => sb.AppendLine(p.ToString()));
            txtBox.Text = sb.ToString();
        }

        private string PegarTexto()
        {
            string nome_arquivo = EscolherArquivo();

            if (nome_arquivo == string.Empty) MessageBox.Show("Arquivo Não Selecionado: " + nome_arquivo);
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                PdfReader pdfReader = new PdfReader(nome_arquivo); //Initialize PDF reader
                PdfDocument pdfDoc = new PdfDocument(pdfReader);

                for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string data = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
                    stringBuilder.Append(data);
                }
                return stringBuilder.ToString();
            }

            return string.Empty;
        }

        private string EscolherArquivo()
        {
            var fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK && fileDialog.CheckFileExists)
                return fileDialog.FileName;
            else return string.Empty;
        }

        public DadoTransformado Transformar(string dadoBruto)
        {
            // Regex para capturar data no formato dd/MM
            Regex regexData = new Regex(@"\b\d{2}/\d{2}\b");
            // Regex para capturar o valor (com ou sem traço)
            Regex regexValor = new Regex(@"-?\s*\d{1,3}(\.\d{3})*,\d{2}");
            // Regex para capturar o nome (entre data e valor)
            Regex regexNome = new Regex(@"(?<=\b\d{2}/\d{2}\b).*?(?=-?\s*\d{1,3}(\.\d{3})*,\d{2})");

            // Procurar os elementos no dado bruto
            string data = regexData.Match(dadoBruto).Value;
            string valor = regexValor.Match(dadoBruto).Value.Replace(" ","");
            string nome = regexNome.Match(dadoBruto).Value.Trim();

            if (string.IsNullOrEmpty(data) && string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(valor))
                return new DadoTransformado();
            else
            {
                if (nome.Contains("PAGAMENTO EFETUADO")) return new DadoTransformado();
                return new DadoTransformado(data, nome, valor);
            }
        }
    }

    public class DadoTransformado
    {

        public DadoTransformado() { }
        public DadoTransformado(string data, string nome, string valor)
        {
            Data = data;
            Nome = nome;
            Valor = valor;
        }

        public string Data { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public bool E_Valido
        {
            get { return !string.IsNullOrEmpty(Data) && !string.IsNullOrEmpty(Nome) && !string.IsNullOrEmpty(Valor); }
        }

        public override string ToString()
        {
            return $"{Data}\t{Nome}\t{Valor}";
        }
    }
}
