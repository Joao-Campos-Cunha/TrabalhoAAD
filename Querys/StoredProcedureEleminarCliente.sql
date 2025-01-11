CREATE PROCEDURE EliminarCliente
    @ClienteID INT
AS
BEGIN
    -- Iniciar uma transação para garantir consistência
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar alugueres associados ao cliente
        DELETE FROM Aluguer WHERE ClientID = @ClienteID;

        -- Eliminar multas associadas ao cliente
        DELETE FROM Multa WHERE ClienteID = @ClienteID;

        -- Eliminar contactos associados ao cliente
        DELETE FROM Contacto WHERE ClientID = @ClienteID;

        -- Eliminar relações com vendedores
        DELETE FROM ClienteVendedor WHERE ClienteID = @ClienteID;

        -- Eliminar o cliente
        DELETE FROM Cliente WHERE ClienteID = @ClienteID;

        -- Confirmar a transação
        COMMIT TRANSACTION;

        -- Mensagem de sucesso
        PRINT 'Cliente eliminado com sucesso.';
    END TRY

    BEGIN CATCH
        -- Reverter a transação em caso de erro
        ROLLBACK TRANSACTION;

        -- Mostrar mensagem de erro
        PRINT 'Erro ao eliminar o cliente.';
    END CATCH;
END;