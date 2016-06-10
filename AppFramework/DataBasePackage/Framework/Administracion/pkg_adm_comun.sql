CREATE OR REPLACE PACKAGE PKG_ADM_COMUN AS
        /*Obiene la fecha del sistema*/
        function fnc_obt_fecha_actual return date;
        /*Elimina un grupo de registro dado el nombre de la tabla y el nombre del padre id*/
        procedure PRC_ELIMINAR(ec_tabla varchar2, ec_campo varchar2, en_id number );
END PKG_ADM_COMUN;

CREATE OR REPLACE PACKAGE BODY PKG_ADM_COMUN AS
    /*Obiene la fecha del sistema*/
        function fnc_obt_fecha_actual return date is
        ln_fecha date;
        begin
            select sysdate into ln_fecha from dual;
            return ln_fecha;
        end fnc_obt_fecha_actual;
        procedure PRC_ELIMINAR(ec_tabla varchar2, ec_campo varchar2, en_id number ) IS
        lc_delete varchar2(300);
        BEGIN
            lc_delete:= 'DELETE '||ec_tabla || ' WHERE '|| ec_campo||' = ' ||en_id;
            EXECUTE IMMEDIATE lc_delete;
        END PRC_ELIMINAR;
END PKG_ADM_COMUN;
