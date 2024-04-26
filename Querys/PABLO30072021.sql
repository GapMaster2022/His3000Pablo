
create procedure sp_ConvenioPago
@cat_nombre varchar(200)
as
begin
select ae.ASE_RUC from ASEGURADORAS_EMPRESAS ae 
inner join CATEGORIAS_CONVENIOS cc on ae.ASE_CODIGO=cc.ASE_CODIGO
where cc.CAT_NOMBRE=@cat_nombre
end