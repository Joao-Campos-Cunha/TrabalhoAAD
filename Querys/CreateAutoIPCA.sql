-- Criação da Database

CREATE DATABASE autoIPCA;

-- Utiliza a Database criada "autoIPCA"
USE autoIPCA;

-- Criação da tabela "CP"

CREATE TABLE CP(
	CPid INTEGER PRIMARY KEY,
	Localidade VARCHAR(255) NOT NULL
);

-- Criação da tabela "TipoMulta"

CREATE TABLE TipoMulta(
	TMultaID INTEGER PRIMARY KEY,
	Tipo VARCHAR(255) NOT NULL
);

-- Criação da tabela "Cliente"

CREATE TABLE Cliente(
	ClienteID INTEGER PRIMARY KEY,
	Nome VARCHAR(255) NOT NULL,
    Email VARCHAR(255),
    DataNasc DATE,
    NumeroDeAlugures INTEGER,
    CPid INTEGER,
    FOREIGN KEY (CPid) REFERENCES CP(CPid)
);

-- Criação da tabela "Multa"

CREATE TABLE Multa(
	MultaID INTEGER PRIMARY KEY,
	Valor INTEGER NOT NULL,
	StatusPag VARCHAR(255) NOT NULL,
	DataMulta DATE,
	ClienteID INTEGER,
	CPid INTEGER,
	TMultaID INTEGER,
	FOREIGN KEY (ClienteID) REFERENCES Cliente(ClienteID),
	FOREIGN KEY (CPid) REFERENCES CP(CPid),
	FOREIGN KEY (TMultaID) REFERENCES TipoMulta(TMultaID)
);

-- Criação da tabela "Vendedor"

CREATE TABLE Vendedor (
    VendedorID INTEGER PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Email VARCHAR(255),
    NumeroDeVendas INTEGER NOT NULL,
    DataNasc DATE
);

-- Criação da tabela "ClienteVendedor"

CREATE TABLE ClienteVendedor (
    VendedorID INTEGER,
    ClienteID INTEGER,
    PRIMARY KEY (VendedorID, ClienteID),
    FOREIGN KEY (VendedorID) REFERENCES Vendedor(VendedorID),
    FOREIGN KEY (ClienteID) REFERENCES Cliente(ClienteID)
);

-- Criação da tabela "Marca"

CREATE TABLE Marca (
    MarcaID INTEGER PRIMARY KEY,
	Nome VARCHAR(255) NOT NULL
 );

-- Criação da tabela "TipoDeVeiculo"

CREATE TABLE TipoDeVeiculo (
    TipoVeilID INTEGER PRIMARY KEY,
    Tipo VARCHAR(255),
    MarcaID INTEGER,
    FOREIGN KEY (MarcaID) REFERENCES Marca(MarcaID)
);

-- Criação da tabela "Veiculo"

 CREATE TABLE Veiculo (
    VeicID INTEGER PRIMARY KEY,
    Matricula VARCHAR(255) NOT NULL,
    Lugares INTEGER,
    Preco INTEGER,
    Ano INTEGER,
    Cor VARCHAR(255),
    MarcaID INTEGER,
    TipoVeilID INTEGER,
    FOREIGN KEY (MarcaID) REFERENCES Marca(MarcaID),
    FOREIGN KEY (TipoVeilID) REFERENCES TipoDeVeiculo(TipoVeilID)
);

-- Criação da tabela "Pagamento"

CREATE TABLE Pagamento (
    PagID INTEGER PRIMARY KEY,
    Valor INTEGER NOT NULL
);

-- Criação da tabela "Aluguer"

CREATE TABLE Aluguer (
    AluguerID INTEGER PRIMARY KEY,
    Custo INTEGER,
    Comissao INTEGER,
    Data DATE,
    ClientID INTEGER,
    VendedorID INTEGER,
    PagID INTEGER,
    VeicID INTEGER,
    TipoVeilID INTEGER,
    CPid INTEGER,
    FOREIGN KEY (ClientID) REFERENCES Cliente(ClienteID),
    FOREIGN KEY (VendedorID) REFERENCES Vendedor(VendedorID),
    FOREIGN KEY (PagID) REFERENCES Pagamento(PagID),
    FOREIGN KEY (VeicID) REFERENCES Veiculo(VeicID),
    FOREIGN KEY (TipoVeilID) REFERENCES TipoDeVeiculo(TipoVeilID),
    FOREIGN KEY (CPid) REFERENCES CP(CPid)
);

-- Criação da tabela "Fatura"

CREATE TABLE Fatura (
    FATID INTEGER PRIMARY KEY,
    Nif INTEGER,
    PagID INTEGER,
    FOREIGN KEY (PagID) REFERENCES Pagamento(PagID)
);

-- Criação da tabela "TiposDeContacto"

CREATE TABLE TiposDeContacto (
    TPID INTEGER PRIMARY KEY,
    Tipo VARCHAR(255)
);

-- Criação da tabela "Contacto"

CREATE TABLE Contacto (
    CID INTEGER PRIMARY KEY,
    Email VARCHAR(255),
    Telefone INTEGER,
    TPID INTEGER,
    ClientID INTEGER,
    VendedorID INTEGER,
    FOREIGN KEY (TPID) REFERENCES TiposDeContacto(TPID),
    FOREIGN KEY (ClientID) REFERENCES Cliente(ClienteID),
    FOREIGN KEY (VendedorID) REFERENCES Vendedor(VendedorID)
);

-- Criação da tabela "TipoMan"

CREATE TABLE TipoMan (
    TManID INTEGER PRIMARY KEY,
    Tipo VARCHAR(255)
);

-- Criação da tabela "Manutencao"

CREATE TABLE Manutencao (
    ManID INTEGER PRIMARY KEY,
    DataMan DATE,
    Descricao VARCHAR(255),
    Custo INTEGER,
    VeicID INTEGER,
    MarcaID INTEGER,
    TipoVeilID INTEGER,
    TManID INTEGER,
    FOREIGN KEY (VeicID) REFERENCES Veiculo(VeicID),
    FOREIGN KEY (MarcaID) REFERENCES Marca(MarcaID),
    FOREIGN KEY (TipoVeilID) REFERENCES TipoDeVeiculo(TipoVeilID),
    FOREIGN KEY (TManID) REFERENCES TipoMan(TManID)
);

CREATE TABLE Utilizador (
    UserID INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARBINARY(MAX) NOT NULL
);
