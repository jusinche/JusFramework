CREATE OR REPLACE PACKAGE PKG_ADM_CATALOGO AS
        /*Obtiene los items de un catalogo*/
     PROCEDURE PRC_ITEM_CATALOGO_OBT( ec_codigo VARCHAR2, sq_resultado out sys_refcursor);
END PKG_ADM_CATALOGO;

CREATE OR REPLACE PACKAGE BODY PKG_ADM_CATALOGO AS
    /*Obtiene los items de un catalogo*/
     PROCEDURE PRC_ITEM_CATALOGO_OBT( ec_codigo VARCHAR2, sq_resultado out sys_refcursor) IS
    BEGIN
            open sq_resultado for
                SELECT ite.ite_id,  ite.ite_codigo, ite.ite_nombre, cat.cat_codigo, cat.cat_nombre
        FROM TADM_CATALOGO cat,TADM_ITEM_CATALOGO ite    where cat.cat_id=ite.cat_id and  cat.cat_codigo=ec_codigo;
    END PRC_ITEM_CATALOGO_OBT;
END PKG_ADM_CATALOGO;
