-- Atualizar número de alugueres
CREATE TRIGGER AtualizarNumeroAlugueres
ON Aluguer
AFTER INSERT
AS
BEGIN
    UPDATE Cliente
    SET NumeroDeAlugures = NumeroDeAlugures + 1
    WHERE ClienteID = (SELECT ClientID FROM inserted);
END;