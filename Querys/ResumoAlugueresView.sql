-- Resumo de alugueres
CREATE VIEW vw_resumo_alugueres AS
SELECT 
    Cliente.Nome AS Cliente,
    Veiculo.Matricula AS Veiculo,
    Aluguer.Custo AS Custo,
    Aluguer.Data AS DataAluguer
FROM Aluguer
JOIN Cliente ON Aluguer.ClientID = Cliente.ClienteID
JOIN Veiculo ON Aluguer.VeicID = Veiculo.VeicID;