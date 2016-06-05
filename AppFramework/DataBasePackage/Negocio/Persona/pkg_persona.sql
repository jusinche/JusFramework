CREATE OR REPLACE PACKAGE PKG_NEG_PERSONA AS    
    /*Permite INSERTA UNA PERSONA NATURAL*/
    PROCEDURE PRC_PERSONA_INS(en_tipo_identificacion number, ec_identificacion varchar2, ec_primer_nombre varchar2, ec_segundo_nombre varchar2, ec_primer_apellido varchar2, ec_segundo_apellido varchar2,
                                                    en_tipo_persona number,ef_fecha_nac date, en_genero number, en_estado_civil number, ec_usuario varchar2, sn_id out number);
   /*Permite actualizar UNA PERSONA NATURAL*/
    PROCEDURE PRC_PERSONA_ACT( ec_primer_nombre varchar2, ec_segundo_nombre varchar2, ec_primer_apellido varchar2, ec_segundo_apellido varchar2,
                                                   ef_fecha_nac date, en_genero number, en_estado_civil number, en_id number, ec_usuario varchar2, en_version number, sn_reg_modificados out number);
   --ELIMINA una persona por su id    
    PROCEDURE PRC_PERSONA_DEL(en_id number,en_version number, ec_usuario varchar2, sn_reg_modificados out number);
    --Obtiene una persona por su id
    PROCEDURE PRC_PERSONA_OBT(en_id number,sq_resultado out sys_refcursor);
    --Obtiene un listado de personas segun el criterio
    PROCEDURE PRC_PERSONA_OBT(ec_identificacion varchar2, ec_primer_nombre varchar2, ec_segundo_nombre varchar2, ec_primer_apellido varchar2, ec_segundo_apellido varchar2,sq_resultado out sys_refcursor);
END PKG_NEG_PERSONA;

