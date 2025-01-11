CREATE PROCEDURE ProcurarCliente
    @ClienteID INT
AS
BEGIN
    SELECT * FROM Cliente WHERE Cliente.ClienteID=@ClienteID;
END;

