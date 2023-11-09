using ContFiscal.Infra48.Interfaces;
using hfl.argentina;
using hfl.argentina.Hasar_Funcs;
using log4net;
using System;
using System.Configuration;
using System.Reflection;

namespace ConfFiscal.Logica48.Servicios.Implementacion
{
    public class ControladorFiscalMigServicio : IControladorFiscalMig
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HasarImpresoraFiscalRG3561 _hasar;
        private AtributosDeTexto estilo;
        private long pos;

        public ControladorFiscalMigServicio()
        {
            _hasar = new HasarImpresoraFiscalRG3561();
            estilo = new AtributosDeTexto();
            pos = 1;

            //
        }
        

        public void Conectarse()
        {
            HasarImpresoraFiscalRG3561.RespuestaConsultarVersion resp;
            string msj = string.Empty;

            try
            {
                _log.Info($"Iniciando actividad {DateTime.Now}");
                var hoy = DateTime.Today;

                //se registra el archivo que sera el log del sistema
                _hasar.archivoRegistro($"{ConfigurationManager.AppSettings["RutaLogControladorFiscarl"]}\\Fiscal_{hoy.Year}{hoy.Month.ToString().PadLeft(2, '0')}{hoy.Day.ToString().PadLeft(2, '0')}.log");

                int espera1 = ConfigurationManager.AppSettings["TiempoDeEspera"].ToInt();
                _hasar.establecerTiempoDeEspera(espera1); // Por defecto, 10000
                var espera2 = ConfigurationManager.AppSettings["TiempoDeEsperaLecturaEscritura"].ToInt();
                _hasar.establecerTiempoDeEsperaLecturaEscritura(espera2);

                //conectamos con el controlador Hasar
                var ip = ConfigurationManager.AppSettings["IPControladorHasar"].ToString();
                var port = ConfigurationManager.AppSettings["PuertoCF"].ToString().ToInt();
                msj = $"Conectando a la  IP:{ip} - Puerto: {port}\r\n Timeout esperando conexión = {espera1} ms\r\nTiemout esperando respuesta = {espera2} ms";
                _log.Info(msj);
                Console.WriteLine(msj);

                _hasar.conectar(ip, port);


                _hasar.ConfigurarImpresoraFiscal(HasarImpresoraFiscalRG3561.Configuracion.TIMEOUT_ENVIO_RESPUESTA_EN_ESPERA, "20");

                resp = _hasar.ConsultarVersion();

                msj = $"Conexión: OK !\r\n" +
                  $"Protocolo impresora fiscal 2G =[{resp.getVersionProtocolo()}]\r\n" +
                  $"DLL 2G Protocolo              =[{_hasar.ObtenerVersionProtocolo()}]\r\n" +
                  $"DLL 2G Revisión               =[{_hasar.ObtenerRevision()}]\r\n" +
                  $"Timeout interrupción DC2/DC4  = 20 seg\r\n" +
                  "-----------------------------------------------------------\r\n" +
                  "    Punto de venta  HABILITADO  para operar\r\n" +
                  "-----------------------------------------------------------";

                _log.Info(msj);
                Console.WriteLine(msj);

                //TENER MUY EN CUENTA ESTE IF
                if (resp.getVersionProtocolo() > _hasar.ObtenerVersionProtocolo())
                {
                    msj = "DLL INCOMPATIBLE CON impresora fiscal 2G\r\n" +
                      "Se procederá a FINALIZAR el proceso. Presione una tecla";
                    Console.WriteLine(msj);
                    Console.ReadKey();
                    throw new Exception(msj);
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hubo un error. Verifique el Log.");
                _log.Error("Hubo un error.");
                _log.Error(ex);
            }
        }
    }
}
