CREATE OR REPLACE PACKAGE PKG_SEG_FUNCIONALIDAD AS
        /*Obiene el menu de un usuario*/
        PROCEDURE PRC_OBT_MENU_ITEM(ec_usuario VARCHAR2, ec_sistema VARCHAR2, sq_resultado out sys_refcursor);
END PKG_SEG_FUNCIONALIDAD;

CREATE OR REPLACE PACKAGE BODY PKG_SEG_FUNCIONALIDAD AS    
    /*Obiene la cuenta  de cuentas de un usuario */
    PROCEDURE PRC_OBT_MENU_ITEM(ec_usuario VARCHAR2, ec_sistema VARCHAR2, sq_resultado out sys_refcursor) IS
    BEGIN
            open sq_resultado for
                SELECT PER_ID,   CUE_ULTIMO_ACCESO, CUE_LOGIN,  CUE_ESTADO, CUE_EN_LINEA,    CUE_CLAVE,
                     CUE_CAMBIO_CLAVE, CUE_ACCESOS, CUE_VERSION n_version,CUE_ID n_id
        FROM TSEG_CUENTA    where CUE_LOGIN=ec_usuario;
    END PRC_OBT_MENU_ITEM;  
END PKG_SEG_FUNCIONALIDAD;

