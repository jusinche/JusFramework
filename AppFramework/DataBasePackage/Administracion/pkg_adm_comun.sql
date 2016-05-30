CREATE OR REPLACE PACKAGE PKG_ADM_COMUN AS
        /*Obiene la fecha del sistema*/
        function fnc_obt_fecha_actual return date;
END PKG_ADM_COMUN;

CREATE OR REPLACE PACKAGE BODY PKG_ADM_COMUN AS
    /*Obiene la fecha del sistema*/
        function fnc_obt_fecha_actual return date is
        ln_fecha date;
        begin
            select sysdate into ln_fecha from dual;
            return ln_fecha;
        end fnc_obt_fecha_actual;    
END PKG_ADM_COMUN;
