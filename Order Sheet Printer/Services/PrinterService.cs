using Microsoft.Reporting.WinForms;
using OrderSheetPrinter.Auxiliar;
using OrderSheetPrinter.Controller;
using OrderSheetPrinter.Model;
using OrderSheetPrinter.Model.ReportsDataSources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Services
{
    public class PrinterService
    {
        private static PrinterService instance;
        private string dimensao;
        private double height = 100;

        private readonly String CATEGORIAS_COMPLEMENTO_REPORT = "OrderSheetPrinter.View.Reports.CategoriasComplementoReport.rdlc";
        private readonly String COMANDAS_FECHADAS_REPORT = "OrderSheetPrinter.View.Reports.ComandasFechadasReport.rdlc";
        private readonly String COMANDA_INDIVIDUAL_REPORT = "OrderSheetPrinter.View.Reports.ComandaIndividualReport.rdlc";
        private readonly String COMPLEMENTOS_REPORT = "OrderSheetPrinter.View.Reports.ComplementoReport.rdlc";
        private readonly String FORMAS_PAGAMENTO_REPORT = "OrderSheetPrinter.View.Reports.FormaPagamentoReport.rdlc";
        private readonly String ITENS_PEDIDO_REPORT = "OrderSheetPrinter.View.Reports.ItemPedidoReport.rdlc";
        private readonly String ITENS_RELATORIO_PRODUTOS_REPORT = "OrderSheetPrinter.View.Reports.ItemRelatorioProdutos.rdlc";
        private readonly String MESA_REPORT = "OrderSheetPrinter.View.Reports.MesaReport.rdlc";
        private readonly String NOVO_PEDIDO_REPORT = "OrderSheetPrinter.View.Reports.NovoModelo.NewNovoPedidoReport.rdlc";
        private readonly String NOVO_PEDIDO_REPORT58MM = "OrderSheetPrinter.View.Reports.NovoModelo.NewNovoPedidoReport58mm.rdlc";
        private readonly String OBSERVACAO_ITEM_PEDIDO_REPORT = "OrderSheetPrinter.View.Reports.ObservacaoItemPedidoReport.rdlc";
        private readonly String PEDIDOS_REPORT = "OrderSheetPrinter.View.Reports.PedidosAbertosMesaReport.rdlc";
        private readonly String PRODUTOS_RELATORIO_PRODUTOS_REPORT = "OrderSheetPrinter.View.Reports.ProdutoRelatorioReport.rdlc";
        private readonly String RELATORIO_PRODUTOS_REPORT = "OrderSheetPrinter.View.Reports.RelatorioProdutosReport.rdlc";

        private PrinterService() { }

        public static PrinterService GetInstance()
        {
            if (instance == null)
                instance = new PrinterService();
            return instance;
        }

        public void SetDimensao(string obj) => dimensao = obj;
        public string GetDimensao() => dimensao;

        public static void Clean()
        {
            PrinterService.instance = null;
        }

        public void SetHeight(double height)
        {
            this.height = height;
        }

        public double GetHeight()
        {
            return height;
        }
        public bool Print(NovoPedido pedido, String impressora)
        {
            try
            {
                if (!pedido.Order_Type.Equals("Transferência") && !shouldPrint(pedido))
                    return false;

                using (var report = new ReportViewer())
                {
                    report.Reset();
                    report.LocalReport.DataSources.Clear();

                    if ((AuxEnum.DimensoImpressora)ConfigManager.Preferencias?.GetPreferenciaValue(NomesPreferencias.DIMENSAO_IMPRESSORA, "0").StringToShort(0) == AuxEnum.DimensoImpressora._58mm)
                    {
                        report.LocalReport.ReportEmbeddedResource = NOVO_PEDIDO_REPORT58MM;
                    }
                    else
                        report.LocalReport.ReportEmbeddedResource = NOVO_PEDIDO_REPORT;

                    report.LocalReport.DataSources.Add(new ReportDataSource("DsNovoPedido", new List<NovoPedido>() { pedido }));

                    report.LocalReport.SetParameters(new List<ReportParameter>()
                    {
                        new ReportParameter("with_withdrawal", pedido.isEntrega ? pedido.DescricaoEntrega : "")

                    });

                    report.LocalReport.SubreportProcessing += (object sender, SubreportProcessingEventArgs e) =>
                    {
                        var rep = e.ReportPath;

                        if (rep.Equals(ITENS_PEDIDO_REPORT))
                        {
                            e.DataSources.Add(new ReportDataSource("DSItemPedido", pedido.itens));
                        }
                        else if (rep.Equals(CATEGORIAS_COMPLEMENTO_REPORT))
                        {
                            var idItem = Convert.ToInt32(e.Parameters["item"].Values[0]);
                            var item = FindItemPedido(idItem, pedido);

                            e.DataSources.Add(new ReportDataSource("DsCategoria", item.categoriasComplemento));
                        }
                        else if (rep.Equals(COMPLEMENTOS_REPORT))
                        {
                            var idItem = Convert.ToInt32(e.Parameters["item"].Values[0]);
                            var item = FindItemPedido(idItem, pedido);

                            var idCategoria = e.Parameters["categoria"].Values[0];
                            var categoria = FindCategoria(idCategoria, item);

                            e.DataSources.Add(new ReportDataSource("DsComplemento", categoria.complementos));
                        }
                        else if (rep.Equals(OBSERVACAO_ITEM_PEDIDO_REPORT))
                        {
                            var idItem = Convert.ToInt32(e.Parameters["item"].Values[0]);
                            var item = FindItemPedido(idItem, pedido);
                            e.DataSources.Add(new ReportDataSource("DsObservacaoItemPedido", new List<ObservacaoItemPedido> { item.observacao }));
                        }
                    };

                    report.LocalReport.Refresh();
                    report.RefreshReport();
                    report.Refresh();

                    report.LocalReport.PrintToPrinter(80, 200, impressora);

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool shouldPrint(NovoPedido pedido)
        {
            return pedido.itens.Count > 0;
        }

        public bool Print(Mesa mesa, String impressora)
        {
            try
            {
                using (var report = new ReportViewer())
                {
                    report.Reset();
                    report.LocalReport.DataSources.Clear();

                    report.LocalReport.ReportEmbeddedResource = MESA_REPORT;
                    report.LocalReport.DataSources.Add(new ReportDataSource("DsMesa", new List<Mesa>() { mesa }));

                    report.LocalReport.SubreportProcessing += (object sender, SubreportProcessingEventArgs e) =>
                    {
                        var rep = e.ReportPath;

                        if (rep.Equals(PEDIDOS_REPORT))
                        {
                            var tipo = e.Parameters["tipo"].Values[0];
                            e.DataSources.Add(new ReportDataSource("DsNovoPedido", tipo == "FECHADOS" ? mesa.pedidosFechados : mesa.pedidosPendentes));
                        }
                        else if (rep.Equals(ITENS_PEDIDO_REPORT))
                        {
                            var numeroPedido = e.Parameters["pedido"].Values[0];

                            var pedido = FindPedidoOnTable(numeroPedido, mesa);

                            e.DataSources.Add(new ReportDataSource("DSItemPedido", pedido.itens));
                        }
                        else if (rep.Equals(CATEGORIAS_COMPLEMENTO_REPORT))
                        {
                            var numeroPedido = e.Parameters["pedido"].Values[0];
                            var pedido = FindPedidoOnTable(numeroPedido, mesa);

                            var idItem = Convert.ToInt32(e.Parameters["item"].Values[0]);
                            var item = FindItemPedido(idItem, pedido);

                            e.DataSources.Add(new ReportDataSource("DsCategoria", item.categoriasComplemento));

                        }
                        else if (rep.Equals(COMPLEMENTOS_REPORT))
                        {
                            var numeroPedido = e.Parameters["pedido"].Values[0];
                            var pedido = FindPedidoOnTable(numeroPedido, mesa);

                            var idItem = Convert.ToInt32(e.Parameters["item"].Values[0]);
                            var item = FindItemPedido(idItem, pedido);

                            var idCategoria = e.Parameters["categoria"].Values[0];
                            var categoria = FindCategoria(idCategoria, item);

                            e.DataSources.Add(new ReportDataSource("DsComplemento", categoria.complementos));
                        }
                        else if (rep.Equals(OBSERVACAO_ITEM_PEDIDO_REPORT))
                        {
                            var numeroPedido = e.Parameters["pedido"].Values[0];
                            var pedido = FindPedidoOnTable(numeroPedido, mesa);

                            var idItem = Convert.ToInt32(e.Parameters["item"].Values[0]);
                            var item = FindItemPedido(idItem, pedido);

                            e.DataSources.Add(new ReportDataSource("DsObservacaoItemPedido", new List<ObservacaoItemPedido> { item.observacao }));
                        }
                        else if (rep.Equals(COMANDAS_FECHADAS_REPORT))
                        {
                            e.DataSources.Add(new ReportDataSource("DsNovoPedido", mesa.pedidosFechados));
                        }
                    };

                    report.LocalReport.Refresh();
                    report.RefreshReport();
                    report.Refresh();

                    report.LocalReport.PrintToPrinter(80, 200, impressora);

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Print(ComandaIndividual comanda, String impressora)
        {
            try
            {
                using (var report = new ReportViewer())
                {
                    report.Reset();
                    report.LocalReport.DataSources.Clear();

                    report.LocalReport.ReportEmbeddedResource = COMANDA_INDIVIDUAL_REPORT;
                    report.LocalReport.DataSources.Add(new ReportDataSource("DsComandaIndividual", new List<ComandaIndividual>() { comanda }));

                    report.LocalReport.SubreportProcessing += (object sender, SubreportProcessingEventArgs e) =>
                    {
                        var rep = e.ReportPath;

                        if (rep.Equals(ITENS_PEDIDO_REPORT))
                        {
                            e.DataSources.Add(new ReportDataSource("DSItemPedido", comanda.itens));
                        }
                        else if (rep.Equals(CATEGORIAS_COMPLEMENTO_REPORT))
                        {
                            var idItem = e.Parameters["item"].Values[0];
                            var item = FindItemPedidoOnComanda(idItem, comanda);

                            e.DataSources.Add(new ReportDataSource("DsCategoria", item.categoriasComplemento));
                        }
                        else if (rep.Equals(COMPLEMENTOS_REPORT))
                        {
                            var idItem = e.Parameters["item"].Values[0];
                            var item = FindItemPedidoOnComanda(idItem, comanda);

                            var idCategoria = e.Parameters["categoria"].Values[0];
                            var categoria = FindCategoria(idCategoria, item);

                            e.DataSources.Add(new ReportDataSource("DsComplemento", categoria.complementos));
                        }
                        else if (rep.Equals(OBSERVACAO_ITEM_PEDIDO_REPORT))
                        {
                            var idItem = e.Parameters["item"].Values[0];
                            var item = FindItemPedidoOnComanda(idItem, comanda);

                            e.DataSources.Add(new ReportDataSource("DsObservacaoItemPedido", new List<ObservacaoItemPedido> { item.observacao }));
                        }
                    };

                    report.LocalReport.Refresh();
                    report.RefreshReport();
                    report.Refresh();

                    report.LocalReport.PrintToPrinter(80, 200, impressora);

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Print(RelatorioProdutos relatorio, String impressora)
        {
            try
            {
                using (var report = new ReportViewer())
                {
                    report.Reset();
                    report.LocalReport.DataSources.Clear();

                    report.LocalReport.ReportEmbeddedResource = RELATORIO_PRODUTOS_REPORT;
                    report.LocalReport.DataSources.Add(new ReportDataSource("DsRelatorioProdutos", new List<RelatorioProdutos>() { relatorio }));

                    report.LocalReport.SubreportProcessing += (object sender, SubreportProcessingEventArgs e) =>
                    {
                        var rep = e.ReportPath;

                        if (rep.Equals(ITENS_RELATORIO_PRODUTOS_REPORT))
                        {
                            e.DataSources.Add(new ReportDataSource("DsItemRelatorio", relatorio.pedidos));
                        }
                        else if (rep.Equals(PRODUTOS_RELATORIO_PRODUTOS_REPORT))
                        {
                            var tipo = e.Parameters["tipo"].Values[0];
                            var idItem = e.Parameters["item"].Values[0];

                            var itens = new List<ItemPedido>();
                            if (tipo == "VENDIDOS")
                                itens = relatorio.pedidos.Find(x => x.id == Convert.ToInt32(idItem)).itens;
                            else
                            {
                                relatorio.pedidosCancelados.ForEach(x => itens.AddRange(x.itens));
                            }

                            e.DataSources.Add(new ReportDataSource("DsProduto", itens));
                        }
                        else if (rep.Equals(FORMAS_PAGAMENTO_REPORT))
                        {
                            e.DataSources.Add(new ReportDataSource("DsFormaPagamento", relatorio.formasPagamento));
                        }
                    };

                    report.LocalReport.Refresh();
                    report.RefreshReport();
                    report.Refresh();

                    report.LocalReport.PrintToPrinter(80, 200, impressora);

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private NovoPedido FindPedidoOnTable(String id, Mesa mesa)
        {
            var pedido = mesa.pedidosPendentes.Find(x => x.id.ToString() == id);

            if (pedido == null)
                pedido = mesa.pedidosFechados.Find(x => x.id.ToString() == id);

            return pedido;
        }

        private ItemPedido FindItemPedido(int idItem, NovoPedido pedido)
        {
            return pedido.itens.Find(x => x.id == idItem);
        }

        private ItemPedido FindItemPedidoOnComanda(String idItem, ComandaIndividual comanda)
        {
            return comanda.itens.Find(x => x.descricao == idItem);
        }

        private CategoriaComplemento FindCategoria(String id, ItemPedido item)
        {
            return item.categoriasComplemento.Find(x => x.id == id);
        }
    }
}
