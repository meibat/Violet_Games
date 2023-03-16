select * from Agendamentos;
select * from ItemPedido;
select * from Pedidos;
select * from Usuarios;
select * from Funcionarios;
select * from Clientes;
select * from Consoles;
select * from Jogos;
select * from Produtos;


SET IDENTITY_INSERT Usuarios on;
SET IDENTITY_INSERT Agendamentos on;

insert into Usuarios(Id, Name, Login, Email, Perfil, Passwd, DateSingIn) values 
(1, 'Admin', 'admin', 'violet.games@hotmail.com', 1, 'd033e22ae348aeb5660fc2140aec35850c4da997', Getdate());

insert into Agendamentos(Id, LoginUser, CPFClient, NameGameOrConsole, Category, DateSchedule, TotalValue, Payment) values 
(1, 'Mei', '458329349843', 'Forza 5', 1, Getdate(), 2, 2);
