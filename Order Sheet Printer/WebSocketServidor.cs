using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketBE.Entities;
using WebSocketBE.Enums;
using WebSocketBE.Interfaces;
using Windows.Foundation;
using Windows.Networking.Sockets;

namespace WebSocketBE
{
    public class WebSocketServidor
    {
        #region Propriedades privates
        [JsonProperty("url")]
        private string Url { get; set; }
        private int Porta { get; set; }
        private string Host { get; set; }
        private string IP { get; set; }
        [JsonProperty("validarToken")]
        private bool IsUseToken { get; set; }
        private IUsuario _usuario { get; set; }
        private TipoConexao _tipoConexao { get; set; }
        [JsonProperty("habilitado")]

        private bool isAlive { get; set; }

        #endregion

        #region Propriedades publicas
        public ClientWebSocket clientWebSocket;
        public bool Run { get; set; }
        public bool Habilitado { get; set; }
        public ThreadControl tr { get; set; }

        #endregion


        #region Eventos
        public event Action<ThreadControl> OnMessageReceivedEvent;
        #endregion




        #region Construtores
        public WebSocketServidor()
        {
            Url = UrlConnections.ConnectionServidorPrinter;
            IsUseToken = true;
            Habilitado = true;
            Run = true;
        }

        public WebSocketServidor(string url, bool habilitado, bool usarToken)
        {
            Url = url;
            Habilitado = habilitado;
            Run = habilitado;
            IsUseToken = usarToken;
        }
        #endregion

        public void CreateInstance(TipoConexao tipoConexao, IUsuario usuario)
        {
            _usuario = usuario;
            _tipoConexao = tipoConexao;
            clientWebSocket = new ClientWebSocket();
        }
        private void ResetConection()
        {
            CreateInstance(_tipoConexao, _usuario);
            OpenConnection();
        }
        public void Start()
        {

            new Thread(x =>
            {
                BackGroundWorker();
                Run = false;
            }).Start();
        }

        private void OpenConnection()
        {
            try
            {
                Run = true;
                if (clientWebSocket.State == WebSocketState.Aborted)
                    ResetConection();

                else if (clientWebSocket.State == WebSocketState.Closed)
                    Run = false;

                else if (clientWebSocket.State != WebSocketState.Open)
                {
                    if (IsUseToken && _usuario?.token?.Length > 0 && clientWebSocket.State == WebSocketState.None)
                        clientWebSocket.Options.SetRequestHeader("Authorization", $"Bearer {_usuario.token}");

                    _ = Task.Run(async () =>
                    {
                        await clientWebSocket.ConnectAsync(new Uri(Url), new CancellationToken());
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void Close(bool force = false)
        {
            try
            {
                Run = false;
                if (force)
                {
                    clientWebSocket.Dispose();
                }
                if (clientWebSocket?.State == WebSocketState.Open)
                {
                    _ = Task.Run(async () =>
                    {
                        await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", new CancellationToken());

                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void BackGroundWorker()
        {
            tr = new ThreadControl();

            while (Run)
            {
                try
                {
                    //Caso esteja conectando, aguarda a conexao.
                    if (clientWebSocket.State == WebSocketState.Connecting)
                        continue;

                    //se o servidor caiu , levanta
                    if (clientWebSocket.State != WebSocketState.Open)
                    {
                        OpenConnection();
                    }
                    else if (!tr.IsAlive)
                    {
                        tr.IsAlive = true;
                        byte[] buffer = new byte[9999999];
                        var segment = new ArraySegment<byte>(buffer, 0, buffer.Length);
                        WebSocketReceiveResult recvResult;
                        CancellationTokenSource cts = new CancellationTokenSource(3000);


                        Task.Run(async () =>
                         {
                             try
                             {
                                 recvResult = await clientWebSocket.ReceiveAsync(segment, cts.Token);

                                 if (segment.ToArray()?.Length > 0)
                                 {
                                     byte[] bytes = segment.ToArray(); 
                                     string messageResult = Encoding.UTF8.GetString(bytes);

                                     tr.Json.Append(messageResult);
                                     if (messageResult.Contains(@"""OK""}"))
                                     {
                                         tr.Complete = true;
                                     }
                                     else
                                         tr.IsAlive = false;

                                 }
                                 else
                                     tr.IsAlive = false;
                             }
                             catch (Exception ex)
                             {
                                 tr.Reset();
                             }
                         }).Wait();


                        if (tr.Complete)
                        {
                            OnMessageReceivedEvent?.Invoke(tr);

                            tr.Reset();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    tr.Reset();
                }
                Thread.Sleep(200);
            }
        }

        public string GetUrl()
        {
            return Url;
        }

        public bool GetUsarToken()
        {
            return IsUseToken;
        }

        public bool IsHabilitado()
        {
            return Habilitado;
        }
    }
}
