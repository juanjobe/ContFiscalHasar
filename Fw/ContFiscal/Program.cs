using ConfFiscal.Logica48.Servicios.Implementacion;
using hfl.argentina;
using System;
using System.Configuration;
using System.Reflection;
using System.Threading;

namespace ContFiscal
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        static void Main(string[] args)
        {
            try
            {
                //var hasar = new HasarImpresoraFiscalRG3561();
                //var hoy = DateTime.Today;

                ////se registra el archivo que sera el log del sistema
                //hasar.archivoRegistro($"{ConfigurationManager.AppSettings["RutaLogControladorFiscarl"]}\\Fiscal_{hoy.Year}{hoy.Month.ToString().PadLeft(2, '0')}{hoy.Day.ToString().PadLeft(2, '0')}.log");
                ////conectamos con el controlador Hasar
                //var ip = ConfigurationManager.AppSettings["IPControladorHasar"];
                //hasar.conectar(ip,6300);
                //Console.WriteLine($"Se Conectó a la IP: {ip}");
                //log.Info($"Se Conectó a la IP: {ip}");
                ////verificamos el protocolo DLL .Net 2G 
                //var protocolo = hasar.ObtenerVersionProtocolo();
                //Console.WriteLine($"Protocolo: {protocolo}");
                //log.Info($"Protocolo: {protocolo}");
                //var version = hasar.ConsultarVersion().getVersion();
                //Console.WriteLine($"Versión: {version}");
                //log.Info($"Versión: {version}");
                var cf = new ControladorFiscalMigServicio();
                Thread hilo = new Thread(new ThreadStart(cf.Conectarse));

                hilo.Start();
                hilo.Join();

            }
            catch (Exception ex)

            {
                Console.WriteLine($"Error: {ex.Message} StackTrace: {ex.StackTrace}");
                log.Error(ex);
            }
            Console.ReadKey();
        }      
    }
}
