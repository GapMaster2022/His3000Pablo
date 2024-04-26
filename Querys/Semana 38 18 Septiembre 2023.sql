--PRODECIMIENTOS PARA CREAR PERFIL DE USUARIO SIC
CREATE PROCEDURE 
[dbo].[sp_ActualizarUsuarioGrupo]


    @CODUSU BIGINT,
    @CODGRU BIGINT
AS
BEGIN
    -- Primera operación: Actualizar SeguridadUsuarioOpciones
    UPDATE sic3000..SeguridadUsuarioOpciones
    SET staopc = 'N'
    WHERE codusu = @CODUSU;

    -- Segunda operación: Actualizar SeguridadUsuarioOpciones basado en SeguridadGrupoOpciones
    IF EXISTS (
        SELECT 1
        FROM sic3000..SeguridadGrupoOpciones
        WHERE codgru = @CODGRU AND staopc = 'S'
    )
    BEGIN
        UPDATE sic3000..SeguridadUsuarioOpciones
        SET staopc = 'S'
        WHERE codusu = @CODUSU
        AND codopc IN (
            SELECT codopc
            FROM sic3000..SeguridadGrupoOpciones
            WHERE codgru = @CODGRU AND staopc = 'S'
        );
    END;
END;
GO
---------------------------------------------------------------------
--PRODECIMIENTOS PARA CREAR PERFIL DE USUARIO CG

CREATE PROCEDURE 
[dbo].[sp_ActualizarUsuarioGrupoCg]


    @CODUSU BIGINT,
    @CODGRU BIGINT
AS
BEGIN
    -- Primera operación: Actualizar SeguridadUsuarioOpciones
    UPDATE cg3000..Cgopciusu
    SET staopc = 'N'
    WHERE codusu = @CODUSU;

    -- Segunda operación: Actualizar SeguridadUsuarioOpciones basado en SeguridadGrupoOpciones
    IF EXISTS (
        SELECT 1
        FROM Cg3000..Cggruopc
        WHERE codgru = @CODGRU AND staopc = 'S'
    )
    BEGIN
        UPDATE cg3000..Cgopciusu
        SET staopc = 'S'
        WHERE codusu = @CODUSU
        AND codopc IN (
            SELECT codopc
            FROM Cg3000..Cggruopc
            WHERE codgru = @CODGRU AND staopc = 'S'
        );
    END;
END;
GO

-----------------------------------------------------------
---CREAR EN EL CG
CREATE TABLE [dbo].[Cgusugrup](
	[codgu] [bigint] IDENTITY(1,1) NOT NULL,
	[codusu] [bigint] NOT NULL,
	[codgru] [bigint] NOT NULL,
	[staopc] [bit] NULL
) ON [PRIMARY]
GO

--------------------------------------------------
--CREAR EL SIC

CREATE TABLE dbo.SeguridadesUsuarioGrupo
	(
	codsg  BIGINT IDENTITY NOT NULL,
	codusu BIGINT NOT NULL,
	codgru BIGINT NOT NULL,
	staopc BIT NULL
	)
GO


--------------------his
alter table HC_EXONERACION_RETIRO add CON_CODIGO INT