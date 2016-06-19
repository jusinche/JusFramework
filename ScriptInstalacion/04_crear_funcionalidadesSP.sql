create or replace procedure prc_ins_funcionalidades(--datos del modulo
ec_codigo_modulo VARCHAR2,ec_nombre_modulo VARCHAR2,ec_menu_modulo VARCHAR2,ec_sistema_codigo VARCHAR2,
--datos de la funcionalidad
ec_codigo_funcionalidad  VARCHAR2,ec_nombre_funcionalidad  VARCHAR2,ec_nombre_menu  VARCHAR2,ec_controlador  VARCHAR2,
ec_accion varchar2,ec_fun_parametros  VARCHAR2,ec_estado_fun  VARCHAR2,ec_operaciones  VARCHAR2,ec_modulo_codigo  VARCHAR2 )IS 
 --obtener los datos del excel de catalogos para que sean insertados
 ln_cant number;
 ln_mod_id number;
 ln_fun_id number;
 ln_sis_id number;
 ln_estado_fun number;
 CURSOR lq_permisos(ec_parametros varchar2) IS
 select permiso from (with t as (select ec_parametros as txt from dual)
        select REGEXP_SUBSTR (txt, '[^,]+', 1, level) permiso from t connect by level <= length(regexp_replace(txt,'[^,]*'))+1);
begin
    SELECT COUNT(*) INTO ln_cant FROM tseg_modulo WHERE mod_codigo=ec_codigo_modulo;    
    if(ln_cant=0) then 
       select sis_id into ln_sis_id from TSEG_SISTEMA WHERE sis_codigo=ec_sistema_codigo;
        select SSEG_MODULO.nextval into ln_mod_id from dual;
       INSERT INTO TSEG_MODULO (   SIS_ID, MOD_NOMBRE_MENU, MOD_NOMBRE,    MOD_ID, MOD_CODIGO) 
        VALUES ( ln_sis_id, ec_nombre_modulo,ec_menu_modulo, ln_mod_id,ec_codigo_modulo); 
         dbms_output.put_line('insertar modulo '||ec_codigo_modulo||' - '||ec_menu_modulo);
    ELSE
         SELECT mod_id into ln_mod_id from tseg_modulo WHERE mod_codigo=ec_codigo_modulo;
        dbms_output.put_line('actualizar modulo'||ec_codigo_modulo||ec_menu_modulo);
        UPDATE TSEG_MODULO      SET 
               MOD_NOMBRE_MENU = ec_menu_modulo,
               MOD_NOMBRE      = ec_nombre_modulo               
        WHERE  MOD_CODIGO = ec_codigo_modulo;
    END IF;
    select count(*) into ln_cant from tseg_funcionalidad fun where fun_codigo=ec_codigo_funcionalidad;
    select ITE.itE_id into ln_estado_fun from tadm_catalogo cat, tadm_item_catalogo ite 
            where cat.cat_id=ite.cat_id and cat.cat_codigo='FUNCIONALIDADESTADO' and ite.ite_codigo=ec_estado_fun;
     if(ln_cant=0) then        
        select SSEG_FUNCIONALIDAD.nextval into ln_fun_id from dual;
         INSERT INTO TSEG_FUNCIONALIDAD (   MOD_ID, FUN_controlador,fun_accion, FUN_PARAMETROS,    FUN_NOMBRE_MENU, FUN_NOMBRE, FUN_ID,    FUN_ESTADO, FUN_CODIGO) 
            VALUES (ln_mod_id,ec_controlador,ec_accion,ec_fun_parametros, ec_nombre_menu,ec_nombre_funcionalidad, ln_fun_id, ln_estado_fun ,ec_codigo_funcionalidad);
         dbms_output.put_line('insertar funcionalidad '||ec_nombre_funcionalidad);
    ELSE
        SELECT fun_id into ln_fun_id from tseg_funcionalidad fun where fun_codigo=ec_codigo_funcionalidad;
        UPDATE TSEG_FUNCIONALIDAD SET  
               fun_parametros  = ec_fun_parametros,
               fun_nombre_menu = ec_nombre_menu,
               fun_nombre      = ec_nombre_funcionalidad,
               fun_controlador = ec_controlador,
               fun_accion      =ec_accion,
               fun_estado=ln_estado_fun,
               mod_id=ln_mod_id
            WHERE  FUN_CODIGO  = ec_codigo_funcionalidad;
            dbms_output.put_line('actualizar funcionalidad '||ec_nombre_funcionalidad);
    END IF;
     FOR lr_permiso IN  lq_permisos(ec_operaciones)   LOOP
        select count(*) into ln_cant from  TSEG_OPERACION OPE, TSEG_FUN_OPERACION fop 
            where OPE.OPE_ID=fop.ope_id and ope.OPE_CODIGO= lr_permiso.permiso and fop.fun_id=ln_fun_id;
          if ln_cant =0 then
             INSERT INTO TSEG_FUN_OPERACION (OPE_ID, FUN_ID, FOP_ID) values
                        ( (SELECT ope.OPE_ID FROM  TSEG_OPERACION OPE WHERE ope.OPE_CODIGO= lr_permiso.permiso),ln_fun_id,SSEG_FUN_OPERACION.NEXTVAL);
          dbms_output.put_line('insertar operacion '|| lr_permiso.permiso);
          end if;
          
     end loop;
     EXCEPTION  WHEN OTHERS THEN  -- handles all other errors
        DBMS_OUTPUT.PUT_LINE (SQLERRM);
END; 

