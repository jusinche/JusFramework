CREATE OR REPLACE PACKAGE PKG_SEG_CUENTA AS
        /*Obiene la cantidad de cuentas de un usuario si es mas de uno es correcto caso controrio es incorrecto*/
        PROCEDURE PRC_OBT_CANT_CUENTA( ec_usuario VARCHAR2, ec_clave VARCHAR2, sn_cantidad out NUMBER);
         /*Obiene la cuenta  de cuentas de un usuario */
        PROCEDURE PRC_OBT_CUENTA( ec_usuario VARCHAR2, sq_resultado out sys_refcursor);
         /*Obiene la cuenta  de cuentas de un usuario */
        PROCEDURE PRC_ACT_CUENTA( en_per_id NUMBER,   ef_cue_ultimo_acceso DATE, en_cue_estado NUMBER, en_cue_accesos NUMBER,
                                 ec_cue_en_linea VARCHAR2, ec_login VARCHAR2,  en_id NUMBER,  ec_usuario VARCHAR2, en_version NUMBER, sn_reg_modificados out NUMBER ) ;
END PKG_SEG_CUENTA;

CREATE OR REPLACE PACKAGE BODY PKG_SEG_CUENTA AS
    /*Obiene la cantidad de cuentas de un usuario si es mas de uno es correcto caso controrio es incorrecto*/
    PROCEDURE PRC_OBT_CANT_CUENTA( ec_usuario VARCHAR2, ec_clave VARCHAR2, sn_cantidad out NUMBER) IS
    BEGIN
            select count(*) into sn_cantidad from tseg_cuenta where cue_login=ec_usuario and cue_clave=ec_clave;
    END PRC_OBT_CANT_CUENTA;
    /*Obiene la cuenta  de cuentas de un usuario */
    PROCEDURE PRC_OBT_CUENTA( ec_usuario VARCHAR2, sq_resultado out sys_refcursor) IS
    BEGIN
            open sq_resultado for
                SELECT PER_ID,   CUE_ULTIMO_ACCESO, CUE_LOGIN,  CUE_ESTADO, CUE_EN_LINEA,    CUE_CLAVE,
                     CUE_CAMBIO_CLAVE, CUE_ACCESOS, CUE_VERSION n_version,CUE_ID n_id
        FROM TSEG_CUENTA    where CUE_LOGIN=ec_usuario;
    END PRC_OBT_CUENTA;
     /*Obiene la cuenta  de cuentas de un usuario */
    PROCEDURE PRC_ACT_CUENTA( en_per_id NUMBER,   ef_cue_ultimo_acceso DATE, en_cue_estado NUMBER, en_cue_accesos NUMBER,
                                 ec_cue_en_linea VARCHAR2, ec_login VARCHAR2,  en_id NUMBER,  ec_usuario VARCHAR2, en_version NUMBER, sn_reg_modificados out NUMBER ) IS
   lnm number;
   
 BEGIN
        PKG_AUDITORIA.AUDITAR(en_ID , 'TSEG_CUENTA' , ec_usuario , PKG_AUDITORIA.CC_ACTUALIZAR );                                              
       update  TSEG_CUENTA set PER_ID=en_PER_ID, CUE_VERSION=CUE_VERSION+1, CUE_USUARIO_MOD=ec_USUARIO, CUE_ULTIMO_ACCESO=ef_CUE_ULTIMO_ACCESO, 
                    CUE_FECHA_MOD=PKG_ADM_COMUN.fnc_obt_fecha_actual, CUE_ESTADO=EN_CUE_ESTADO, CUE_EN_LINEA=ec_CUE_EN_LINEA, CUE_ACCESOS=en_CUE_ACCESOS
       where CUE_ID=en_id and CUE_VERSION=en_version;
        sn_reg_modificados := SQL%ROWCOUNT;  
    END PRC_ACT_CUENTA;    
END PKG_SEG_CUENTA;

