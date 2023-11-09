using ContFiscal.Infra48.Entidades;
using ContFiscal.Infra48.Interfaces;
using hfl.argentina;
using hfl.argentina.Hasar_Funcs;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConfFiscal.Logica48.Servicios.Implementacion
{
    public class ControladorFiscalServicio : IControladorFiscal
    {

        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly HasarImpresoraFiscalRG3561 _hasar;

        public ControladorFiscalServicio()
        {
            _hasar.eventoComandoEnProceso += _hasar_eventoComandoEnProceso;
            _hasar.eventoComandoProcesado += _hasar_eventoComandoProcesado;
        }

        private void _hasar_eventoComandoProcesado()
        {
            throw new NotImplementedException();
        }

        private void _hasar_eventoComandoEnProceso()
        {
            throw new NotImplementedException();
        }

        public ControladorFiscalServicio(HasarImpresoraFiscalRG3561  hasar)
        {
            _hasar = hasar;
        }

        public void Abortar()
        {
            throw new NotImplementedException();
        }

        public void AbrirCajonDinero()
        {
            throw new NotImplementedException();
        }

        public HasarImpresoraFiscalRG3561.RespuestaAbrirDocumento AbrirDocumento(HasarImpresoraFiscalRG3561.TiposComprobante comprobante)
        {
            throw new NotImplementedException();
        }

        public void CambiarFechaInicioActividades(DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public HasarImpresoraFiscalRG3561.CerrarJornadaFiscalX CerrarJornadaFiscalX()
        {
            var resX = _hasar.CerrarJornadaFiscal(HasarImpresoraFiscalRG3561.TipoReporte.REPORTE_X);
            _log.Info($"Reporte X: {JsonConvert.SerializeObject(resX)}");
            return resX.X;
        }

        public HasarImpresoraFiscalRG3561.CerrarJornadaFiscalZ CerrarJornadaFiscalZ()
        {
            var resZ = _hasar.CerrarJornadaFiscal(HasarImpresoraFiscalRG3561.TipoReporte.REPORTE_Z);
            _log.Info($"Reporte X: {JsonConvert.SerializeObject(resZ)}");
            return resZ.Z;
        }

        public bool EmitirComprobante(HasarImpresoraFiscalRG3561.TiposComprobante tiposComprobante, List<Producto> productos)
        {
            ImprimirFantasia();
            ImprimirEncabezado();

            switch (tiposComprobante)
            {
                case HasarImpresoraFiscalRG3561.TiposComprobante.NO_DOCUMENTO:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.FACTURA_A:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.NOTA_DE_DEBITO_A:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.NOTA_DE_CREDITO_A:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.RECIBO_A:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.FACTURA_B:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.NOTA_DE_DEBITO_B:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.NOTA_DE_CREDITO_B:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.RECIBO_B:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.FACTURA_C:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.NOTA_DE_DEBITO_C:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.NOTA_DE_CREDITO_C:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.RECIBO_C:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.FACTURA_M:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.NOTA_DE_DEBITO_M:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.NOTA_DE_CREDITO_M:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.RECIBO_M:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.INFORME_DIARIO_DE_CIERRE:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_FACTURA_A:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_FACTURA_B:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE:

                    _hasar.AbrirDocumento(tiposComprobante);


                    foreach (var item in productos)
                    {
                        _hasar.ImprimirItem(item.Descripcion, item.Cantidad, double.Parse(item.PrecioUnitario.ToString()), HasarImpresoraFiscalRG3561.CondicionesIVA.GRAVADO,
                            21, HasarImpresoraFiscalRG3561.ModosDeMonto.MODO_SUMA_MONTO, HasarImpresoraFiscalRG3561.ModosDeImpuestosInternos.II_FIJO_MONTO,
                            10, HasarImpresoraFiscalRG3561.ModosDeDisplay.DISPLAY_SI, HasarImpresoraFiscalRG3561.ModosDePrecio.MODO_PRECIO_TOTAL, item.Id.ToString());
                        
                    }



                    _hasar.CerrarDocumento();

                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.REMITO_R:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_NOTA_CREDITO:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_FACTURA_C:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_NOTA_CREDITO_A:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_NOTA_CREDITO_B:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_NOTA_CREDITO_C:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_NOTA_DEBITO_A:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_NOTA_DEBITO_B:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_NOTA_DEBITO_C:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_FACTURA_M:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_NOTA_CREDITO_M:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.TIQUE_NOTA_DEBITO_M:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.REMITO_X:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.RECIBO_X:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.PRESUPUESTO_X:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.INFORME_DE_AUDITORIA:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.REPORTE_RESUMEN_TOTALES:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.COMPROBANTE_DONACION:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.REPORTES_DUPLICADOS_A:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.REPORTE_CTD:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.GENERICO:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.MENSAJE_CF:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.ESTADISTICA_DE_VENTA_HORARIA_Y_POR_RUBRO:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.DETALLE_DE_VENTAS:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.CAMBIO_IVA:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.CAMBIO_FECHA_HORA:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.CAMBIO_CATEGORIZACION_IVA:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.CAMBIO_INSCRIPCION_ING_BRUTOS:
                    break;
                case HasarImpresoraFiscalRG3561.TiposComprobante.PRUEBA_PERIFERICOS:
                    break;
                default:
                    break;
            }

            return true;
        }

        private void ImprimirEncabezado()
        {
            AtributosDeTexto estilo = DefineEstilo(false);
            _hasar.ConfigurarZona(1, estilo, "ENCABEZADO 1", HasarImpresoraFiscalRG3561.TiposDeEstacion.ESTACION_POR_DEFECTO, HasarImpresoraFiscalRG3561.ZonasDeLineasDeUsuario.ZONA_1_ENCABEZADO);
            _hasar.ConfigurarZona(2, estilo, "ENCABEZADO 2", HasarImpresoraFiscalRG3561.TiposDeEstacion.ESTACION_POR_DEFECTO, HasarImpresoraFiscalRG3561.ZonasDeLineasDeUsuario.ZONA_1_ENCABEZADO);
            _hasar.ConfigurarZona(3, estilo, "ENCABEZADO 3", HasarImpresoraFiscalRG3561.TiposDeEstacion.ESTACION_POR_DEFECTO, HasarImpresoraFiscalRG3561.ZonasDeLineasDeUsuario.ZONA_1_ENCABEZADO);

        }

        private void ImprimirFantasia()
        {
            AtributosDeTexto estilo = DefineEstilo(true);

            _hasar.ConfigurarZona(1, estilo, "Óptica", HasarImpresoraFiscalRG3561.TiposDeEstacion.ESTACION_POR_DEFECTO, HasarImpresoraFiscalRG3561.ZonasDeLineasDeUsuario.ZONA_FANTASIA);
            _hasar.ConfigurarZona(2, estilo, "C&M", HasarImpresoraFiscalRG3561.TiposDeEstacion.ESTACION_POR_DEFECTO, HasarImpresoraFiscalRG3561.ZonasDeLineasDeUsuario.ZONA_FANTASIA);

        }

        private static hfl.argentina.Hasar_Funcs.AtributosDeTexto DefineEstilo(bool centrado)
        {
            var estilo = new hfl.argentina.Hasar_Funcs.AtributosDeTexto();
            estilo.setBorradoTexto(false);
            estilo.setCentrado(centrado);
            estilo.setDobleAncho(false);
            estilo.setNegrita(false);
            return estilo;
        }
    }
}
