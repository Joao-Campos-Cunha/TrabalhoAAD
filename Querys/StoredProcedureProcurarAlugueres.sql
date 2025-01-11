-- Procurar aluguéis de um cliente
CREATE PROCEDURE ProcurarAlugueres
    @ClienteID INT
AS
BEGIN
    SELECT * FROM Aluguer WHERE ClientID = @ClienteID;
END;