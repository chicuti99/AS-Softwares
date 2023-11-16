using CredentialManagement;
using OrderSheetPrinter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Services
{
    public abstract class CredentialService
    {
        public static Usuario Get(SistemaEnum sistema)
        {
            var cm = new Credential { Target = sistema.ToString() };
            if (!cm.Load())
            {
                return null;
            }

            var user = new Usuario();
            user.usuario = cm.Username;
            user.senha = cm.Password;
            user.ultimoLogin = cm.LastWriteTime;

            return user;
        }

        public static bool Save(SistemaEnum sistema, Usuario usuario)
        {
            return new Credential
            {
                Target = sistema.ToString(),
                Username = usuario.usuario,
                Password = usuario.senha,
                PersistanceType = PersistanceType.LocalComputer
            }.Save();
        }

        public static bool Delete(SistemaEnum sistema)
        {
            return new Credential { Target = sistema.ToString() }.Delete();
        }
    }
}
