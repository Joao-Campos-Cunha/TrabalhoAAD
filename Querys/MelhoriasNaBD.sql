-- Índices para melhorar desempenho em consultas
CREATE INDEX idx_cliente_cpid ON Cliente(CPid);
CREATE INDEX idx_aluguer_clientid ON Aluguer(ClientID);
CREATE INDEX idx_veiculo_marcaid ON Veiculo(MarcaID);
CREATE INDEX idx_multa_clienteid ON Multa(ClienteID);

-- Datas coerentes
ALTER TABLE Cliente ADD CONSTRAINT chk_data_nasc CHECK (DataNasc < GETDATE());
ALTER TABLE Aluguer ADD CONSTRAINT chk_data_aluguer CHECK (Data < GETDATE());

-- Valores positivos
ALTER TABLE Veiculo ADD CONSTRAINT chk_preco_positivo CHECK (Preco > 0);
ALTER TABLE Aluguer ADD CONSTRAINT chk_custo_positivo CHECK (Custo >= 0);
ALTER TABLE Manutencao ADD CONSTRAINT chk_custo_manutencao CHECK (Custo >= 0);

-- Validação de emails
ALTER TABLE Cliente ADD CONSTRAINT chk_email_cliente CHECK (Email LIKE '%@%.%');
ALTER TABLE Vendedor ADD CONSTRAINT chk_email_vendedor CHECK (Email LIKE '%@%.%');


