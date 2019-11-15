using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace NotaroConsole
{
    public static class Namespaces
    {
        public const String NFe = "http://www.portalfiscal.inf.br/nfe";
    }
    /// <summary>
    /// NF-e processada
    /// </summary>
    [XmlType(Namespace = Namespaces.NFe)]
    [XmlRoot("nfeProc", Namespace = Namespaces.NFe, IsNullable = false)]
    public class ProcNFe
    {
        public NFe NFe;

        public ProtNFe protNFe;

        [XmlAttribute]
        public string versao;
    }


    /// <summary>
    /// Identificação do Ambiente
    /// </summary>
    [Serializable]
    [XmlType(Namespace = Namespaces.NFe)]
    public enum TAmb
    {
        [XmlEnum("1")]
        Producao = 1,

        [XmlEnum("2")]
        Homologacao = 2,
    }

    /// <summary>
    /// Dados do protocolo de status
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class InfProt
    {

        /// <summary>
        /// Identificação do Ambiente
        /// </summary>
        public TAmb tpAmb;

        public string verAplic;
        public string chNFe;


        public string nProt;
        public int cStat;
        public string xMotivo;

        [XmlAttribute(DataType = "ID")]
        public string Id;
    }


    /// <summary>
    /// Tipo Protocolo de status resultado do processamento da NF-e<
    /// </summary>
    [Serializable]
    [XmlType(Namespace = Namespaces.NFe)]
    public partial class ProtNFe
    {
        public InfProt infProt;

        [XmlAttribute]
        public string versao;
    }



    [Serializable]
    [XmlType(Namespace = Namespaces.NFe)]
    public partial class NFe
    {
        public InfNFe infNFe;
    }


    [Serializable]
    [XmlType(Namespace = Namespaces.NFe)]
    public partial class Endereco
    {

        public string xLgr;
        public string nro;
        public string xCpl;
        public string xBairro;
        public string cMun;
        public string xMun;
        public String UF;
        public string CEP;
        public string fone;
    }

    public class EmpresaNf
    {
        public string CNPJ;
        public string CPF;
        public string xNome;
        public string IE;
        public string IEST;

        public string email;

        [XmlIgnore]
        public Endereco Endereco;
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class Destinatario : EmpresaNf
    {
        public string ISUF;
        //public string email;

        public Endereco enderDest
        {
            get
            {
                return Endereco;
            }
            set
            {
                Endereco = value;
            }
        }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class Emitente : EmpresaNf
    {

        public string xFant;
        public string IM;
        public string CNAE;

        public Endereco enderEmit
        {
            get
            {
                return Endereco;
            }
            set
            {
                Endereco = value;
            }
        }

        /// <summary>
        /// Código de Regime Tributário
        /// </summary>
        public String CRT { get; set; }
    }


    /// <summary>
    /// Dados dos produtos e serviços da NF-e
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class ProdutoNf
    {
        public string cProd;
        public string cEAN;
        public string xProd;
        public string NCM;
        public string EXTIPI;
        public int CFOP;
        public string uCom;
        public double qCom;
        public double vUnCom;
        public double vProd;
        public string cEANTrib;
        public string uTrib;
        public string qTrib;
        public string vUnTrib;
        public string vFrete;
        public string vSeg;
        public string vDesc;
        public string vOutro;
        public string xPed;
        public string nItemPed;
        public string nFCI;
    }


    [Serializable]
    [XmlTypeAttribute(AnonymousType = true, Namespace = Namespaces.NFe)]
    public class ImpostoICMS
    {
        public String orig;
        public String CST;
        public String CSOSN;
        public double vBC;
        public double pICMS;
        public double vICMS;
    }

    public class ImpostoICMS00 : ImpostoICMS { }
    public class ImpostoICMS10 : ImpostoICMS { }
    public class ImpostoICMS20 : ImpostoICMS { }
    public class ImpostoICMS30 : ImpostoICMS { }
    public class ImpostoICMS40 : ImpostoICMS { }
    public class ImpostoICMS51 : ImpostoICMS { }
    public class ImpostoICMS60 : ImpostoICMS { }
    public class ImpostoICMS70 : ImpostoICMS { }
    public class ImpostoICMS90 : ImpostoICMS { }
    public class ImpostoICMSPart : ImpostoICMS { }
    public class ImpostoICMSSN101 : ImpostoICMS { }
    public class ImpostoICMSSN102 : ImpostoICMS { }
    public class ImpostoICMSSN201 : ImpostoICMS { }
    public class ImpostoICMSSN202 : ImpostoICMS { }
    public class ImpostoICMSSN500 : ImpostoICMS { }
    public class ImpostoICMSSN900 : ImpostoICMS { }
    public class ImpostoICMSST : ImpostoICMS { }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class ProdutoICMS
    {

        [XmlElement("ICMS00", typeof(ImpostoICMS00))]
        [XmlElement("ICMS10", typeof(ImpostoICMS10))]
        [XmlElement("ICMS20", typeof(ImpostoICMS20))]
        [XmlElement("ICMS30", typeof(ImpostoICMS30))]
        [XmlElement("ICMS40", typeof(ImpostoICMS40))]
        [XmlElement("ICMS51", typeof(ImpostoICMS51))]
        [XmlElement("ICMS60", typeof(ImpostoICMS60))]
        [XmlElement("ICMS70", typeof(ImpostoICMS70))]
        [XmlElement("ICMS90", typeof(ImpostoICMS90))]
        [XmlElement("ICMSPart", typeof(ImpostoICMSPart))]
        [XmlElement("ICMSSN101", typeof(ImpostoICMSSN101))]
        [XmlElement("ICMSSN102", typeof(ImpostoICMSSN102))]
        [XmlElement("ICMSSN201", typeof(ImpostoICMSSN201))]
        [XmlElement("ICMSSN202", typeof(ImpostoICMSSN202))]
        [XmlElement("ICMSSN500", typeof(ImpostoICMSSN500))]
        [XmlElement("ICMSSN900", typeof(ImpostoICMSSN900))]
        [XmlElement("ICMSST", typeof(ImpostoICMSST))]
        public ImpostoICMS ICMS;
    }


    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class ProdutoIPI
    {

        public string clEnq;
        public string CNPJProd;
        public string cSelo;
        public string qSelo;
        public string cEnq;
        public IPITrib IPITrib;
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class IPITrib
    {

        public string CST;
        public double? pIPI;
        public double? qUnid;
        public double? vBC;
        public double? vUnid;
        public double? vIPI;
    }

    /// <summary>
    /// Tributos incidentes nos produtos ou serviços da NF-e
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class ProdutoImposto
    {
        public string vTotTrib;
        public ProdutoICMS ICMS;
        public ProdutoIPI IPI;
    }

    /// <summary>
    /// Dados dos detalhes da NF-e
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public class Detalhe
    {

        public ProdutoNf prod;
        public ProdutoImposto imposto;
        public string infAdProd;

        [XmlAttribute]
        public string nItem;
    }


    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class Duplicata
    {

        public string nDup;
        public DateTime? dVenc;
        public Double? vDup;
    }


    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class Fatura
    {
        public string nFat;
        public string vOrig;
        public string vDesc;
        public string vLiq;
    }



    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class Cobranca
    {
        public Fatura fat;

        [XmlElement("dup")]
        public List<Duplicata> dup { get; set; }

        public Cobranca()
        {
            dup = new List<Duplicata>();
        }
    }


    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class ObsCont
    {

        public string xTexto;

        [XmlAttribute]
        public string xCampo;
    }



    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class ObsFisco
    {

        public string xTexto;

        [XmlAttribute]
        public string xCampo;
    }


    /// <summary>
    /// Informações adicionais da NF-e
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class InfAdic
    {

        public string infAdFisco;
        public string infCpl;

        [XmlElement("obsCont")]
        public List<ObsCont> obsCont { get; set; }

        [XmlElement("obsFisco")]
        public List<ObsFisco> obsFisco { get; set; }

        public InfAdic()
        {
            obsCont = new List<ObsCont>();
            obsFisco = new List<ObsFisco>();
        }

    }


    /// <summary>
    /// Grupo de Valores Totais referentes ao ICMS
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class ICMSTotal
    {
        /// <summary>
        /// Base de Cálculo do ICMS 
        /// </summary>
        public Double vBC;

        /// <summary>
        /// Valor Total do ICMS 
        /// </summary>
        public Double vICMS;

        /// <summary>
        /// Valor total do ICMS Interestadual para a UF de destino
        /// </summary>
        public double? vICMSUFDest;

        /// <summary>
        /// Valor total do ICMS Interestadual para a UF do remetente
        /// </summary>
        public double? vICMSUFRemet;

        /// <summary>
        /// Valor total do ICMS relativo Fundo de Combate à Pobreza(FCP) da UF de destino
        /// </summary>
        public double? vFCPUFDest;

        /// <summary>
        /// Base de Cálculo do ICMS ST 
        /// </summary>
        public Double vBCST;

        /// <summary>
        /// Valor Total do ICMS ST 
        /// </summary>
        public Double vST;

        /// <summary>
        /// Valor Total dos produtos e serviços
        /// </summary>
        public Double vProd;

        /// <summary>
        /// Valor Total do Frete 
        /// </summary>
        public Double vFrete;

        /// <summary>
        /// Valor Total do Seguro
        /// </summary>
        public Double vSeg;

        /// <summary>
        /// Valor Total do Desconto
        /// </summary>
        public Double vDesc;

        /// <summary>
        /// Valor Total do II 
        /// </summary>
        public double vII;

        /// <summary>
        /// Valor Total do IPI 
        /// </summary>
        public double vIPI;

        /// <summary>
        /// Valor do PIS 
        /// </summary>
        public double vPIS;

        /// <summary>
        /// Valor do COFINS 
        /// </summary>
        public double vCOFINS;

        /// <summary>
        /// Outras Despesas acessórias 
        /// </summary>
        public Double vOutro;

        /// <summary>
        /// Valor Total da NF-e 
        /// </summary>
        public Double vNF;


        public Double? vTotTrib;
    }

    /// <summary>
    /// Totais referentes ao ISSQN
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class ISSQNTotal
    {

        public Double? vServ;
        public Double? vBC;
        public Double? vISS;
        public Double? vPIS;
        public Double? vCOFINS;
    }


    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class Total
    {

        public ICMSTotal ICMSTot;
        public ISSQNTotal ISSQNtot;
    }


    /// <summary>
    /// Modalidade do frete
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public enum ModalidadeFrete
    {

        [XmlEnum("0")]
        PorContaRemetente = 0,

        [XmlEnum("1")]
        PorContaDestinatario = 1,

        [XmlEnum("2")]
        PorContaTerceiros = 2,

        [XmlEnum("3")]
        ProprioContaRemetente = 3,

        [XmlEnum("4")]
        ProprioContaDestinatario = 4,

        [XmlEnum("9")]
        SemTransporte = 9,
    }


    /// <summary>
    /// Dados do transportador
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class Transportador
    {

        public string CNPJ;
        public string CPF;
        public string xNome;
        public string IE;
        public string xEnder;
        public string xMun;
        public String UF;

    }


    [Serializable]
    [XmlType(Namespace = Namespaces.NFe)]
    public partial class Veiculo
    {

        public string placa;
        public String UF;
        public string RNTC;
    }



    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class Volume
    {

        public Double? qVol;
        public string esp;
        public string marca;
        public string nVol;
        public Double? pesoL;
        public Double? pesoB;
    }


    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class Transporte
    {

        public ModalidadeFrete modFrete;
        public Transportador transporta;

        public String balsa;
        public String vagao;

        public Veiculo reboque;
        public Veiculo veicTransp;

        [XmlElement("vol")]
        public List<Volume> vol { get; set; }

        public Transporte()
        {
            vol = new List<Volume>();
        }
    }



    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class InfNFe
    {

        public Identificacao ide;
        public Emitente emit;
        public Destinatario dest;

        [XmlElement("det")]
        public List<Detalhe> det { get; set; }

        public Total total;
        public Transporte transp;
        public Cobranca cobr;
        public InfAdic infAdic;
        [XmlAttribute]
        public string versao;

        /// <summary>
        /// Informação adicional de compra
        /// </summary>
        public InfCompra compra { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id;

        public InfNFe()
        {
            det = new List<Detalhe>();
        }

        [XmlIgnore]
        public Versao Versao
        {
            get
            {
                return Versao.Parse(versao);
            }
        }
    }
    public class Versao
    {
        private const String _Pattern = @"(\d+)\.(\d+)";
        public int Maior { get; protected set; }
        public int Menor { get; protected set; }

        public Versao(int maior, int menor)
        {
            Maior = maior;
            Menor = menor;
        }

        [DebuggerStepThrough]
        public static Versao Parse(String str)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("O parâmetro str não pode ser nulo ou vazio.", "str");
            }

            Match m = Regex.Match(str, _Pattern);
            Versao v = new Versao(0, 0);

            if (!m.Success)
            {
                throw new ArgumentException("A versão não pode ser interpretada.", "str");
            }

            v.Maior = int.Parse(m.Groups[1].Value);
            v.Menor = int.Parse(m.Groups[2].Value);

            return v;
        }
    }

    /// <summary>
    /// Informações de Compras
    /// </summary>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class InfCompra
    {

        /// <summary>
        /// Nota de Empenho
        /// </summary>
        public string xNEmp { get; set; }

        /// <summary>
        /// Pedido
        /// </summary>
        public string xPed { get; set; }

        /// <summary>
        /// Contrato
        /// </summary>
        public string xCont { get; set; }
    }

    /// <summary>
    /// Forma de emissão da NF-e
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum FormaEmissao
    {

        [XmlEnum("1")]
        Normal = 1,

        [XmlEnum("2")]
        ContingenciaFS = 2,

        [XmlEnum("3")]
        ContingenciaSCAN = 3,

        [XmlEnum("4")]
        ContingenciaDPEC = 4,

        [XmlEnum("5")]
        ContingenciaFSDA = 5,

        [XmlEnum("6")]
        ContingenciaSVCAN = 6,

        [XmlEnum("7")]
        ContingenciaSVCRS = 7,

        [XmlEnum("9")]
        ContingenciaOffLineNFCe = 9,
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public partial class Identificacao
    {

        public string natOp { get; set; }

        /// <summary>
        /// Código do modelo do Documento Fiscal. 55 = NF-e; 65 = NFC-e.
        /// </summary>
        public int mod { get; set; }

        public short serie { get; set; }
        public int nNF { get; set; }
        public DateTime? dEmi { get; set; }

        #region DataHora Emissão e Saída v2-

        /// <summary>
        /// Data de Saída/Entrada, NFe2
        /// </summary>
        public DateTime? dSaiEnt { get; set; }

        /// <summary>
        /// Hora de Saída/Entrada, NFe2
        /// </summary>
        public String hSaiEnt { get; set; }

        #endregion

        #region DataHora Emissão e Saída v3+

        /// <summary>
        /// Data e Hora de Emissão, NFe v3
        /// </summary>
     //   public DateTimeOffsetIso8601? dhEmi { get; set; }

        /// <summary>
        /// Data e Hora de Saída/Entrada, NFe v3
        /// </summary>
       // public DateTimeOffsetIso8601? dhSaiEnt { get; set; }

        #endregion

        public Tipo tpNF { get; set; }

        /// <summary>
        /// Tipo de Impressao
        /// </summary>
        public int tpImp { get; set; }

        /// <summary>
        /// Forma de emissão da NF-e
        /// </summary>
        public FormaEmissao tpEmis { get; set; }

        public TAmb tpAmb { get; set; }

        /// <summary>
        /// Data e Hora da entrada em contingência
        /// </summary>
      //  public DateTimeOffsetIso8601? dhCont { get; set; }

        /// <summary>
        /// Justificativa da entrada em contingência 
        /// </summary>
        public String xJust { get; set; }

        /// <summary>
        /// Grupo de informação das NF/NF-e referenciadas
        /// </summary>
        [XmlElementAttribute("NFref")]
        public List<NFReferenciada> NFref { get; set; }

        public Identificacao()
        {
            NFref = new List<NFReferenciada>();
        }
    }

    /// <summary>
    /// Tipo do Documento Fiscal
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public enum Tipo
    {

        [XmlEnum("0")]
        Entrada = 0,
        [XmlEnum("1")]
        Saida = 1,
    }
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public class NFReferenciada
    {
        [XmlElement("refCTe", typeof(string))]
        [XmlElement("refECF", typeof(RefECF))]
        [XmlElement("refNF", typeof(RefNF))]
        [XmlElement("refNFP", typeof(RefNFP))]
        [XmlElement("refNFe", typeof(string))]
        [XmlChoiceIdentifier("TipoNFReferenciada")]
        public object Item;

        [XmlIgnore]
        public TipoNFReferenciada TipoNFReferenciada { get; set; }

        public override string ToString()
        {
            if (TipoNFReferenciada == TipoNFReferenciada.refCTe || TipoNFReferenciada == TipoNFReferenciada.refNFe)
            {
                string chaveAcesso = Item.ToString();
                return $"{UtilsNfe.TipoDFeDeChaveAcesso(chaveAcesso)} Ref.: {Formatador.FormatarChaveAcesso(Item.ToString())}";
            }
            else
                return Item.ToString();
        }

    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public enum TipoNFReferenciada
    {
        refCTe,
        refECF,
        refNF,
        refNFP,
        refNFe,
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public class RefECF
    {
        public string mod { get; set; }
        public string nECF { get; set; }
        public string nCOO { get; set; }

        public override string ToString()
        {
            return $"ECF Ref.: Modelo: {mod} ECF: {nECF} COO: {nCOO}";
        }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public class RefNF
    {
        public string AAMM { get; set; }
        public string CNPJ { get; set; }
        public string mod { get; set; }
        public string serie { get; set; }
        public string nNF { get; set; }

        public override string ToString()
        {
            return $"NF Ref.: Série: {serie} Número: {nNF} Emitente: {Formatador.FormatarCnpj(CNPJ)} Modelo: {mod}";
        }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = Namespaces.NFe)]
    public class RefNFP
    {
        public string AAMM { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string IE { get; set; }
        public string mod { get; set; }
        public string serie { get; set; }
        public string nNF { get; set; }

        public override string ToString()
        {
            String cpfCnpj = !String.IsNullOrWhiteSpace(CNPJ) ? CNPJ : CPF;
            return $"NFP Ref.: Série: {serie} Número: {nNF} Emitente: {Formatador.FormatarCpfCnpj(cpfCnpj)} Modelo: {mod} IE: {IE}";
        }


    }
    public static class Formatador
    {
        /// <summary>
        /// Cultura pt-BR
        /// </summary>
        public static readonly CultureInfo Cultura = new CultureInfo(1046);

        static Formatador()
        {
            Cultura.NumberFormat.CurrencyPositivePattern = 2;
            Cultura.NumberFormat.CurrencyNegativePattern = 9;
        }

        public const String FormatoNumeroNF = @"000\.000\.000";

        public const String CEP = @"^(\d{5})\-?(\d{3})$";
        public const String CNPJ = @"^(\d{2})\.?(\d{3})\.?(\d{3})\/?(\d{4})\-?(\d{2})$";
        public const String CPF = @"^(\d{3})\.?(\d{3})\.?(\d{3})\-?(\d{2})$";
        public const String Telefone = @"^\(?(\d{2})\)?\s*(\d{4,5})\s*\-?\s*(\d{4})$";

        public const String FormatoMoeda = "#,0.00##";
        public const String FormatoNumero = "#,0.####";

        private static String InternalRegexReplace(String input, String pattern, String replacement)
        {
            String result = input;

            if (!String.IsNullOrWhiteSpace(input))
            {
                input = input.Trim();

                Regex rgx = new Regex(pattern);

                if (rgx.IsMatch(input))
                {
                    result = rgx.Replace(input, replacement);
                }
            }

            return result;
        }

        /// <summary>
        /// Formata a linha 1 do endereço. Ex. Floriano Peixoto, 512
        /// </summary>
        /// <param name="endereco"></param>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static String FormatarEnderecoLinha1(String endereco, int? numero, String complemento = null)
        {
            String sNumero = numero.HasValue ? numero.Value.ToString() : null;
            return FormatarEnderecoLinha1(endereco, numero, complemento);
        }

        /// <summary>
        /// Formata a linha 1 do endereço. Ex. Floriano Peixoto, 512
        /// </summary>
        /// <param name="endereco"></param>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static String FormatarEnderecoLinha1(String endereco, String numero = null, String complemento = null)
        {
            String linha1 = String.Empty;

            if (!String.IsNullOrWhiteSpace(endereco))
            {
                linha1 = String.Format("{0}, {1}", endereco.Trim(), String.IsNullOrWhiteSpace(numero) ? "S/N" : numero.Trim());

                if (!String.IsNullOrWhiteSpace(complemento))
                {
                    linha1 += " - " + complemento.Trim();
                }
            }

            return linha1;
        }

        /// <summary>
        /// Formata um CEP
        /// </summary>
        /// <param name="cep">CEP</param>
        /// <returns>CEP Formatado ou vazio caso cep inválido</returns>
        public static String FormatarCEP(String cep)
        {
            return InternalRegexReplace(cep, CEP, "$1-$2");
        }

        public static String FormatarCEP(int cep)
        {
            if (cep < 0)
            {
                throw new ArgumentOutOfRangeException("cep", "o cep não pode ser negativo.");
            }

            return FormatarCEP(cep.ToString().PadLeft(8, '0'));
        }

        public static String FormatarCnpj(String cnpj)
        {
            return InternalRegexReplace(cnpj, CNPJ, "$1.$2.$3/$4-$5");
        }

        public static String FormatarCpf(String cpf)
        {
            return InternalRegexReplace(cpf, CPF, "$1.$2.$3-$4");
        }

        /// <summary>
        /// Formata um número de documento
        /// </summary>
        /// <param name="cpfCnpj"></param>
        /// <returns></returns>
        public static String FormatarCpfCnpj(String cpfCnpj)
        {
            String result;

            if (!String.IsNullOrWhiteSpace(cpfCnpj))
            {
                result = cpfCnpj.Trim();

                if (Regex.IsMatch(result, CPF))
                {
                    result = FormatarCpf(result);
                }
                else if (Regex.IsMatch(result, CNPJ))
                {
                    result = FormatarCnpj(result);
                }
            }
            else
            {
                result = String.Empty;
            }

            return result;
        }

        /// <summary>
        /// Formata uma string de município com a uf, ex Caçapava do Sul - RS
        /// </summary>
        /// <param name="municipio">Município</param>
        /// <param name="uf">UF</param>
        /// <param name="separador">Separador</param>
        /// <returns>String formatada.</returns>
        public static String FormatarMunicipioUf(String municipio, String uf, String separador = " - ")
        {
            String result = "";

            if (!String.IsNullOrWhiteSpace(municipio) && !String.IsNullOrWhiteSpace(uf))
            {
                result = String.Format("{0}{1}{2}", municipio.Trim(), separador, uf.Trim());
            }
            else if (!String.IsNullOrWhiteSpace(municipio))
            {
                result = municipio.Trim();
            }
            else if (!String.IsNullOrWhiteSpace(uf))
            {
                result = uf.Trim();
            }

            return result;
        }

        public static String FormatarTelefone(String telefone)
        {
            return InternalRegexReplace(telefone, Telefone, "($1) $2-$3");
        }

        public static String FormatarChaveAcesso(String chaveAcesso)
        {
            return Regex.Replace(chaveAcesso, ".{4}", "$0 ").TrimEnd();
        }

        public static String Formatar(this Double number, String formato = FormatoMoeda)
        {
            return number.ToString(formato, Cultura);
        }

        public static String Formatar(this int number, String formato = FormatoMoeda)
        {
            return number.ToString(formato, Cultura);
        }

        public static String Formatar(this int? number, String formato = FormatoMoeda)
        {
            return number.HasValue ? number.Value.Formatar(formato) : String.Empty;
        }

        public static String Formatar(this Double? number, String formato = FormatoMoeda)
        {
            return number.HasValue ? number.Value.Formatar(formato) : String.Empty;
        }

        public static String FormatarMoeda(this Double? number)
        {
            return number.HasValue ? number.Value.ToString("C", Cultura) : String.Empty;
        }

        public static String Formatar(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString("dd/MM/yyyy") : String.Empty;
        }

        public static String FormatarDataHora(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString("dd/MM/yyyy hh:mm:ss") : String.Empty;
        }

        public static String Formatar(this TimeSpan? timeSpan)
        {
            return timeSpan.HasValue ? timeSpan.Value.ToString() : String.Empty;
        }
    }
    internal static class UtilsNfe
    {
        /// <summary>
        /// Verifica se uma string contém outra string no formato chave: valor.
        /// </summary>
        public static Boolean StringContemChaveValor(String str, String chave, String valor)
        {
            if (String.IsNullOrWhiteSpace(chave)) throw new ArgumentException(nameof(chave));
            if (String.IsNullOrWhiteSpace(str)) return false;

            return Regex.IsMatch(str, $@"({chave}):?\s*{valor}", RegexOptions.IgnoreCase);
        }

        public static String TipoDFeDeChaveAcesso(String chaveAcesso)
        {
            if (String.IsNullOrWhiteSpace(chaveAcesso)) throw new ArgumentException(nameof(chaveAcesso));

            if (chaveAcesso.Length == 44)
            {
                String f = chaveAcesso.Substring(20, 2);

                if (f == "55") return "NF-e";
                else if (f == "57") return "CT-e";
                else if (f == "65") return "NFC-e";
            }

            return "DF-e Desconhecido";
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

