using OrderSheetPrinter.Domain.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSheetPrinter.Auxiliar
{
    public class Preferencias
    {
        public Dictionary<string, Preferencia> Variaveis { get; set; }

        public Preferencias()
        {
            Variaveis = new Dictionary<string, Preferencia>();
        }


        public void Refresh()
        {
            try
            {
                Variaveis = CarregarConfiguracoes().Variaveis;
            }
            finally
            {
               
            }
        }


        public T GetPreferenciaValue<T>(string nome, T defaultValue)
        {
            Preferencia pref;
            if (Variaveis.TryGetValue(nome, out pref))
                try
                {
                    string valor = pref?.Valor ?? "";
                    if (valor?.Length == 0)
                    {
                        var obj = defaultValue;
                        return obj;
                    }
                    return (T)Convert.ChangeType(pref.Valor, typeof(T));
                }
                catch (Exception ex)
                {
                    return defaultValue;
                }
            return defaultValue;
        }

        public void Update(string nome, string valor)
        {
            Preferencia pref;

            if (Variaveis.TryGetValue(nome, out pref))
            {
                Variaveis[nome].Valor = valor;
            }
            else
            {
                Insert(nome, 'T', valor);
                if (!Variaveis.ContainsKey(nome))
                    Variaveis.Add(nome, new Preferencia(nome, 'T', valor));
            }
            SalvarConfiguracoes(this);
        }

        /// <summary>
        /// Insere uma nova preferencia na tabela de preferências.
        /// </summary>
        /// <param name="nome">O nome da preferencia. Este valor é utilizado posteriormente como chave do dicionário de Variaveis.</param>
        /// <param name="tipo">O Tipo da preferencia sendo inserida.</param>
        /// <param name="valor">O valor da preferência. Este atributo é sempre uma string, mesmo quando o tipo da preferência é
        /// numérico.</param>
        public void Insert(string nome, char tipo, string valor)
        {

            try
            {
                if (!Variaveis.ContainsKey(nome))
                {
                    Variaveis.Add(nome, new Preferencia(nome, 'T', valor));
                    SalvarConfiguracoes(this);
                }
            }
            catch { }
        }


        internal static T GetPreferenciaValue<T>(object eXIBIR_TELA_ULTIMOS_REGISTROS_NUMERADOS, T v)
        {
            throw new NotImplementedException();
        }

        public static Preferencias CarregarConfiguracoes()
        {
            Preferencias result = null;
            try
            {
                string json = "";
                if (File.Exists(GetFullPathFilename()))
                    json = File.ReadAllText(GetFullPathFilename());
                else
                    File.WriteAllText(GetFullPathFilename(), json);


                if (json != string.Empty)
                    result = Utils.DeserializeJson<Preferencias>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return result;
        }

        public static void SalvarConfiguracoes(Preferencias preferencias)
        {
            string json = Utils.SerializeJson(preferencias);
            File.WriteAllText(GetFullPathFilename(), json);
        }

        private static string GetDirectory() => AppDomain.CurrentDomain.BaseDirectory;


        public static string GetFilename() => "Preferencias.txt";


        private static string GetFullPathFilename() => GetDirectory() + GetFilename();
    }

    public class Preferencia
    {
        public Preferencia()
        {

        }
        private string nome;
        private char tipo;
        public string Valor { get; set; }

        public string Nome
        {
            get
            {
                return nome;
            }
        }

        public char Tipo
        {
            get
            {
                return tipo;
            }
        }

        public Preferencia(DataRow dr)
        {
            nome = dr["nome"].ToString();
            if (!Char.TryParse(dr["tipo"].ToString(), out tipo))
                tipo = ' ';
            Valor = dr["valor"].ToString();
        }

        public Preferencia(string NomePref, char tipoPref, string ValorPref)
        {
            nome = NomePref;
            tipo = tipoPref;
            Valor = ValorPref;
        }
    }
}
