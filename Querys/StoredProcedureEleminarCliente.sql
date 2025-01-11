CREATE PROCEDURE EliminarCliente
    @ClienteID INT
AS
BEGIN
    -- Iniciar uma transa��o para garantir consist�ncia
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar alugueres associados ao cliente
        DELETE FROM Aluguer WHERE ClientID = @ClienteID;

        -- Eliminar multas associadas ao cliente
        DELETE FROM Multa WHERE ClienteID = @ClienteID;

        -- Eliminar contactos associados ao cliente
        DELETE FROM Contacto WHERE ClientID = @ClienteID;

        -- Eliminar rela��es com vendedores
        DELETE FROM ClienteVendedor WHERE ClienteID = @ClienteID;

        -- Eliminar o cliente
        DELETE FROM Cliente WHERE ClienteID = @ClienteID;

        -- Confirmar a transa��o
        COMMIT TRANSACTION;

        -- Mensagem de sucesso
        PRINT 'Cliente eliminado com sucesso.';
    END TRY

    BEGIN CATCH
        -- Reverter a transa��o em caso de erro
        ROLLBACK TRANSACTION;

        -- Mostrar mensagem de erro
        PRINT 'Erro ao eliminar o cliente.';
    END CATCH;
END;