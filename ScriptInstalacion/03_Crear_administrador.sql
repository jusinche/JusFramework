--Primero tiene que estar creado el catalogos e insertados sus items
INSERT INTO TSEG_CUENTA (CUE_USUARIO_MOD,  CUE_LOGIN, CUE_ID,     CUE_ESTADO, CUE_CLAVE )
VALUES (  'admin', 'admin', sSEG_CUENTA.nextval,
  (select ite.ite_id from tadm_catalogo cat, tadm_item_catalogo ite where cat.cat_id=ite.cat_id and cat.cat_codigo='CUENTAESTADO' and ite.ite_codigo='ACTIVO' ),
 'F333A3D4C7B44669E21CD8B5EF984788'); 
 --clave= Adm1nAdm1n
 --crear rol administrador
INSERT INTO TSEG_ROL ( ROL_USUARIO_MOD, ROL_NOMBRE, ROL_ID,  ROL_ESTADO) 
VALUES (  'admin','Administrador',sSEG_ROL.nextval, (select ITE.itE_id from tadm_catalogo cat, tadm_item_catalogo ite where cat.cat_id=ite.cat_id and cat.cat_codigo='ROLESTADO' and ite.ite_codigo='ACTIVO') );
--inserte cuenta rol
INSERT INTO TSEG_CUE_ROL ( ROL_ID, CUE_ID,     CRO_USUARIO_MOD, CRO_ID) 
VALUES ( (select rol_id from tseg_rol where rol_nombre ='Administrador'), (select cue_id from tseg_cuenta where CUE_LOGIN ='admin'), 'admin', sSEG_CUE_ROL.nextval);