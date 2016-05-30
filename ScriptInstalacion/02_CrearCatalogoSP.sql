/*++++++++++++++++++++++++++++++++++++++++
    --Primero crear el sp
    Luego ejecutar el script que se genera en el archivo excel
++++++++++++++++++++++++++++++++++++
*/
create or replace procedure prc_ins_catalogo(--datos del catalogo
ec_cat_CODIGO VARCHAR2, ec_cat_NOMBRE VARCHAR2,   ec_cat_DESCRIPCION VARCHAR2,    ec_cat_EDITABLE VARCHAR2,    ec_cat_ITEM_PADRE VARCHAR2, ec_cat_cat_PADRE VARCHAR2,
--datos del item
ec_ite_CODIGO VARCHAR2,    ec_ite_NOMBRE VARCHAR2,    ec_ite_DESCRIPCION VARCHAR2 )Is 
 --obtener los datos del excel de catalogos para que sean insertados
 ln_cant number;
 ln_cat_id number;
 ln_ite_id_padre number;
begin
    select count(*) into ln_cant from tadm_catalogo where cat_codigo=ec_cat_CODIGO;
    if(ln_cant=0) then        
        if ec_cat_ITEM_PADRE<> '' or ec_cat_ITEM_PADRE is not null then
            select ite.ite_id into ln_ite_id_padre from tadm_catalogo cat, tadm_item_catalogo ite where ite.cat_id=cat.cat_id and ite_codigo=ec_cat_ITEM_PADRE and  cat_codigo=ec_cat_cat_PADRE;
        end if;
        select sADM_CATALOGO.nextval into ln_cat_id from dual;
        INSERT INTO TADM_CATALOGO (
           ITE_ID, CAT_NOMBRE, CAT_ID, 
           CAT_EDITABLE, CAT_DESCRIPCION, 
           CAT_CODIGO) 
        VALUES ( ln_ite_id_padre,
         ec_CAT_NOMBRE,
         ln_cat_id,        
         ec_CAT_EDITABLE,
         ec_CAT_DESCRIPCION,
         ec_cat_CODIGO);
         dbms_output.put_line('insertar catalogo '||ec_cat_CODIGO||ec_cat_NOMBRE);
    else
         select cat_id into ln_cat_id from TADM_CATALOGO where cat_codigo=ec_cat_CODIGO;
        dbms_output.put_line('actualizar catalogo'||ec_cat_CODIGO||ec_cat_NOMBRE);
    end if;
    select count(*) into ln_cant from tadm_catalogo cat, tadm_item_catalogo ite where ite.cat_id=cat.cat_id and ite_codigo=ec_ite_CODIGO and  cat_codigo=ec_cat_CODIGO;
     if(ln_cant=0) then        
        INSERT INTO TADM_ITEM_CATALOGO (   ITE_NOMBRE, ITE_ID,     ITE_DESCRIPCION, ITE_CODIGO, CAT_ID) 
            VALUES ( ec_ite_NOMBRE, sADM_ITEM_CATALOGO.nextval, ec_ite_DESCRIPCION,ec_ite_CODIGO , ln_cat_id);
            dbms_output.put_line('insertar item'||ec_ite_CODIGO||ec_ite_NOMBRE);
    else
        dbms_output.put_line('actualizar item'||ec_ite_CODIGO||ec_ite_NOMBRE);
    end if;    
end;
