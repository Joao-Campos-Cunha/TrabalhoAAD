-- 1 Quais os vendedores sem aluguéis atribuidos?

SELECT Vendedor.Nome, COUNT(AluguerID) AS NumAlugueis
FROM Vendedor LEFT JOIN Aluguer on Vendedor.VendedorID=Aluguer.VendedorID
GROUP BY Vendedor.Nome, Aluguer.AluguerID
HAVING COUNT(Aluguer.AluguerID) = 0;


-- 2 Qual o nome, morada e idade do cliente que menos propostas submeteu

SELECT Cliente.Nome, CP.Localidade, DATEDIFF(YEAR, Cliente.DataNasc, GETDATE()) AS Idade
FROM Cliente JOIN CP ON Cliente.CPid=CP.CPid
LEFT JOIN Aluguer ON Aluguer.ClientID=Cliente.ClienteID
GROUP BY Cliente.Nome, CP.Localidade, Cliente.DataNasc
HAVING COUNT(Aluguer.AluguerID) = 
(SELECT MIN(contagem)
FROM (SELECT COUNT(Aluguer.AluguerID) AS contagem
FROM Aluguer LEFT JOIN Cliente ON Aluguer.ClientID=Cliente.ClienteID
GROUP BY Cliente.ClienteID
) as subconsulta
);

-- 3 Quantos carros foram alugados no ano passado?

SELECT COUNT(DISTINCT Veiculo.VeicID) as alugueis 
FROM Veiculo JOIN Aluguer ON Aluguer.VeicID=Veiculo.VeicID
WHERE YEAR(Aluguer.Data) = YEAR(GETDATE())-1;

-- 4 Quais os clientes com mais do que 1 aluguer

SELECT Cliente.ClienteID, Cliente.Nome
FROM Cliente
JOIN Aluguer ON Cliente.ClienteID = Aluguer.ClientID
GROUP BY Cliente.ClienteID, Cliente.Nome
HAVING COUNT(Aluguer.AluguerID) > 1;

-- 5 Qual o vendedor que  apresentou mais propostas?

SELECT Vendedor.VendedorID, Vendedor.Nome
FROM Vendedor
JOIN Aluguer ON Vendedor.VendedorID=Aluguer.VendedorID
GROUP BY Vendedor.VendedorID, Vendedor.Nome
HAVING COUNT(Aluguer.AluguerID) = (SELECT MAX(contagem) 
FROM (SELECT COUNT(Aluguer.AluguerID) AS contagem 
FROM Aluguer GROUP BY Aluguer.VendedorID) AS subconsulta
);

-- 6 Qual a média do aluguer de um carro?

SELECT AVG(Aluguer.Custo) AS MediaCusto
FROM Aluguer;

-- 7 Qual é o carro mais caro?

SELECT DISTINCT Veiculo.VeicID, Veiculo.Matricula
FROM Veiculo
JOIN Aluguer ON Aluguer.VeicID=Veiculo.VeicID
WHERE Aluguer.Custo = (SELECT MAX(Aluguer.Custo)
FROM Aluguer);

-- 8 Qual é a marca de carro que foi mais alugada

SELECT Marca.MarcaID, Marca.Nome
FROM Marca
JOIN Veiculo ON Marca.MarcaID=Veiculo.VeicID
JOIN Aluguer ON Aluguer.VeicID=Veiculo.VeicID
GROUP BY Marca.MarcaID, Marca.Nome
HAVING COUNT(Aluguer.AluguerID) = (SELECT MAX(contagem)
FROM (SELECT COUNT(Aluguer.AluguerID) AS contagem 
FROM Aluguer 
JOIN Veiculo ON Aluguer.VeicID = Veiculo.VeicID 
GROUP BY Veiculo.MarcaID) 
AS subconsulta);

-- 9 Qual o nome do vendedor que recebeu maior comissão no aluguer de um carro

SELECT DISTINCT Vendedor.VendedorID, Vendedor.Nome
FROM Vendedor
JOIN Aluguer ON Aluguer.VendedorID=Vendedor.VendedorID
WHERE Aluguer.Comissao=(SELECT MAX(Aluguer.Comissao) FROM Aluguer);

-- 10 Qual a localidade com maior numero de clientes?

SELECT CP.CPid, CP.Localidade 
FROM CP
JOIN Cliente ON Cliente.CPid=CP.CPid
GROUP BY CP.CPid, CP.Localidade 
HAVING COUNT(Cliente.CPid)=(SELECT MAX(contagem) 
FROM (SELECT COUNT(Cliente.CPid) AS contagem 
FROM Cliente 
GROUP BY Cliente.CPid) 
AS subconsulta);

SELECT * FROM vw_multas_cliente

SELECT * FROM vw_resumo_alugueres

EXEC InserirCliente @ClienteID = 3, @Nome = 'João Cunha', @Email = 'joaocunhagmail.com', @DataNasc = '12-12-2012', @CPid = 3

EXEC ProcurarAlugueis @ClienteID = 1;

SET STATISTICS TIME ON; -- Mostra o tempo da query
SET STATISTICS IO ON;   -- Mostra o uso de índices

EXEC sp_helpindex 'Cliente';

SELECT * FROM Cliente WHERE CPid = 1;


