-- Inserir cliente
CREATE PROCEDURE InserirCliente
    @ClienteID INT,
    @Nome NVARCHAR(255),
    @Email NVARCHAR(255),
    @DataNasc DATE,
    @CPid INT
AS
BEGIN
    INSERT INTO Cliente (ClienteID, Nome, Email, DataNasc, NumeroDeAlugures, CPid)
    VALUES (@ClienteID, @Nome, @Email, @DataNasc, 0, @CPid);
END;