INSERT INTO CP (CPid, Localidade) VALUES
(1, 'Braga'),
(2, 'Guimarães');

INSERT INTO TipoMulta (TMultaID, Tipo) VALUES
(1, 'Estacionamento irregular'),
(2, 'Excesso de velocidade');

INSERT INTO Cliente (ClienteID, Nome, Email, DataNasc, NumeroDeAlugures, CPid) VALUES
(1, 'João Silva', 'joao.silva@email.com', '2000-05-15', 0, 1),
(2, 'Ana Santos', 'ana.santos@email.com', '1999-09-25', 0, 2);

INSERT INTO Vendedor (VendedorID, Nome, Email, NumeroDeVendas, DataNasc) VALUES
(1, 'Carlos Pereira', 'carlos.pereira@email.com', 0, '1985-03-10'),
(2, 'Maria Oliveira', 'maria.oliveira@email.com', 0, '1990-07-18');

INSERT INTO ClienteVendedor (VendedorID, ClienteID) VALUES
(1, 1),
(2, 2);

INSERT INTO Marca (MarcaID, Nome) VALUES
(1, 'Toyota'),
(2, 'Ford');

INSERT INTO TipoDeVeiculo (TipoVeilID, Tipo, MarcaID) VALUES
(1, 'Compacto', 1),
(2, 'SUV', 2);

INSERT INTO Veiculo (VeicID, Matricula, Lugares, Preco, Ano, Cor, MarcaID, TipoVeilID) VALUES
(1, 'AA-12-BC', 5, 20000, 2020, 'Vermelho', 1, 1),
(2, 'ZZ-99-XY', 7, 35000, 2021, 'Preto', 2, 2);

INSERT INTO Pagamento (PagID, Valor) VALUES
(1, 500),
(2, 700);

INSERT INTO Aluguer (AluguerID, Custo, Comissao, Data, ClientID, VendedorID, PagID, VeicID, TipoVeilID, CPid) VALUES
(1, 500, 50, '2025-01-01', 1, 1, 1, 1, 1, 1),
(2, 700, 70, '2025-01-05', 2, 2, 2, 2, 2, 2);

INSERT INTO Fatura (FATID, Nif, PagID) VALUES
(1, 123456789, 1),
(2, 987654321, 2);

INSERT INTO TiposDeContacto (TPID, Tipo) VALUES
(1, 'Telefone'),
(2, 'Email');

INSERT INTO Contacto (CID, Email, Telefone, TPID, ClientID, VendedorID) VALUES
(1, 'joao.silva@email.com', 912345678, 2, 1, NULL),
(2, 'maria.oliveira@email.com', NULL, 2, NULL, 2);

INSERT INTO TipoMan (TManID, Tipo) VALUES
(1, 'Troca de óleo'),
(2, 'Alinhamento de rodas');

INSERT INTO Manutencao (ManID, DataMan, Descricao, Custo, VeicID, MarcaID, TipoVeilID, TManID) VALUES
(1, '2025-01-02', 'Troca de óleo completa', 100, 1, 1, 1, 1),
(2, '2025-01-06', 'Alinhamento das rodas', 50, 2, 2, 2, 2);

INSERT INTO Multa (MultaID, Valor, StatusPag, DataMulta, ClienteID, CPid, TMultaID) VALUES
(1, 100, 'Pago', '2025-01-03', 1, 1, 1),
(2, 200, 'Por pagar', '2025-01-04', 2, 2, 2);

INSERT INTO Utilizador (Username, PasswordHash)
VALUES ('admin', CONVERT(NVARCHAR(MAX), HASHBYTES('SHA2_256', 'admin123'), 1)),
       ('user1', CONVERT(NVARCHAR(MAX), HASHBYTES('SHA2_256', 'user123'), 1));
	   ('agent1', CONVERT(NVARCHAR(MAX), HASHBYTES('SHA2_256', 'agent123'), 1));

INSERT INTO Utilizador (Username, PasswordHash)
VALUES ('agent1', CONVERT(NVARCHAR(MAX), HASHBYTES('SHA2_256', 'agent123'), 1));

	SELECT *
	FROM Utilizador