--------------------------------------------------------------20/11/2023---------------------------------------------------------------------
use His3000
go 
CREATE TABLE ATENCIONES_REINGRESO(
ID BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
ATE_CODIGO_PRINCIPAL BIGINT NOT NULL,
ATE_CODIGO_REING BIGINT NOT NULL,
ID_USUARIO	BIGINT NOT NULL,
FECHA_CREACION	DATETIME,
ESTADO BIT NOT NULL
)
--------------------------------------------------------------22/11/2023---------------------------------------------------------------------
use SERIES3000_AUDITORIA
go
create table ATENCIONES_REINGRESO_AUD(
ID_PAC_AUD INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
ID BIGINT NOT NULL,
ATE_CODIGO_PRINCIPAL BIGINT NOT NULL,
ATE_CODIGO_REING BIGINT NOT NULL,
ID_USUARIO	BIGINT NOT NULL,
FECHA_CREACION	DATETIME,
ESTADO BIT NOT NULL
)
GO
--------------------------------------------------------------22/11/2023---------------------------------------------------------------------
use His3000
go 
CREATE TRIGGER TR_ATENCIONES_REINGRESO_UPDATE
ON dbo.ATENCIONES_REINGRESO
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO SERIES3000_AUDITORIA..ATENCIONES_REINGRESO_AUD(
        ID,
		ATE_CODIGO_PRINCIPAL,
		ATE_CODIGO_REING,
		ID_USUARIO,
		FECHA_CREACION,
		ESTADO
    )
    SELECT
        i.ID,
		i.ATE_CODIGO_PRINCIPAL,
        i.ATE_CODIGO_REING,        
        i.ID_USUARIO,
        GETDATE(),
        i.ESTADO
    FROM
        inserted i
    INNER JOIN
        deleted d ON i.ID = d.ID
    WHERE
        i.ATE_CODIGO_PRINCIPAL <> d.ATE_CODIGO_PRINCIPAL
        OR i.ATE_CODIGO_REING <> d.ATE_CODIGO_REING
		OR i.ID_USUARIO <> d.ID_USUARIO
        OR i.FECHA_CREACION <> d.FECHA_CREACION
        OR i.ESTADO <> d.ESTADO
        -- Aquí debes incluir las demás columnas que quieres monitorear para cambios.
END;