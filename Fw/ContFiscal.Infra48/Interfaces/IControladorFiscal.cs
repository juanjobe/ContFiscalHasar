
using ContFiscal.Infra48.Entidades;
using hfl.argentina;
using System;
using System.Collections.Generic;

namespace ContFiscal.Infra48.Interfaces
{
    public interface IControladorFiscal
    {
        /// <summary>
        /// Permite interrumpir los reintentos constantes que efectúa la DLL 2G cuando se ha producido un error entre el envío del comando a la IFH 2G, y la recepción de la respuesta al comando enviado. Por ejemplo, cuando la IFH 2G se encuentra offline.
        /// </summary>
        void Abortar();
        /// <summary>
        /// Permite enviar a la IFH 2G el comando de apertura del cajón de dinero conectado a la IFH 2G. El envío de este comando tiene sentido si la IFH 2G soporta conexión de cajón de dinero, y éste se encuentra físicamente conec-tado.
        /// </summary>
        void AbrirCajonDinero();

        HasarImpresoraFiscalRG3561.RespuestaAbrirDocumento AbrirDocumento(HasarImpresoraFiscalRG3561.TiposComprobante comprobante);

        /// <summary>
        /// Para que el co-mando generado por CambiarFechaInicioActividades( ) sea aceptado por la IFH 2G, previamente se debe haber enviado el comando generado por CerrarJornadaFiscal( ) -Cierre ‘Z’-, y no debe haber un comprobante abierto.
        /// </summary>
        /// <param name="fecha">una fecha valida</param>
        void CambiarFechaInicioActividades(DateTime fecha);
        /// <summary>
        /// Permite enviar a la IFH 2G el comando de impresión del Informe Diario de Cierre ‘Z’.
        /// </summary>
        /// <param name="tipoReporte"></param>
        /// <returns></returns>
        HasarImpresoraFiscalRG3561.CerrarJornadaFiscalX CerrarJornadaFiscalX();
        HasarImpresoraFiscalRG3561.CerrarJornadaFiscalZ CerrarJornadaFiscalZ();

        bool EmitirComprobante(HasarImpresoraFiscalRG3561.TiposComprobante tiposComprobante,List<Producto> productos);
    }
}
