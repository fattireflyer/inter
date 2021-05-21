create database unica_db
go 

use unica_db
go

create table pessoas
(
	codigo				int						not null	identity primary key,
	nome					varchar(50)		not null,
  telefone			varchar(20)		not null,
  email					varchar(50)		not null,
	logradouro		varchar(max)	not null,
	numero				varchar(5)		not null,
	complemento		varchar(max)			null,
	bairro				varchar(max)	not null,
	cidade				varchar(max)	not null,
	estado	  		varchar(2)		not null,
	cep 					varchar(8)		not null,
	status		    int,
  check(status in (1,2))
)
go

create table clientes 
(
	pessoa_codigo	 int		 		    not null,
  cnpj           varchar(20)    not null,
  razao_social   varchar(150)   not null,
	constraint pk_cliente		primary key (pessoa_codigo),
	constraint fk_pessoa		foreign key (pessoa_codigo)		   references pessoas
)
go

create table funcionarios 
(
	pessoa_codigo	    	int			    		not null,
  cpf                 varchar(14)     not null unique, 
  salario             money           not null,
  cargo        				varchar(max)    not null,
  usuario             varchar(20)     not null,
  senha               varchar(50)     not null,                         
	constraint pk_funcionario	primary key (pessoa_codigo),
	constraint fk_pessoa		foreign key (pessoa_codigo)		   references pessoas
)
go

create procedure cadFunc
(
	@nome	varchar(50), 
	@telefone	varchar(20), 
	@email	varchar(50),
	@logradouro	varchar(max), 
	@numero	varchar(5), 
	@complemento varchar(max),
	@bairro	varchar(max), 
	@cidade	varchar(max), 
	@estado	varchar(2),
	@cep varchar(8), 
	@status int, 
	@cpf varchar(14), 
	@cargo varchar(max),
	@salario money, 
	@usuario varchar(20), 
	@senha varchar(50) 		
)
as
begin
	insert into pessoas  
	values (@nome, @telefone, @email, @logradouro, @numero, @complemento, @bairro,
					@cidade, @estado, @cep, @status)
	insert into funcionarios values (@@IDENTITY, @cpf, @cargo, @usuario, @senha)
end
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
	veiculo_placa		varchar 		not null,
	data_devolucao 		datetime 		not null,
	constraint pk_reservas		primary key (contrato_id, veiculo_placa),
	constraint fk_contrato_id	foreign key (contrato_id)			references contratos,
	constraint fk_veiculo_placa foreign key (veiculo_placa)			references veiculos
)



/*

PROCEDURES

 */

-- Cadastrar Cliente --

go
create procedure cadCli
(
	@nome					varchar(50), 
	@telefone			varchar(20), 
	@email				varchar(50),
	@logradouro		varchar(max), 
	@numero				varchar(5), 
	@complemento 	varchar(max),
	@bairro				varchar(max), 
	@cidade				varchar(max), 
	@estado				varchar(2),
	@cep 					varchar(8), 
	@status 			int, 
	@cnpj 				varchar(20), 
	@razao_social varchar(150)		
)
as
begin
	insert into pessoas  
	values (@nome, @telefone, @email, @logradouro, @numero, @complemento, @bairro,
					@cidade, @estado, @cep, @status)
	insert into clientes values (@@IDENTITY, @cnpj, @razao_social)
end
go
-- alterar cliente --
go
create procedure altCli
(
	@codigo				int,
	@nome					varchar(50), 
	@telefone			varchar(20), 
	@email				varchar(50),
	@logradouro		varchar(max), 
	@numero				varchar(5), 
	@complemento 	varchar(max),
	@bairro				varchar(max), 
	@cidade				varchar(max), 
	@estado				varchar(2),
	@cep 					varchar(8), 
	@status 			int, 
	@cnpj 				varchar(20), 
	@razao_social varchar(150)	
)
as
begin
	update pessoas set nome = @nome, telefone = @telefone, email = @email, logradouro = @logradouro,
	 numero = @numero, complemento = @complemento, bairro = @bairro, cidade = @cidade, estado = @estado,
	 cep = @cep, status = @status
	where codigo = @codigo

	update clientes set cnpj = @cnpj, razao_social = @razao_social
	where pessoa_codigo = @codigo
end
go

-- desativar pessoa --
create procedure deactivatePes
(
	@codigo int,
	@status int 
)
as
begin
	update pessoas set status = 2
	where codigo = @codigo
end
go

-- cadastrar funcionario --

go
create procedure cadFunc
(
	@nome					varchar(50), 
	@telefone			varchar(20), 
	@email				varchar(50),
	@logradouro		varchar(max), 
	@numero				varchar(5), 
	@complemento 	varchar(max),
	@bairro				varchar(max), 
	@cidade				varchar(max), 
	@estado				varchar(2),
	@cep 					varchar(8), 
	@status 			int, 
	@cpf 					varchar(14), 
	@cargo			  varchar(max),
	@usuario      varchar(20),
  @senha         varchar(50)       

)
as
begin
	insert into pessoas  
	values (@nome, @telefone, @email, @logradouro, @numero, @complemento, @bairro,
					@cidade, @estado, @cep, @status)
	insert into funcionarios values (@@IDENTITY, @cpf, @cargo, @usuario, @senha)
end
go


-- alterar funcionario
go
create procedure altFunc
(
	@codigo       int,
	@nome					varchar(50), 
	@telefone			varchar(20), 
	@email				varchar(50),
	@logradouro		varchar(max), 
	@numero				varchar(5), 
	@complemento 	varchar(max),
	@bairro				varchar(max), 
	@cidade				varchar(max), 
	@estado				varchar(2),
	@cep 					varchar(8), 
	@status 			int, 
	@cpf 					varchar(14), 
	@cargo			  varchar(max),
	@usuario      varchar(20),
  @senha         varchar(50)  
	
)
as
begin
	update pessoas set nome = @nome, telefone = @telefone, email = @email, logradouro = @logradouro,
	 numero = @numero, complemento = @complemento, bairro = @bairro, cidade = @cidade, estado = @estado,
	 cep = @cep, status = @status
	where codigo = @codigo

	update clientes set cpf = @cpf, cargo = @cargo, usuario = @usuario, senha = @senha
	where pessoa_codigo = @codigo
end
go





/*

VIEWS

*/

go 
create view v_clientes as 
	select p.*, c.cnpj, c.razao_social 
		from pessoas p 
		INNER JOIN clientes c ON  c.pessoa_codigo = p.codigo
		ORDER BY p.nome
go


go create view v_funcionarios AS
	select p.*, f.cpf, f.cargo, f.usuario, f.senha 
		from pessoas p 
		INNER JOIN funcionarios f ON  f.pessoa_codigo = p.codigo
		ORDER BY p.nome
go


