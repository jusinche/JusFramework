declare
ln_rol_id number;
ln_cant number;
CURSOR lq_func_operacion   IS
        select fop.* 
            from tseg_sistema sis, tseg_modulo mod, tseg_funcionalidad fun,  tseg_fun_operacion fop
        where sis.sis_id=mod.sis_id and mod.mod_id=fun.mod_id and  fun.fun_id=fop.fun_id ;
begin
    select rol_id into ln_rol_id from tseg_rol where rol_nombre ='Administrador';
     FOR lr_func_operacion    IN lq_func_operacion   LOOP
        select count(*) into ln_cant from TSEG_ROL_OPE_FUNCIONALIDAD where rol_id=ln_rol_id and fop_id =lr_func_operacion.fop_id;
      if (ln_cant=0) then
        INSERT INTO TSEG_ROL_OPE_FUNCIONALIDAD ( ROL_ID, RFO_USUARIO_MOD,  FOP_ID, RFO_ID) 
        VALUES ( ln_rol_id, 'admin',lr_func_operacion.fop_id,     SSEG_ROL_OPE_FUNCIONALIDAD.nextval);
      end if;
    END LOOP;
end;