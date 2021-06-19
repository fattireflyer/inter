create database unica_db
go 

use unica_db
go

create table pessoas
(
	id				int						not null	identity primary key,
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
	pessoa_id	 int		 		    not null,
  cnpj           varchar(20)    not null,
  razao_social   varchar(150)   not null,
	constraint pk_cliente		primary key (pessoa_id),
	constraint fk_pessoa		foreign key (pessoa_id)		   references pessoas
)
go

create table funcionarios 
(
	pessoa_id	    	int			    		not null,
  cpf                 varchar(14)     not null unique, 
  salario             money           not null,
  cargo        				varchar(max)    not null,
  usuario             varchar(20)     not null,
  senha               varchar(50)     not null,                         
	constraint pk_funcionario	primary key (pessoa_id),
	constraint fk_pessoa		foreign key (pessoa_id)		   references pessoas
)
go

create table categorias
(
    id              int             not null        primary key identity,
    descricao           varchar(50)     not null
)
go

create table tipos
(
    id              int             not null        primary key identity,
    descricao           varchar(50)     not null
)
go



create table veiculos
(
    id							int 						not null identity, 
		placa               varchar(7)      not null,
    descricao           varchar(50)     not null,
    valor_diaria        money           not null,
    lugares             int             not null,
    carga               int                 null,
    categoria_id    int             not null,
    tipo_id         int             not null,
    status		        int,
    check(status in (1,2)),
    constraint pk_veiculo       primary key (id),
    constraint fk_categoria     foreign key (categoria_id)      references categorias,
    constraint fk_tipo          foreign key (tipo_id)           references tipos                 
)
go

create table contratos
(
	id 						int 					not null,
	valor_total 	money 				not null,
	data_inicial	datetime			not null,
	cliente_id		int 					not null, 
	data_final 		datetime 					null,
	status		    int,
    check(status in (1,2,3,4)),
	contraint pk_contratos		primary key (id)
)

create table contratos_lp
(
	contrato_id 		int 					not null,
	mensalidade			money 				not null,
	tempo_contrato	int 					not null,
	constraint pk_conttratos_lp	primary key (contrato_id),
	constraint fk_contrato_id 	foreign key (contrato_id)			references contratos
)

create table reservas
(
	contrato_id			int 				not null,
	veiculo_placa		varchar 		not null,
	data_saida			datetime 		not null,
	data_contratada datetime 		not null,
	data_devolucao 	datetime 	not null,
	STATUS					int,
	check (status in (0,1)),
	constraint pk_reservas		primary key (contrato_id, veiculo_placa),
	constraint fk_contrato_id	foreign key (contrato_id)			references contratos,
	constraint fk_veiculo_placa foreign key (veiculo_placa)	references veiculos
)



			 


/*	PROCEDURES */

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
	@id				int,
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
	where id = @id

	update clientes set cnpj = @cnpj, razao_social = @razao_social
	where pessoa_id = @id
end
go
-- desativar pessoa --
create procedure deactivatePes
(
	@id int,
	@status int 
)
as
begin
	update pessoas set status = 2
	where id = @id
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
	@id       int,
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
	where id = @id

	update clientes set cpf = @cpf, cargo = @cargo, usuario = @usuario, senha = @senha
	where pessoa_id = @id
end
go
-- Cadastrar Veículo
create procedure cadVei
(
	@placa               varchar(7),
  @descricao           varchar(50),
  @valor_diaria        money,
  @lugares             int,
  @carga               int,
  @categoria_id    int,
  @tipo_id         int,
  @status		        	 int
)
as
begin 
		insert into veiculos 
		values 
				(@placa, @descricao, @valor_diaria, @lugares, @carga, @categoria_id, @tipo_id, @status)
end 
go
-- Alterar Veículo
go 
create procedure altVei
(
	@id 						 int,
	@placa               varchar(7),
  @descricao           varchar(50),
  @valor_diaria        money,
  @lugares             int,
  @carga               int,
  @categoria_id    int,
  @tipo_id         int,
  @status		        	 int
)
as 
begin 
	update veiculos set placa = @placa, descricao = @descricao, valor_diaria = @valor_diaria,
	lugares = @lugares, carga = @carga, categoria_id = @categoria_id, tipo_id = @tipo_id, status = @status
	where id = @id; 
end 
go 

/* VIEWS */
go 
create view v_clientes as 
	select p.*, c.cnpj, c.razao_social 
		from pessoas p 
		INNER JOIN clientes c ON  c.pessoa_id = p.id
		ORDER BY p.nome
go

go 
create view v_funcionarios AS
	select p.*, f.cpf, f.cargo, f.usuario, f.senha 
		from pessoas p 
		INNER JOIN funcionarios f ON  f.pessoa_id = p.id
		ORDER BY p.nome
go

go 
create view v_veiculos as 
	select v.*, c.descricao categoria, t.descricao tipo
	from veiculos v 
	inner join categorias c
	ON v.categoria_id = c.id
	inner join tipos
	ON v.tipo_id = t.id
go

go create view v_reservas as 
select r.contrato_id, r.data_saida, r.data_contratada, r.data_devolucao, r.status,
		   v.*
from 	     reservas r 
inner join v_veiculos v
on				 r.veiculo_placa = v.placa
go 


