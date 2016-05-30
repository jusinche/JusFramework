using System;
using System.Data.Common;
using Oracle.DataAccess.Client;

namespace Jus.Plataforma.Dal
{
    /// <summary>
    /// Estereotipo : Class
    /// Responsabilidad : Traductor de exceptiones Oracle
    /// </summary>
    public class OracleDBException : Exception
    {
        /// <summary>
        /// Deuelve una Exception
        /// </summary>
        /// <param name="ex">DbException</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>SyDataBaseException</returns>
        /*public override SyDataBaseException Handler(DbException ex, string infoLog)
        {
            //TODO: FALTA TRATAR MENSAJES DE CADA EXCEPCION MAS MENSAJE DE LOGS
            SyDataBaseException objeto = null;
            string msg = string.Empty;

            //TODO JSA, GENERAR EL MENSAJE A PARTIR DEL CODIGO DEL ERROR PARA NO ESTAR QUEMANDO LOS MENSAJES 
            //CodigosErroresOracle

            if (ex != null)
            {
                var exception = ex as OracleException;
                if (exception != null)
                {
                    switch (exception.Number)
                    {
                        case CodigosErroresOracle.REFERENTIAL_INTEGRITY:
                            msg = "REFERENTIAL_INTEGRITY\n" + infoLog;
                            objeto = new SyDataBaseIntegrityReferential(msg, exception);
                            break;
                        case CodigosErroresOracle.PARENT_KEY_NOT_FOUND:
                            //TODO: JSA NO SE DEBE COLOCAR MENSAJES AMIGABLES EN EXCEPCIONES DE BASE DE DATOS, EXISTE UNA CLASE QUE VISUALIZA MENSAJES AMIGABLES SEGUN UNA CONFIGURACION
                            msg =
                                "La Operación no se pudo Completar, <br/>Los datos han sido modificados <br/>Recarge la Ventana\n" +
                                infoLog;
                            objeto = new SyDataBaseParentKeyNotFound(msg, exception);
                            break;
                        case CodigosErroresOracle.NOT_DATA_FOUND:
                            //TODO: JSA, REVISAR LA INTERPRETACION DEL ERROR
                            msg = "NOT_DATA_FOUND\n" + infoLog;

                            objeto = new SyDataBaseNoDataFound(msg, exception);
                            break;
                            //case CodigosErroresOracle.ACCESS_INTO_NULL:
                            //    objeto = new SyDataBaseAccesIntoNull("ACCESS_INTO_NULL", exception);
                            break;
                        case CodigosErroresOracle.CASE_NOT_FOUND:

                            msg = "CASE_NOT_FOUND\n" + infoLog;
                            objeto = new SyDataBaseCaseNotFound(msg, exception);
                            break;
                        case CodigosErroresOracle.CURSOR_ALREADY_OPEN:

                            objeto = new SyDataBaseCursorAlreadyOpen(infoLog, exception);
                            break;
                        case CodigosErroresOracle.DUP_VAL_ON_INDEX:

                            msg = "DUP_VAL_ON_INDEX\n" + infoLog;

                            objeto = new SyDataBaseDupValOnIndex(msg, exception);
                            break;
                        case CodigosErroresOracle.INVALID_CURSOR:
                            msg = "INVALID_CURSOR\n" + infoLog;

                            objeto = new SyDataBaseInvalidCursor(msg, exception);
                            break;
                        case CodigosErroresOracle.INVALID_NUMBER:
                            msg = "INVALID_NUMBER\n" + infoLog;
                            objeto = new SyDataBaseInvalidNumber(msg, exception);
                            break;
                        case CodigosErroresOracle.LOGIN_DENIED:
                            msg = "NOT_LOGGED_ON\n" + infoLog;
                            objeto = new SyDataBaseLoginDeneid(msg, exception);
                            break;
                        case CodigosErroresOracle.NOT_LOGGED_ON:

                            objeto = new SyDataBaseNotLoggedOn(infoLog, exception);
                            break;
                        case CodigosErroresOracle.PACKAGE_ERROR_BODY:
                            msg = "PACKAGE_ERROR_BODY\n" + infoLog;
                            objeto = new SyDataBasePackageErrorBody(msg, exception);
                            break;
                        case CodigosErroresOracle.STORAGE_ERROR:
                            objeto = new SyDataBaseStorageError(infoLog, exception);
                            break;
                        case CodigosErroresOracle.SUBSCRIPT_BEYOND_COUNT:
                            objeto = new SyDatabaseSubscriptBeyondCount(infoLog, exception);
                            break;
                        case CodigosErroresOracle.SUBSCRIPT_OUTSIDE_LIMIT:
                            objeto = new SyDataBaseSubscriptOutsideLimit(infoLog, exception);
                            break;
                        case CodigosErroresOracle.SYS_INVALID_ROWID:
                            objeto = new SyDataBaseSysInvalidRowId(infoLog, exception);
                            break;
                        case CodigosErroresOracle.TIMEOUT_ON_RESOURCE:
                            objeto = new SyDataBaseTimeoutOnResource(infoLog, exception);
                            break;
                        case CodigosErroresOracle.TOO_MANY_ROWS:
                            objeto = new SyDataBaseTooManyRows(infoLog, exception);
                            break;
                        case CodigosErroresOracle.VALUE_ERROR:
                            objeto = new SyDataBaseValueError(infoLog, exception);
                            break;
                        case CodigosErroresOracle.ZERO_DIVIDE:
                            msg = "ZERO_DIVIDE\n" + infoLog;
                            objeto = new SyDataBaseZeroDivide(msg, exception);
                            break;

                            //CODIGOS PERSONALIZADOS    
                        case CodigosErroresOracle.CONCURRENCIA_EXCEPCION:
                            var messageFriend = MessageOracleExceptionParser.Parser(exception);
                            objeto = new SyDataBaseConcurrencia(infoLog, exception, messageFriend);
                            break;
                            //CODIGOS PERSONALIZADOS    
                        case CodigosErroresOracle.DATA_DUPLICATE_EXCEPCION:
                            var messageAmigable = MessageOracleExceptionParser.Parser(exception);
                            objeto = new SyDataBaseCuentaDuplicadaException(infoLog, exception, messageAmigable);
                            break;
                        //CODIGOS PERSONALIZADOS    
                        case CodigosErroresOracle.CUPOS_NO_DISPONIBLES_EXCEPCION:
                            var messageCupos = MessageOracleExceptionParser.Parser(exception);
                            objeto = new SyDataBaseCuposNoDisponiblesException(infoLog, exception, messageCupos);
                            break;
                        default:
                            objeto = new SyDataBaseException(infoLog, exception);
                            break;
                    }
                }
                else
                {
                    throw new ArgumentException("La excepcion debe ser de Tipo Oracle Exeption");
                }
            }
            else
            {
                throw new ArgumentNullException("Excepcion no puede ser nula" + ex);
            }

            return objeto;
        }
         * */
    }
}