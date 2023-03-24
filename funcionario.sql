select * from Usuarios;
select * from Funcionarios;
select * from Consoles;
select * from Jogos;
select * from Produtos;

select * from Clientes;
select * from Planos;
select * from Agendamentos;
select * from ItemPedido;
select * from Pedidos;


SET IDENTITY_INSERT Usuarios on;
insert into Usuarios(Id, Name, Login, Email, Perfil, Passwd, DateSingIn) values 
(1, 'Admin', 'admin', 'violet.games@hotmail.com', 1, 'd033e22ae348aeb5660fc2140aec35850c4da997', Getdate()),
(2, 'Maka', 'maka', 'violet.games@hotmail.com', 2, 'd033e22ae348aeb5660fc2140aec35850c4da997', Getdate());
SET IDENTITY_INSERT Usuarios off;

SET IDENTITY_INSERT Funcionarios on;
insert into Funcionarios(Id, Name, Email, DateBirthday, CPF, Office, Phone) values 
(1, 'Vitoria Souza',  'vitoriaalmeidasouza@teleworm.us', '08/02/1991', '601.944.620-48', 1, '2155784245'),
(2, 'Vinícius Barros',  'viniciusgomesbarros@superrito.com', '11/07/1928', '193.672.188-00', 2, '1979664863')
SET IDENTITY_INSERT Funcionarios off;

SET IDENTITY_INSERT Clientes on;
insert into Clientes(Id, Name, Email, DateBirthday, CPF, Phone, Plano, PlanDay, payment) values 
(1, 'Beatrice Castro',  'beatriceoliveiracastro@teleworm.us', '13/11/2002', '409.261.988-07', '(11) 99888-5683', 25, '10/03/2023', 2),
(2, 'Guilherme Santos',  'guilhermecunhasantos@armyspy.com', '07/10/1982', '592.824.411-80', '(11) 97953-3318', 0, '10/03/2023', 1),
(3, 'João Costa',  'joaoazevedocosta@superrito.com', '18/07/1986', '460.440.186-10', '(11) 97966-4863', 50, '10/03/2023', 2),
(4, 'Vitoria Souza',  'vitoriaalmeidasouza@teleworm.us', '10/05/1989', '601.944.620-48', '(11) 95100-8359', 0, '10/03/2023', 1)
SET IDENTITY_INSERT Clientes off;

SET IDENTITY_INSERT Planos on;
insert into Planos(Id, CPF, Plano, PlanDay, payment) values 
(1, '409.261.988-07',  25, '10/03/2023', 2),
(2, '460.440.186-10',  50, '10/03/2023', 2)
SET IDENTITY_INSERT Planos off;

SET IDENTITY_INSERT Consoles on;
insert into Consoles(Id, Name, PriceHour, CategoryConsole, StatusConsole) values 
(1, 'PS1',  1, 1, 1),
(2, 'PS2',  2, 2, 2),
(3, 'PS3',  3, 3, 1),
(4, 'PS4',  4, 4, 1),
(5, 'PS4',  4, 4, 2),
(6, 'PS5',  5, 5, 1),
(7, 'Nitendo Wii',  3, 6, 1),
(8, 'PC Gamer',  3, 7, 2),
(9, 'Nitendo 64',  3, 64, 1),
(10, 'Xbox 360',  3, 360, 1),
(11, 'Xbox One',  4, 11, 1),
(12, 'PS1',  1, 1, 3),
(13, 'PS2',  2, 2, 3),
(14, 'PS3',  3, 3, 3)
SET IDENTITY_INSERT Consoles off;

SET IDENTITY_INSERT Jogos on;
insert into Jogos(Id, Name, PriceHour, CategoryConsole, StatusJogo) values 
(1, 'Castlevania: Symphony of The Night',  1, 1, 1),
(2, 'Grand Theft Auto: San Andreas',  2, 2, 2),
(3, 'Red Dead Redemption',  3, 3, 1),
(4, 'Final Fantasy VII Remake',  4, 4, 1),
(5, 'The Witcher 3: Wild Hunt',  4, 4, 2),
(6, 'God of War Ragnarok',  5, 5, 1),
(7, 'Okami',  3, 6, 1),
(8, 'Overwatch 2',  3, 7, 2),
(9, 'The Legend of Zelda: Ocarina of Time',  3, 64, 1),
(10, 'GTA V',  3, 360, 1),
(11, 'Forza Horizon 5',  4, 11, 1),
(12, 'Silent Hill',  1, 1, 3),
(13, 'Silent Hill 2',  2, 2, 3),
(14, 'Portal 2',  3, 3, 3)
SET IDENTITY_INSERT Jogos off;

SET IDENTITY_INSERT Produtos on;
insert into Produtos(Id, Name, QtdAvailable, PriceUnity, CategoryProduct) values 
(1, 'Castlevania: Symphony of The Night - PS1',  10, 10, 1),
(2, 'Grand Theft Auto: San Andreas - PS2',  20, 20, 1),
(3, 'Red Dead Redemption - PS3',  30, 30, 1),
(4, 'Final Fantasy VII Remake - PS4',  40, 40, 1),
(5, 'The Witcher 3: Wild Hunt - PS4',  40, 40, 1),
(6, 'God of War Ragnarok - PS5',  50, 50, 1),
(7, 'Okami - Nitendo Wii',  30, 60, 1),
(8, 'Overwatch 2 - PS4',  30, 70, 1),
(9, 'The Legend of Zelda: Ocarina of Time - Nitendo 64',  30, 64, 1),
(10, 'GTA V - Xbox 360',  30, 36, 1),
(11, 'Forza Horizon 5 - Xbox One',  40, 11, 1),
(12, 'Silent Hill - PS1',  10, 10, 1),
(13, 'Silent Hill 2 - PS2',  20, 20, 1),
(14, 'Portal 2 - PS3',  30, 30, 1),
(15, 'PS4',  40, 4000, 2),
(16, 'PS5',  50, 5000, 2),
(17, 'Nitendo Wii',  30, 600, 2),
(18, 'PC Gamer',  30, 700, 2),
(19, 'Nitendo 64',  30, 640, 2),
(20, 'Xbox 360',  30, 360, 2),
(21, 'Xbox One',  40, 1100, 2),
(22, 'PS1',  10, 100, 2),
(23, 'PS2',  20, 200, 2),
(24, 'PS3',  30, 300, 2)
SET IDENTITY_INSERT Produtos off;



