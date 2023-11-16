using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketBE.Enums;
using WebSocketBE.Interfaces;

namespace WebSocketBE.Entities
{
    public static class ServidorGateway
    {
        public static WebSocketServidor WebSocketServidor { get; set; }


        public static void InitWebSocketServer(TipoConexao tipo, IUsuario usuario)
        {
            if (WebSocketServidor.clientWebSocket == null)
                WebSocketServidor.CreateInstance(tipo, usuario);

            WebSocketServidor.Start();            
        }

        public static bool VerificaAtividadeConexaoWebSocket()
        {
            if (!WebSocketServidor.Run)            
                return false;

            return true;
        }
    }
}
