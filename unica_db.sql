create database unica_db
go 

use unica_db
go


create table pessoas
(
	codigo				int				not null	identity,
	nome				varchar(50)		not null,
    telefone			varchar(20)		not null,
    email			    varchar(50)		not null,
	status		        int,
    check(status in (1,2)),
	primary key(codigo)
)
go

create table cidades
(
	codigo				int				not null,
	nome				varchar(100)    not null,
	sigla_uf			char(02)		not null,
	primary key (codigo)
)
go

create table ceps
(
	numero				varchar(20)		not null,
	cidade_codigo		int				not null,
	primary key(numero),
	foreign key(cidade_codigo) references cidades
)
go

create table enderecos
(
	pessoa_codigo		int				not null,
	id					int				not null,
	cep_numero			varchar(20)		not null,
	logradouro			varchar(200)    not null,
	numero				varchar(20)		not null,
	bairro				varchar(100)	not null,
	primary key (pessoa_codigo, id),
	foreign key (pessoa_codigo) references pessoas,
	foreign key (cep_numero)    references ceps
)
go

create table clientes 
(
	pessoa_codigo	    int			    not null,
    cnpj                varchar(20)     not null,
    razao_social        varchar(150)    not null,
	constraint pk_cliente		primary key (pessoa_codigo),
	constraint fk_pessoa		foreign key (pessoa_codigo)		   references pessoas
)
go

create table cargos
(
    codigo              int             not null primary key identity,
    descricao           varchar(20)     not null
)
go

create table funcionarios 
(
	pessoa_codigo	    int			    not null,
    cpf                 varchar(14)     not null unique, 
    salario             money           not null,
    cargo_codigo        int             not null,
    usuario             varchar(20)     not null,
    senha               varchar(50)     not null,                         
	constraint pk_funcionario	primary key (pessoa_codigo),
	constraint fk_pessoa		foreign key (pessoa_codigo)		   references pessoas,
    constraint fk_cargo 		foreign key (cargo_codigo)		   references cargos
)
go

create table categorias
(
    codigo              int             not null        primary key identity,
    descricao           varchar(50)     not null
)
go

create table tipos
(
    codigo              int             not null        primary key identity,
    descricao           varchar(50)     not null
)
go



create table veiculos
(
    placa               varchar(7)      not null,
    descricao           varchar(50)     not null,
    valor_diaria        money           not null,
    lugares             int             not null,
    carga               int                 null,
    categoria_codigo    int             not null,
    tipo_codigo         int             not null,
    status		        int,
    check(status in (1,2)),
    constraint pk_veiculo       primary key (placa),
    constraint fk_categoria     foreign key (categoria_codigo)      references categorias,
    constraint fk_tipo          foreign key (tipo_codigo)           references tipos                 
)
go



create table contratos
(
	id 					int 			not null,
	valor 				money 			not null,
	data_inicial		datetime		not null,
	data_final 			datetime 			null,
	status		        int,
    check(status in (1,2,3,4)),
	contraint pk_contratos		primary key (id)
)

create table contratos_lp
(
	contrato_id 		int 			not null,
	mensalidade			money 			not null,
	desconto 			decimal(10,2)	not null,
	tempo_contrato		int 			not null,
	constraint pk_conttratos_lp	primary key (contrato_id),
	constraint fk_contrato_id 	foreign key (contrato_id)			references contratos
)

create table reservas
(
	contrato_id			int 			not null,
	veiculo_placa		varchar 		not null
	data_devolucao 		datetime 		not null,
	constraint pk_reservas		primary key (contrato_id, veiculo_placa),
	constraint fk_contrato_id	foreign key (contrato_id)			references contratos,
	constraint fk_veiculo_placa foreign key (veiculo_placa)			references veiculos
)




