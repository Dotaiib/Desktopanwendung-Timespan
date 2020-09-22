use Paie

create table Paie_initial(
Id_Table nvarchar(max) not null,
Matricule nvarchar(max) not null,
FullName varchar(max) not null,
The_Date date null,
Entree_01 time null,
Sortie_01 time null,
Entree_02 time null,
Sortie_02 time null,
Entree_03 time null,
Sortie_03 time null,
Original_Time_Minutes time null,
);

create table Paie_total(
Id_Table nvarchar(max) not null,
Matricule nvarchar(max) not null,
FullName varchar(max) not null,
The_Date varchar(max) not null,
Entree_01 nvarchar(max) null,
Sortie_01 nvarchar(max) null,
Entree_02 nvarchar(max) null,
Sortie_02 nvarchar(max) null,
Entree_03 nvarchar(max) null,
Sortie_03 nvarchar(max) null,
Original_Time_Minutes nvarchar(max) null,
Total nvarchar(max) null,
Temps_Sortie nvarchar(max) null
);

create table section(
id int not null
);

insert into section values(100111),(100112),(100113),(2000),(10012)


select * from Paie_initial where Id_Table='99' order by Matricule

select * from Paie_total

delete Paie_total where Id_Table='t01'

update Paie_initial set Sortie_03='05:10' where Id_Table='99' and Matricule='005127FSOS'

insert into Paie_initial values ('test01','000068por','mr','20/07/2020','08:00','12:00','15:20','22:45')

select distinct Matricule,FullName,left(The_Date,10)[The_Date],left(Temps_Sortie,5)[Temps_Sortie]  from Paie_total where Id_Table = '999'

select distinct Matricule,left(The_Date,10)[The_Date],left(Temps_Sortie,5)[Temps_Sortie]  from Paie_total where Id_Table='mpf0508'



/*Trigger replace empty columns 01*/
create trigger replace_empty_time_column01 on Paie_initial After insert as
update Paie_initial set Entree_01='00:00', Sortie_01='00:00' where Entree_01= '' and Sortie_01= ''

/*Trigger replace empty columns 02*/
create trigger replace_empty_time_column02 on Paie_initial After insert as
update Paie_initial set Entree_02='00:00', Sortie_02='00:00' where Entree_02= '' and Sortie_02= ''

/*Trigger replace empty columns 03*/
create trigger replace_empty_time_column03 on Paie_initial After insert as
update Paie_initial set Entree_03='00:00', Sortie_03='00:00' where Entree_03= '' and Sortie_03= ''

*************************************************

/*Trigger replace null columns01*/
create trigger replace_empty_null_column01 on Paie_initial After insert as
update Paie_initial set Entree_01='00:00', Sortie_01='00:00' where Entree_01 is null or Sortie_01 is null

/*Trigger replace null columns02*/
create trigger replace_empty_null_column02 on Paie_initial After insert as
update Paie_initial set Entree_02='00:00', Sortie_02='00:00' where Entree_02 is null or Sortie_02 is null

/*Trigger replace null columns03*/
create trigger replace_empty_null_column03 on Paie_initial After insert as
update Paie_initial set Entree_03='00:00', Sortie_03='00:00' where Entree_03 is null or Sortie_03 is null

******************************************************

/*Trigger replace Temps_Sortie column*/
create trigger replace_Temps_Sortie_column on Paie_total After insert as
update Paie_total set Temps_Sortie= null where Temps_Sortie='103.04:40:39'


INSERT INTO Paie_total VALUES('','007070FSOS','ENNAKNAFI RKIA','05/08/2020 00:00:00','','','','','','','','22:35:00')


select * from Paie_total where Matricule='000368FSOS'

select distinct Id_Table from Paie_initial where Temps_Sortie='103.04:40:39'



