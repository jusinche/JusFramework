declare
CURSOR lq_personas IS
SELECT per.* FROM TNEG_PERSONA per, tneg_identificacion ide
where per.per_id=ide.per_id(+) and ide.ide_id is null and per.per_identificacion=ide.IDE_NUMERO(+);
begin
    FOR lr_persona IN  lq_personas   LOOP  
          INSERT INTO TNEG_IDENTIFICACION ( PER_ID, IDE_USUARIO_MOD, IDE_TIPO, IDE_PRINCIPAL, IDE_NUMERO,  IDE_ID) 
         VALUES ( lr_persona.per_id, lr_persona.per_usuario_mod ,lr_persona.PER_TIPO_IDENTIFICACION,'S',lr_persona.PER_IDENTIFICACION,SNEG_IDENTIFICACION.nextval);
     end loop;
end;