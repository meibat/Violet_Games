select * from Usuarios;
select * from Funcionarios;
select * from Clientes;
select * from Consoles;
select * from Produtos;


SET IDENTITY_INSERT Usuarios ON;

insert into Usuarios(Id, Name, Login, Email, Perfil, Passwd, DateSingIn) values 
(1, 'Admin', 'admin', 'violet.games@hotmail.com', 1, 'admin', Getdate());
