-- Penalização por atrasos
CREATE TRIGGER PenalizarAtrasos
ON Aluguer
AFTER UPDATE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted WHERE Data > GETDATE())
    BEGIN
        INSERT INTO Multa (MultaID, Valor, StatusPag, DataMulta, ClienteID, CPid, TMultaID)
        VALUES (DEFAULT, 50, 'Por pagar', GETDATE(), (SELECT ClientID FROM inserted), (SELECT CPid FROM inserted), 1);
    END
END;