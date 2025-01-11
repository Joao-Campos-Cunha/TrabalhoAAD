-- Relatório de multas
CREATE VIEW vw_multas_cliente AS
SELECT 
    Cliente.Nome AS Cliente,
    Multa.Valor AS Multa,
    Multa.StatusPag AS EstadoPagamento,
    Multa.DataMulta AS DataMulta,
    TipoMulta.Tipo AS TipoMulta
FROM Multa
JOIN Cliente ON Multa.ClienteID = Cliente.ClienteID
JOIN TipoMulta ON Multa.TMultaID = TipoMulta.TMultaID;


