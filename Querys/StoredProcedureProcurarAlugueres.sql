-- Procurar alugu�is de um cliente
CREATE PROCEDURE ProcurarAlugueres
    @ClienteID INT
AS
BEGIN
    SELECT * FROM Aluguer WHERE ClientID = @ClienteID;
END;