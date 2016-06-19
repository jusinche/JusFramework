CREATE OR REPLACE PACKAGE PKG_NEG_CORREO AS    
     /*Permite INSERTAR UN correo*/
    PROCEDURE PRC_CORREO_INS(en_padre number, ec_correo varchar2, ec_usuario varchar2, sn_id out number);
    /*Permite actualizar UN TNEG_CORREO*/
   PROCEDURE PRC_CORREO_ACT( ec_correo varchar2, en_id number, ec_usuario varchar2, en_version number, sn_reg_modificados out number);
   --ELIMINA una persona por su id    
    PROCEDURE PRC_CORREO_DEL(en_id number,en_version number, ec_usuario varchar2, sn_reg_modificados out number);
    --Obtiene un lista de correos persona por su id (en_padre)
    PROCEDURE PRC_CORREO_OBT(en_padre number,sq_resultado out sys_refcursor);
 END PKG_NEG_CORREO;

CREATE OR REPLACE PACKAGE BODY PKG_NEG_CORREO AS
     /*Permite INSERTAR UN correo*/
    PROCEDURE PRC_CORREO_INS(en_padre number, ec_correo varchar2, ec_usuario varchar2, sn_id out number) IS
    BEGIN 
          select SNEG_CORREO.nextval into sn_id from dual;
          INSERT INTO TNEG_CORREO ( PER_ID, COR_USUARIO_MOD,  COR_ID, COR_CORREO) 
        VALUES ( en_padre,ec_usuario ,sn_id, ec_correo);
    END PRC_CORREO_INS;
    /*Permite actualizar UN TNEG_CORREO*/
    PROCEDURE PRC_CORREO_ACT( ec_correo varchar2, en_id number, ec_usuario varchar2, en_version number, sn_reg_modificados out number) IS
    BEGIN
            PKG_AUDITORIA.AUDITAR(en_ID , 'TNEG_CORREO' , ec_usuario , PKG_AUDITORIA.CC_ACTUALIZAR );                                              
            UPDATE TNEG_CORREO
            SET COR_VERSION     = COR_VERSION+1,
                   COR_USUARIO_MOD = ec_usuario,                   
                   COR_FECHA_MOD   = PKG_ADM_COMUN.fnc_obt_fecha_actual,
                   COR_CORREO      = ec_correo
            WHERE  COR_ID          = en_id  and  cor_VERSION =en_version;
             sn_reg_modificados := SQL%ROWCOUNT;  
    END PRC_CORREO_ACT;
    --ELIMINA un correo por su id
    PROCEDURE PRC_CORREO_DEL(en_id number,en_version number, ec_usuario varchar2, sn_reg_modificados out number)IS
    BEGIN
        PKG_AUDITORIA.AUDITAR(en_ID , 'TNEG_CORREO' , ec_usuario , PKG_AUDITORIA.CC_BORRAR );
         DELETE TNEG_CORREO WHERE cor_id=en_id and  cor_VERSION =en_version;
         sn_reg_modificados := SQL%ROWCOUNT;  
    END PRC_CORREO_DEL;
    --Obtiene un lista de correos persona por su id (en_padre)
    PROCEDURE PRC_CORREO_OBT(en_padre number,sq_resultado out sys_refcursor) IS
    BEGIN
        OPEN sq_resultado FOR
        SELECT PER_ID, COR_VERSION n_version, COR_USUARIO_MOD,    COR_ID n_id, COR_FECHA_MOD, COR_CORREO
            FROM TNEG_CORREO cor where  cor.per_id=en_padre;        
    END PRC_CORREO_OBT;    
END PKG_NEG_CORREO;