CREATE OR REPLACE PACKAGE BODY PKG_NEG_PERSONA AS
     /*Permite INSERTA UNA PERSONA NATURAL*/
    PROCEDURE PRC_PERSONA_INS(en_tipo_identificacion number, ec_identificacion varchar2, ec_primer_nombre varchar2, ec_segundo_nombre varchar2, ec_primer_apellido varchar2, ec_segundo_apellido varchar2,
                                                    en_tipo_persona number,ef_fecha_nac date, en_genero number, en_estado_civil number, ec_usuario varchar2, sn_id out number) IS
    BEGIN 
          select SNEG_PERSONA.nextval into sn_id from dual;
           INSERT INTO TNEG_PERSONA (PER_USUARIO_MOD, PER_TIPO_IDENTIFICACION,    PER_TIPO, PER_NOMBRE, PER_IDENTIFICACION,  PER_ID) 
            VALUES ( ec_usuario,  en_tipo_identificacion,en_tipo_persona,  ec_primer_apellido||' '||ec_segundo_apellido||' '||ec_primer_nombre||' '|| ec_segundo_nombre ,    ec_identificacion,  sn_id );
         INSERT INTO TNEG_PERSONA_NATURAL (   PER_USUARIO_MOD, PER_PRIMER_NOMBRE, PER_SEGUNDO_NOMBRE,   PER_PRIMER_APELLIDO,  PER_SEGUNDO_APELLIDO, 
                                             PER_ID, PER_GENERO, PER_FECHA_NACIMIENTO,   PER_ESTADO_CIVIL) 
            VALUES ( ec_usuario, ec_primer_nombre, ec_segundo_nombre ,ec_primer_apellido,ec_segundo_apellido,
             sn_id,en_genero,ef_fecha_nac, en_estado_civil);
    END PRC_PERSONA_INS;
    /*Permite actualizar UNA PERSONA NATURAL*/
    PROCEDURE PRC_PERSONA_ACT( ec_primer_nombre varchar2, ec_segundo_nombre varchar2, ec_primer_apellido varchar2, ec_segundo_apellido varchar2,
                                                   ef_fecha_nac date, en_genero number, en_estado_civil number, en_id number, ec_usuario varchar2, en_version number, sn_reg_modificados out number) IS
    BEGIN
            PKG_AUDITORIA.AUDITAR(en_ID , 'TNEG_PERSONA_NATURAL' , ec_usuario , PKG_AUDITORIA.CC_ACTUALIZAR );                                              
            UPDATE TNEG_PERSONA
                        SET    PER_VERSION             = PER_VERSION+1,
                               PER_NOMBRE              = ec_primer_apellido||' '||ec_segundo_apellido||' '||ec_primer_nombre||' '|| ec_segundo_nombre , 
                               PER_FECHA_MOD           =PKG_ADM_COMUN.fnc_obt_fecha_actual
                        WHERE  PER_ID                  =en_id and  PER_VERSION =en_version;            
            UPDATE TNEG_PERSONA_NATURAL      SET   
               PER_USUARIO_MOD      = ec_usuario,
               PER_SEGUNDO_NOMBRE   = ec_segundo_nombre,
               PER_SEGUNDO_APELLIDO =ec_segundo_apellido,
               PER_PRIMER_NOMBRE    = ec_primer_nombre,
               PER_PRIMER_APELLIDO  =ec_primer_apellido,
               PER_GENERO           = en_genero,
               PER_FECHA_NACIMIENTO = ef_fecha_nac,
               PER_FECHA_MOD        = PKG_ADM_COMUN.fnc_obt_fecha_actual,
               PER_ESTADO_CIVIL     = en_estado_civil,
               PER_VERSION=PER_VERSION+1
        WHERE  PER_ID   = en_id and  PER_VERSION =en_version;
         sn_reg_modificados := SQL%ROWCOUNT;  
    END PRC_PERSONA_ACT;
    --ELIMINA una persona por su id
    PROCEDURE PRC_PERSONA_DEL(en_id number,en_version number, ec_usuario varchar2, sn_reg_modificados out number)IS
    BEGIN
        PKG_AUDITORIA.AUDITAR(en_ID , 'TNEG_PERSONA_NATURAL' , ec_usuario , PKG_AUDITORIA.CC_BORRAR );    
        DELETE TNEG_PERSONA_NATURAL WHERE per_id=en_id and  PER_VERSION =en_version;
         DELETE TNEG_PERSONA WHERE per_id=en_id and  PER_VERSION =en_version;
         sn_reg_modificados := SQL%ROWCOUNT;  
    END PRC_PERSONA_DEL;
    --Obtiene una persona por su id
    PROCEDURE PRC_PERSONA_OBT(en_id number,sq_resultado out sys_refcursor) IS
    BEGIN
        OPEN sq_resultado FOR
        SELECT per.per_id n_id,  per.PER_VERSION n_version, per1.per_tipo_identificacion, per1.per_identificacion,  PER_SEGUNDO_NOMBRE, 
                       PER_SEGUNDO_APELLIDO, PER_PRIMER_NOMBRE, PER_PRIMER_APELLIDO, 
                        PER_GENERO, PER_FECHA_NACIMIENTO, PER_ESTADO_CIVIL
                    FROM TNEG_PERSONA_NATURAL PER, TNEG_PERSONA per1
                    WHERE per.per_id=per1.per_id and per.per_id=en_id;
    END PRC_PERSONA_OBT;
    --Obtiene un listado de personas segun el criterio
    PROCEDURE PRC_PERSONA_OBT(ec_identificacion varchar2, ec_primer_nombre varchar2, ec_segundo_nombre varchar2, ec_primer_apellido varchar2, ec_segundo_apellido varchar2,sq_resultado out sys_refcursor) IS
    BEGIN
        OPEN sq_resultado FOR
        SELECT per.per_id, per1.per_tipo_identificacion, tipo_id.ite_nombre tipo_identificacion, per1.per_identificacion,  PER_SEGUNDO_NOMBRE, 
                       PER_SEGUNDO_APELLIDO, PER_PRIMER_NOMBRE, PER_PRIMER_APELLIDO, 
                        PER_GENERO, genero.ite_nombre genero, PER_FECHA_NACIMIENTO, PER_ESTADO_CIVIL, estado_civil.ite_nombre estado_civil
                    FROM TNEG_PERSONA_NATURAL PER, TNEG_PERSONA per1, tadm_item_catalogo tipo_id, tadm_item_catalogo genero, tadm_item_catalogo estado_civil
                    WHERE per.per_id=per1.per_id and per1.per_tipo_identificacion=tipo_id.ite_id and per.per_genero=GENERO.ITE_ID and per.per_estado_civil=estado_civil.ite_id
                    and (ec_identificacion is null or per1.per_identificacion like '%'||ec_identificacion||'%')
                    and (ec_primer_nombre is null or per.per_primer_nombre like '%'||ec_primer_nombre||'%')
                    and (ec_segundo_nombre is null or per.per_segundo_nombre like '%'||ec_segundo_nombre||'%')
                    and (ec_primer_apellido is null or per.per_primer_apellido like '%'||ec_primer_apellido||'%')
                    and (ec_segundo_apellido is null or per.per_segundo_apellido like '%'||ec_segundo_apellido||'%');
    END PRC_PERSONA_OBT;
END PKG_NEG_PERSONA;


