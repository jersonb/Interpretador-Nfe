using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace NotaroConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            const string RAIZ = @"..\..\..\teste\";
            var listaChaves = File.ReadAllLines(Path.Combine(RAIZ, "chaves.txt")).ToList();

            var anos = Directory.GetDirectories(RAIZ).ToList();

            var linhas = new List<string>
            {
            string.Join("|","nomeArquivo","chave","numero","codProd","descricao","cfop","vlProd","aliqIcms","bcIcms","vlIcms")
            };

            anos.ForEach(ano =>
            {
                var meses = Directory.GetDirectories(ano).ToList();

                meses.ForEach(mes =>
                {
                    var arquivos = Directory.GetFiles(mes, "*.xml").ToList();

                    arquivos.ForEach(arquivo =>
                    {
                        var nf = DesserializaXml(File.ReadAllText(arquivo));
                        var chave = nf.protNFe.infProt.chNFe;

                        if (listaChaves.Contains(chave))
                        {

                            var nomeArquivo = arquivo;
                            var numero = nf.NFe.infNFe.ide.nNF;

                            var produtos = nf.NFe.infNFe.det;

                            produtos.ForEach(produto =>
                            {
                                var codProd = produto.prod.cProd.Replace("|", "");
                                var descricao = produto.prod.xProd.Replace("|", "");
                                var cfop = produto.prod.CFOP;
                                var vlProd = produto.prod.vProd;
                                var aliqIcms = produto.imposto.ICMS.ICMS.pICMS;
                                var bcIcms = produto.imposto.ICMS.ICMS.vBC;
                                var vlIcms = produto.imposto.ICMS.ICMS.vICMS;

                                var linha = string.Join("|", nomeArquivo, chave, numero, codProd, descricao, cfop, vlProd, aliqIcms, bcIcms, vlIcms);
                                linhas.Add(linha.Replace(RAIZ, ""));

                            });

                        }

                    });

                });
            });

            File.AppendAllLines(Path.Combine(RAIZ, "lista.txt"), linhas);


        }
        public static ProcNFe DesserializaXml(string xml)
        {
            var stringReader = new StringReader(xml);
            var xmlReader = new XmlTextReader(stringReader);
            xmlReader.Read();
            var serializer = new XmlSerializer(typeof(ProcNFe));

            if (serializer.CanDeserialize(xmlReader))
            {
                return (ProcNFe)serializer.Deserialize(xmlReader);
            }

            return null;
        }
    }
}
