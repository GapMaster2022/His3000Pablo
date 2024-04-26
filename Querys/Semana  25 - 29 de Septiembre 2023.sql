-----------------------------------------------------------------------------26/09/2023---------------------------------------------------------------------------------------------------------------
alter table Sic3000..SeguridadOpciones add estopc bit not null default 1 

alter table Cg3000..Cgopcion add estopc bit not null default 1 

-----------------------------------------------------------------------------29/09/2023---------------------------------------------------------------------------------------------------------------

alter table His3000..CONTROL_CONSULTA add identificacion varchar(30)

alter table His3000..CONTROL_CONSULTA add resultado varchar(250)
