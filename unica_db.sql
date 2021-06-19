create database unica_db


use unica_db


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


create table clientes 
(
	pessoa_id	 int		 		    not null,
  cnpj           varchar(20)    not null,
  razao_social   varchar(150)   not null,
	constraint pk_cliente		primary key (pessoa_id),
	constraint fk_pessoa_cliente		foreign key (pessoa_id)		   references pessoas
)


create table funcionarios 
(
	pessoa_id	    	int			    		not null,
  cpf                 varchar(14)     not null unique, 
  salario             money           not null,
  cargo        				varchar(max)    not null,
  usuario             varchar(20)     not null,
  senha               varchar(50)     not null,                         
	constraint pk_funcionario	primary key (pessoa_id),
	constraint fk_pessoa_funcionario		foreign key (pessoa_id)		   references pessoas
)


create table veiculos
(
    id							int 						not null identity, 
		placa               varchar(7)      not null,
    descricao           varchar(50)     not null,
    valor_diaria        money           not null,
    lugares             int             not null,
    carga               int                 null,
    categoria			varchar(50)             not null,
    tipo				varchar(50)             not null,
    status		        int,
    check(status in (1,2)),
    constraint pk_veiculo       primary key (id),          
)



alter table veiculos add marca varchar(max)


create table contratos
(
	id 						int 					not null identity,
	valor_total 	money 				not null,
	data_inicial	datetime			not null,
	cliente_id		int 					not null, 
	data_final 		datetime 					null,
	status		    int,
    check(status in (1,2,3,4)),
	constraint pk_contratos		primary key (id)
)


create table contratos_lp
(
	contrato_id 		int 					not null,
	mensalidade			money 				not null,
	tempo_contrato	int 					not null,
	constraint pk_contratos_lp	primary key (contrato_id),
	constraint fk_contrato_id_lp 	foreign key (contrato_id)			references contratos
)

create table reservas
(
	contrato_id			int 				not null,
	veiculo_id		int 		not null,
	data_saida			datetime 		not null,
	data_contratada datetime 		not null,
	data_devolucao 	datetime 	not null,
	STATUS					int,
	check (status in (0,1)),
	constraint pk_reservas		primary key (contrato_id, veiculo_id),
	constraint fk_contrato_id_reserva	foreign key (contrato_id)			references contratos,
	constraint fk_veiculo_id_reserva foreign key (veiculo_id)	references veiculos
)



			 


/*	PROCEDURES */

-- Cadastrar Cliente --


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

-- alterar cliente --

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

-- cadastrar funcionario --

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
	@salario		money,
	@cargo			  varchar(max),
	@usuario      varchar(20),
  @senha         varchar(50)       

)
as
begin
	insert into pessoas  
	values (@nome, @telefone, @email, @logradouro, @numero, @complemento, @bairro,
					@cidade, @estado, @cep, @status)
	insert into funcionarios values (@@IDENTITY, @cpf, @salario, @cargo, @usuario, @senha)
end

-- alterar funcionario

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
	@salario		money,
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

	update funcionarios set cpf = @cpf, salario = @salario, cargo = @cargo, usuario = @usuario, senha = @senha
	where pessoa_id = @id
end

-- Cadastrar Veículo
create procedure cadVei
(
	@placa               varchar(7),
  @descricao           varchar(50),
  @valor_diaria        money,
  @lugares             int,
  @carga               int,
  @categoria    varchar(50),
  @tipo         varchar(50),
  @status		        	 int,
  @marca			varchar(max)
)
as
begin 
		insert into veiculos 
		values 
				(@placa, @descricao, @valor_diaria, @lugares, @carga, @categoria, @tipo, @status, @marca)
end 

-- Alterar Veículo

create procedure altVei
(
	@id 						 int,
	@placa               varchar(7),
  @descricao           varchar(50),
  @valor_diaria        money,
  @lugares             int,
  @carga               int,
  @categoria		varchar(50),
  @tipo				varchar(50),
  @status		        	 int,
  @marca			varchar(max)
)
as 
begin 
	update veiculos set placa = @placa, descricao = @descricao, valor_diaria = @valor_diaria,
	lugares = @lugares, carga = @carga, categoria = @categoria, tipo = @tipo, status = @status,
	marca = @marca
	where id = @id; 
end 


/* VIEWS */

create view v_clientes as 
	select p.*, c.cnpj, c.razao_social 
		from pessoas p 
		INNER JOIN clientes c ON  c.pessoa_id = p.id


create view v_funcionarios AS
	select p.*, f.cpf, f.cargo, f.usuario, f.senha, f.salario
		from pessoas p 
		INNER JOIN funcionarios f ON  f.pessoa_id = p.id



create view v_veiculos as 
	select *
	from veiculos 



create view v_reservas as 
select r.contrato_id, r.data_saida, r.data_contratada, r.data_devolucao, r.status,
		   v.*
from 	     reservas r 
inner join v_veiculos v
on				 r.veiculo_placa = v.placa
 


