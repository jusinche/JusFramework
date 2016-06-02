/*select * 
from tseg_sistema sis, tseg_modulo mod, tseg_funcionalidad fun, tseg_operacion ope, tseg_rol_ope_funcionalidad rof, tseg_rol rol, tseg_cue_rol cro, tseg_cuenta cue, tseg_funcionalidad_recurso fre, tseg_recurso rec,
 tseg_fun_operacion fop
where sis.sis_id=mod.sis_id and mod.mod_id=fun.mod_id and fun.fun_id=fre.fun_id and rec.rec_id=fre.rec_id and cue.cue_id=cro.cue_id and rol.rol_id=cro.rol_id and ope.ope_id=fop.ope_id 
	and rof.fop_id=fop.fop_id and rof.rol_id=rol.rol_id;*/
    
    
    
CREATE OR REPLACE PACKAGE PKG_SEG_SEGURIDAD AS
        /*Obiene la cantidad de cuentas de un usuario si es mas de uno es correcto caso controrio es incorrecto*/
        PROCEDURE  PRC_OBT_FUNCIONALIDAD_MENU( ec_usuario VARCHAR2, ec_sistema VARCHAR2, sq_resultado out sys_refcursor) ;
END PKG_SEG_SEGURIDAD;



CREATE OR REPLACE PACKAGE BODY PKG_SEG_SEGURIDAD AS
    /*Obiene la cantidad de cuentas de un usuario si es mas de uno es correcto caso controrio es incorrecto*/
    PROCEDURE  PRC_OBT_FUNCIONALIDAD_MENU( ec_usuario VARCHAR2, ec_sistema VARCHAR2, sq_resultado out sys_refcursor) IS
    BEGIN
    open sq_resultado for
            select FUN.fun_id, (-1*mod.mod_id) mod_id, mod.mod_nombre, mod.mod_nombre_menu,  fun.fun_nombre, fun.fun_codigo, fun.fun_nombre_menu, fun.fun_controlador,fun.fun_accion, fun.fun_parametros
                from tseg_sistema sis, tseg_modulo mod, tseg_funcionalidad fun, tseg_operacion ope, tseg_rol_ope_funcionalidad rfo, tseg_rol rol, tseg_cue_rol cro, tseg_cuenta cue,  tseg_fun_operacion fop
            where sis.sis_id=mod.sis_id and mod.mod_id=fun.mod_id  and cue.cue_id=cro.cue_id and rol.rol_id=cro.rol_id and ope.ope_id=fop.ope_id and fun.fun_id=fop.fun_id
                and rfo.fop_id=fop.fop_id and rfo.rol_id=rol.rol_id and ope.ope_codigo='O' and cue_login= ec_usuario and sis.sis_codigo=ec_sistema;
    END PRC_OBT_FUNCIONALIDAD_MENU;
END PKG_SEG_SEGURIDAD;



select mod.mod_nombre, mod.MOD_NOMBRE_MENU, mod.mod_codigo, fun.fun_nombre, fun.fun_codigo, fun.fun_nombre_menu, ope.ope_codigo, ope.ope_nombre
                from tseg_sistema sis, tseg_modulo mod, tseg_funcionalidad fun, tseg_operacion ope, tseg_rol_ope_funcionalidad rof, tseg_rol rol, tseg_cue_rol cro, tseg_cuenta cue,  tseg_fun_operacion fop
            where sis.sis_id=mod.sis_id and mod.mod_id=fun.mod_id  and cue.cue_id=cro.cue_id and rol.rol_id=cro.rol_id and ope.ope_id=fop.ope_id and fun.fun_id=fop.fun_id
                and rof.fop_id=fop.fop_id and rof.rol_id=rol.rol_id  ;
                
                
                 select rfo.rfo_id, mod.mod_nombre, mod.MOD_NOMBRE_MENU, mod.mod_codigo, fun.fun_nombre, fun.fun_codigo, fun.fun_nombre_menu, ope.ope_codigo, ope.ope_nombre
                from tseg_sistema sis, tseg_modulo mod, tseg_funcionalidad fun, tseg_operacion ope, tseg_rol_ope_funcionalidad rfo, tseg_rol rol, tseg_cue_rol cro, tseg_cuenta cue,  tseg_fun_operacion fop
            where sis.sis_id=mod.sis_id and mod.mod_id=fun.mod_id  and cue.cue_id=cro.cue_id and rol.rol_id=cro.rol_id and ope.ope_id=fop.ope_id and fun.fun_id=fop.fun_id
                and rfo.fop_id=fop.fop_id and rfo.rol_id=rol.rol_id and ope.ope_codigo='O' and cue_login= 'admin' and sis.sis_codigo='SIS001'
                ;