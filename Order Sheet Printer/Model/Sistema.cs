 using OrderSheetPrinter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model
{
    public class Sistema
    {
        private static Sistema loggedSystem;
        public SistemaEnum type { get; }
        public String nome { get; }
        public Usuario defaultUser { get; set; }
        public IOrderService service { get; }

        public Sistema() { }

        public static void SetLoggedSystem(Sistema sistema)
        {
            loggedSystem = sistema;
        }

        public static Sistema GetLoggedSystem()
        {
            return loggedSystem;
        }
        private Sistema(SistemaEnum type, String nome, IOrderService service)
        {
            this.type = type;
            this.nome = nome;
            this.service = service;
        }

        public static Sistema Create(SistemaEnum sistema)
        {
            switch (sistema)
            {
                case SistemaEnum.GARCOM_DIGITAL:
                    return CreateGarcomDigital();

                case SistemaEnum.QR_EXPRESS:
                    return CreateQrExpress();

                default:
                    throw new Exception($"Sistema desconhecido ({sistema})");
            }
        }

        private static Sistema CreateGarcomDigital()
        {
            return new Sistema(
                SistemaEnum.GARCOM_DIGITAL,
                "Garçom Digital",
                new GarcomDigitalService()
            );
        }

        private static Sistema CreateQrExpress()
        {
            return new Sistema(
                SistemaEnum.QR_EXPRESS,
                "QR Express",
                new QRExpressService()
            );
        }
    }
}
