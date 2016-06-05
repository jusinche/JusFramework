CREATE OR REPLACE PACKAGE PKG_AUDITORIA AS
    CC_INSERTAR VARCHAR2(10):='I';
    CC_ACTUALIZAR VARCHAR2(10):='A';
    CC_BORRAR VARCHAR2(10):='B';
    TYPE array_datos IS TABLE OF VARCHAR2 (4000);
        /*Permite realizar la auditoria de las tablas modificadas*/
PROCEDURE AUDITAR(en_id number, ec_table varchar2, ec_usuario varchar2, ec_operacion varchar2);
END PKG_AUDITORIA;

CREATE OR REPLACE PACKAGE BODY PKG_AUDITORIA AS
     /*Permite realizar la auditoria de las tablas modificadas*/
PROCEDURE AUDITAR(en_id number, ec_table varchar2, ec_usuario varchar2, ec_operacion varchar2) IS
    lc_select varchar2(4000);
    lc_primary varchar2(100);    
    lista_datos   array_datos := array_datos ();   
    lc_datos varchar2(4000);
    ln_ope number;
    p_result_set  sys_refcursor;
     CURSOR lq_columnas (ec_tabla_nombre varchar2) IS      
      select column_name from all_tab_columns where table_name =ec_tabla_nombre;  
  begin 
    lc_select:= 'SELECT ''{';
     FOR lr_columnas IN  lq_columnas(ec_table)       LOOP
           lc_select:= lc_select||'"'||lr_columnas.column_name||'":"''||'||lr_columnas.column_name||'||''"''||'',';
     END LOOP;
     lc_select:=RTRIM(lc_select, ',');
     lc_select:=lc_select||'}'' DATOS';
      SELECT cols.column_name into lc_primary  FROM all_constraints cons, all_cons_columns cols
        WHERE cons.constraint_type = 'P'  AND cons.constraint_name = cols.constraint_name  AND cons.owner = cols.owner AND COLS.table_name =ec_table and rownum=1;     
     lc_select:= lc_select||' from '||ec_table||' WHERE '||lc_primary||' = ' ||en_id;     
     execute immediate lc_select bulk collect into lista_datos;     
    -- dbms_output.put_line(lista_datos(lista_datos.last));
    lc_datos:=lista_datos(lista_datos.last);
    select ope_id into ln_ope from tseg_operacion where ope_codigo=ec_operacion;
    INSERT INTO TAUD_AUDITORIA (AUD_ID_REGISTRO,OPE_ID, AUD_USUARIO, AUD_TABLA,    AUD_FECHA, AUD_DATOS,AUD_ID) 
        VALUES (en_id, ln_ope,  ec_usuario, ec_table,  PKG_ADM_COMUN.fnc_obt_fecha_actual, lc_datos, SAUD_AUDITORIA.nextval);  
    EXCEPTION
    when others   then
        raise_application_error(- 20001, 'en_id:' ||en_id|| ' ec_table :' ||ec_table|| '  ec_usuario:' ||ec_usuario|| ' ec_operacion:' ||ec_operacion);
  end;
END PKG_AUDITORIA;



 Declare
  en_id number:=1;
  ec_table varchar2(100):='TSEG_CUENTA';
  lc_select varchar2(4000);
  lc_primary varchar2(100);  
  TYPE array_datos IS TABLE OF VARCHAR2 (4000);  
  lista_datos   array_datos := array_datos ();   
    p_result_set  sys_refcursor;
  CURSOR lq_columnas (ec_tabla_nombre varchar2) IS      
      select column_name from all_tab_columns where table_name =ec_tabla_nombre;  
  begin 
    lc_select:= 'SELECT ''{';
     FOR lr_columnas IN  lq_columnas(ec_table)       LOOP
           lc_select:= lc_select||'"'||lr_columnas.column_name||'":"''||'||lr_columnas.column_name||'||''"''||'',';
           --  
     END LOOP;
     dbms_output.put_line(lc_select);  
     lc_select:=RTRIM(lc_select, ',');
     lc_select:=lc_select||'}'' DATOS';
      SELECT cols.column_name into lc_primary  FROM all_constraints cons, all_cons_columns cols
        WHERE cons.constraint_type = 'P'  AND cons.constraint_name = cols.constraint_name  AND cons.owner = cols.owner AND COLS.table_name =ec_table and rownum=1;     
     lc_select:= lc_select||' from '||ec_table||' WHERE '||lc_primary||' = ' ||en_id;
     
     dbms_output.put_line(lc_select);  
     
     execute immediate lc_select bulk collect into lista_datos;     
     dbms_output.put_line(lista_datos(lista_datos.last));          
    
   
    
    
  end;
  
  
   select column_name from all_tab_columns where table_name ='TSEG_CUENTA';  